using System;
using System.Configuration;


namespace com.etak.core.jms.listener
{
    /// <summary>
    /// Kind of possible destinations in a JMS system
    /// </summary>
    public enum DestinationTypes
    {
        /// <summary>
        /// Destination type queue
        /// </summary>
        Queue,

        /// <summary>
        /// Destination type topic
        /// </summary>
        Topic
    }

    /// <summary>
    /// Class holding all the configuration parameters for a JMS listener/connection
    /// </summary>
    public class JMSConnectionConfiguration : ConfigurationSection
    {
        /// <summary>
        /// The connection URL to the JMS provider (NMS Active MQ syntax)
        /// </summary>
        [ConfigurationProperty("URL", IsRequired = true)]
        public String URL
        {
            get { return (String)this["URL"]; }
            set { this["URL"] = value; }
        }

        /// <summary>
        /// The Id of the client that will be sent to the JMS provider
        /// </summary>
        [ConfigurationProperty("ClientId", IsRequired = true)]
        public String ClientId
        {
            get { return (String)this["ClientId"]; }
            set { this["ClientId"] = value; }
        }

        /// <summary>
        /// The type of destination where it will connect to (Queue or Topic)
        /// </summary>
        [ConfigurationProperty("DestinationType", IsRequired = true)]
        public DestinationTypes DestinationType
        {
            get { return (DestinationTypes)this["DestinationType"]; }
            set { this["DestinationType"] = value; }
        }

        /// <summary>
        /// The name of destination where it will connect (Queue/Topic name)
        /// </summary>
        [ConfigurationProperty("DestinationName", IsRequired = true)]
        public String DestinationName
        {
            get { return (String)this["DestinationName"]; }
            set { this["DestinationName"] = value; }
        }

        /// <summary>
        /// The number of threads that will be used, each thread 
        /// will open one connection to the JMS provided configured
        /// Via the URL parameter of this class.
        /// </summary>
        [ConfigurationProperty("NumberOfListeners", IsRequired = true)]
        public Int16 NumberOfListeners
        {
            get { return (Int16)this["NumberOfListeners"]; }
            set { this["NumberOfListeners"] = value; }
        }

        /// <summary>
        /// The amount by which the reconnect delay will be multiplied by if useExponentialBackOff is enabled.
        /// </summary>
        [ConfigurationProperty("RedeliveryPolicy.BackOffMultiplier", IsRequired = true)]
        public Int32 BackOffMultiplier
        {
            get { return (Int32)this["RedeliveryPolicy.BackOffMultiplier"]; }
            set { this["RedeliveryPolicy.BackOffMultiplier"] = value; }
        }

        /// <summary>
        ///  This causes the redelivery delay to be adjusted in order to avoid possible collision when messages are redelivered to concurrent consumers. 
        /// </summary>
        [ConfigurationProperty("RedeliveryPolicy.CollisionAvoidancePercent", IsRequired = true)]
        public int CollisionAvoidancePercent
        {
            get { return (Int32)this["RedeliveryPolicy.CollisionAvoidancePercent"]; }
            set { this["RedeliveryPolicy.CollisionAvoidancePercent"] = value; }
        }

        /// <summary>
        /// The initial redelivery delay in milliseconds 
        /// </summary>
        [ConfigurationProperty("RedeliveryPolicy.InitialRedeliveryDelay", IsRequired = true)]
        public int InitialRedeliveryDelay
        {
            get { return (Int32)this["RedeliveryPolicy.InitialRedeliveryDelay"]; }
            set { this["RedeliveryPolicy.InitialRedeliveryDelay"] = value; }
        }

        /// <summary>
        ///  A value less than zero indicates that there is no maximum and the NMS provider should retry forever. 
        /// </summary>
        [ConfigurationProperty("RedeliveryPolicy.MaximumRedeliveries", IsRequired = true)]
        public int MaximumRedeliveries
        {
            get { return (Int32)this["RedeliveryPolicy.MaximumRedeliveries"]; }
            set { this["RedeliveryPolicy.MaximumRedeliveries"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to [use collision avoidance]. 
        /// </summary>
        [ConfigurationProperty("RedeliveryPolicy.UseCollisionAvoidance", IsRequired = true)]
        public Boolean UseCollisionAvoidance
        {
            get { return (Boolean)this["RedeliveryPolicy.UseCollisionAvoidance"]; }
            set { this["RedeliveryPolicy.UseCollisionAvoidance"] = value; }
        }

        /// <summary>
        /// Should exponential back-off be used (i.e. to exponentially increase the timeout) 
        /// </summary>
        [ConfigurationProperty("RedeliveryPolicy.UseExponentialBackOff", IsRequired = true)]
        public bool UseExponentialBackOff
        {
            get { return (Boolean)this["RedeliveryPolicy.UseExponentialBackOff"]; }
            set { this["RedeliveryPolicy.UseExponentialBackOff"] = value; }
        }

        /// <summary>
        /// Number of elements to pre-read from the queue, higher number
        /// increases performance, but reduces the performance of the cluster
        /// as some nodes may starve others.
        /// </summary>
        [ConfigurationProperty("PrefetchPolicy", IsRequired = true)]
        public Int32 PrefetchPolicy
        {
            get { return (Int32)this["PrefetchPolicy"]; }
            set { this["PrefetchPolicy"] = value; }
        }
    }

}
