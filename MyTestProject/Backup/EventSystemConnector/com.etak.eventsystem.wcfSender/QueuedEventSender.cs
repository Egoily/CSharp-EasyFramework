using System;
using System.Configuration;
using com.etak.core.queue.Common;
using com.etak.eventsystem.model;
using com.etak.eventsystem.model.events;

namespace com.etak.eventsystem.wcfSender
{
    /// <summary>
    /// Guaranteed delivery class, reboot proof, Initialize and Stop methods must be invoked.
    /// </summary>
    public class QueuedEventSender : EventSystemContract
    {
        private readonly QueueThread<Event> _queue;        
        private static readonly Object ObjectLock = new object();        
        private static QueuedEventSender _instance;

        /// <summary>
        /// Initializes the queued sender using the configurations section "QueueingSettings" of type QueueThreadConfiguration
        /// </summary>
        /// <param name="senderFactory">Factory to create the real worker that will be used to deliver the messages</param>
        /// <param name="queueConfig">The queueing settings for the intermediate queue for async</param>
        public static void Initialize(IEventContractImplementorFactory senderFactory, QueueThreadConfiguration queueConfig)
        {
            lock (ObjectLock)
            {
                if (_instance != null)
                {
                    throw new Exception("Already Initialized");
                }

                if (senderFactory == null)
                 throw new ArgumentNullException("senderFactory");
                
                if (queueConfig == null)
                 throw new ArgumentNullException("queueConfig");

                _instance = new QueuedEventSender(senderFactory, queueConfig);
                _instance._queue.Start();
            }
        }

        public static void Initialize(IEventContractImplementorFactory senderFactory)
        {
            //Read Queue Conf from web|app.config
            QueueThreadConfiguration queueConfig =
                (QueueThreadConfiguration) ConfigurationManager.GetSection("QueueingSettings");
            if (queueConfig == null)
            {
                throw new Exception("The confiuration section QueueingSettings was not defined");
            }

            Initialize(senderFactory, queueConfig);
        }

        ~QueuedEventSender()
        {
            Console.WriteLine("QueuedEventSender destructor start");
            _queue.Stop();
            Console.WriteLine("QueuedEventSender destructor complete");
        }

        public static QueuedEventSender GetInstance()
        {
            if (_instance == null)
            {
                throw new Exception("The sender has not been initialized");
            }
            return (_instance);
        }

        private QueuedEventSender(IEventContractImplementorFactory sender, QueueThreadConfiguration queueConfig)
        {
            DequeueEventProcessorAdapter adapter2Sender = new DequeueEventProcessorAdapter(sender);
            _queue = new QueueThread<Event>(queueConfig, adapter2Sender);
        }      

        public void ProcessEvent(Event ev)
        {
            _queue.Enqueue(ev);
        }

        public static void Stop()
        {
            if (_instance != null)
            {
                _instance._queue.Stop();
            }
        }
    }
}
