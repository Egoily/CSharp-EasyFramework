using System;
using System.Collections.Generic;
using System.Threading;
using Apache.NMS;

namespace com.etak.core.jms.listener
{
    /// <summary>
    /// Event system receiver over JMS,
    /// </summary>
    /// <typeparam name="TExecutor">The type that will instantiated per message to proccess the message, must implement EventSystemContract</typeparam>
    public class JMSMessageListener<TExecutor> where TExecutor : IMessageProcessor
    {
        static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        private volatile Boolean _running = true;
        private readonly IList<Thread> _listenThreads = new List<Thread>();
        private readonly JMSConnectionConfiguration _conf;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="conf">the configuration settings to start the receiver</param>
        public JMSMessageListener(JMSConnectionConfiguration conf)
        {
            _conf = conf;
        }

        /// <summary>
        /// Starts the event system with the configured number of Listener threads
        /// </summary>
        public void Start()
        {
            for (int i = 0; i < _conf.NumberOfListeners; i++)
            {
                Thread listenThread = new Thread(ListenAndDispatch) {Name = "ListenThread-" + (i + 1)};
                listenThread.Start();
                
                _listenThreads.Add(listenThread);
            }
            
            _running = true;
            Log.Info("Listen Threads started");
        }

        /// <summary>
        /// Stops all the JMS listeners
        /// </summary>
        public void Stop()
        {           
            _running = false;
            foreach (var listenThread in _listenThreads)
            {
                Log.Info("Waiting to " + listenThread.Name + " to stop");
                listenThread.Interrupt();
                if (!listenThread.Join(100000))
                {
                    Log.Warn("Listen Thread could not be stoped, aborting.");
                    listenThread.Abort();
                }
                else
                {
                    Log.Info("All messages have been processed, and listerner shutted down cleanly");
                }
            }
            Log.Info("Event Receiver shutdown complete");
        }


        private void ListenAndDispatch()
        {  
            //Declare local JMS listeners
            JMSConnection connection = new JMSConnection(_conf);
            connection.GenerateConnectionFactory();

            while (_running)
            {
                 Boolean shouldRollback = false;
                 Boolean shouldClosession = true;

                try
                {
                    if (!connection.Connected())
                        connection.ConnectWithSession();

                    while (_running)
                    {
                        IMessage message = connection.GetMessage();
                        shouldRollback = true;
                        
                        //Create an instance of the executor
                        IMessageProcessor executor = Activator.CreateInstance<TExecutor>();

                        //Invoke the executor
                        executor.ProcessMessage(message);
                        connection.Commit();
                    }
                }
                catch (ThreadInterruptedException ex)
                {
                    if (!_running)
                    {
                        Log.Info("The listen thread has been requested to stop");
                    }
                    else
                    {
                        Log.Error("Unexpected ThreadInterruptedException in listening thread", ex);
                    }
                }
                catch (InvalidMessageException)
                {
                    //Rollback, the this will make that redelivery policy kicks in
                    //so at the end it goes to DQL despite it's not the most optimal.
                    shouldClosession = false;
                }
                catch (NMSException ex)
                {
                    Exception recursiveEx = ex;
                    Log.Error("JMSException, Error receiving messages:", ex);
                    while (recursiveEx.InnerException != null)
                    {
                        Log.Error("Error receiving Inner exception: ", recursiveEx.InnerException);
                        recursiveEx = recursiveEx.InnerException;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("JMSException, Error receiving messages:", ex);
                    while (ex.InnerException != null)
                    {
                        Log.Error("Error receiving Inner exception: ", ex.InnerException);
                        ex = ex.InnerException;
                    }
                }

                if (shouldRollback)
                {
                    try
                    {
                        Log.Info("Rolling back session");
                        connection.Rollback();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Error rolling back message", ex);
                    }
                }

                if (shouldClosession)
                {
                    try
                    {
                        //Reset connection if there is a JMS error.
                        Log.Info("Clossing the JMS connection");
                        connection.CloseSessionAndConnection();

                        //Sleep some time, in case the system is down, to avoid extra load
                        //With multiple threads trying to connect concurrently
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Error clossing connection", ex);
                    }
                }
            }

            //we should stop now.
            try
            {
                Log.Info("Clossing connection to the queue system");
                if (connection.Connected())
                    connection.CloseSessionAndConnection();
            }
            catch (Exception ex)
            {
                Log.Error("Error clossing the connection to the queue system:", ex);
            }
            Log.Info("Listening thread exit");
        }
    }
}
