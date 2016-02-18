using System;
using System.Threading;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.Policies;

namespace com.etak.core.jms.listener
{
    /// <summary>
    /// A connection to a JMS system using transactions
    /// </summary>
    public class JMSConnection
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        private IConnectionFactory _factory;
        private readonly JMSConnectionConfiguration _conf;
        private IRedeliveryPolicy _redeliveryPolicy;
        private PrefetchPolicy _prefetchPolicy;
        private ISession _session;
        private IConnection _con;
        private IMessageConsumer _consumer;
        private volatile Boolean _needsCommit;
        
        /// <summary>
        /// Default constructor providing the queue configuration
        /// </summary>
        /// <param name="conf">the configuration that should be used for the connection to JMS</param>
        public JMSConnection(JMSConnectionConfiguration conf)
        {
            _conf = conf;
        }

        /// <summary>
        /// Starts the connection to the JMS Provider
        /// </summary>
        public void ConnectWithSession()
        {
            _con = _factory.CreateConnection();
            _con.RedeliveryPolicy = _redeliveryPolicy;
            (_con as Connection).PrefetchPolicy = _prefetchPolicy;
            

            _session = _con.CreateSession(AcknowledgementMode.Transactional);
           
            IDestination destination;

            if (_conf.DestinationType == DestinationTypes.Queue)
                destination = _session.GetQueue(_conf.DestinationName);
            else if (_conf.DestinationType == DestinationTypes.Topic)
                destination = _session.GetTopic(_conf.DestinationName);
            else
                throw new Exception("Unknown Destination Type: " + _conf.DestinationType);

            _consumer = _session.CreateConsumer(destination);
            _con.Start();
        }

        /// <summary>
        /// Gets a new message from the connection
        /// </summary>
        /// <returns></returns>
        public IMessage GetMessage()
        {
            if (_needsCommit)
            {
                throw new Exception("Previous message has not been commited");
            }
            
            IMessage message = _consumer.Receive();
            _needsCommit = true;
            return (message);
        }

        /// <summary>
        /// Commits the read messages 
        /// </summary>
        public void Commit()
        {
            if (!_needsCommit)
            {
                throw new Exception("Can't commit, no message has been requested");
            }
            _session.Commit();
            _needsCommit = false;
        }

        /// <summary>
        /// Rollbacks all the messages read marking them as Unread
        /// </summary>
        public void Rollback()
        {
            if (!_needsCommit)
            {
                throw new Exception("Can't rollback, no message has been requested");
            }
            _session.Rollback();
            _needsCommit = false;
        }

        /// <summary>
        /// Closses the session and the underlaying connection
        /// </summary>
        public void CloseSessionAndConnection()
        {
            try
            {
                _consumer.Close();
                _consumer = null;
            }
            catch (Exception ex)
            {
                Log.Info("Error clossiong the consumer", ex);
            }
            try
            {
                _session.Close();
                _session = null;
            }
            catch (Exception ex)
            {
                Log.Info("Error clossiong the session", ex);
            }
            try
            {
                _con.Close();
                _con = null;
            }
            catch (Exception ex)
            {
                Log.Info("Error clossiong the connection", ex);
            }

        }

        /// <summary>
        /// Creates a new connection factory to generate the
        /// connections to the JMS provider
        /// </summary>
        public  void  GenerateConnectionFactory()
        {
            _redeliveryPolicy = new RedeliveryPolicy
            {
                BackOffMultiplier = _conf.BackOffMultiplier,
                CollisionAvoidancePercent = _conf.CollisionAvoidancePercent,
                InitialRedeliveryDelay = _conf.InitialRedeliveryDelay,
                MaximumRedeliveries = _conf.MaximumRedeliveries,
                UseCollisionAvoidance = _conf.UseCollisionAvoidance,
                UseExponentialBackOff = _conf.UseExponentialBackOff
            };

            _prefetchPolicy= new PrefetchPolicy
            {
               All = _conf.PrefetchPolicy
            };


            if (_factory == null)
            {
                _factory = new ConnectionFactory(_conf.URL, _conf.ClientId +"-"+ Thread.CurrentThread.Name);
            }
        }

        /// <summary>
        /// Checks if the session is connected
        /// </summary>
        /// <returns></returns>
        public bool Connected()
        {
            return (_session != null);
        }
    }
}
