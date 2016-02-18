using System;
using System.Configuration;

namespace com.etak.core.queue.Common
{
    /// <summary>
    /// Defines the configuration parameters for a Queue thread
    /// </summary>
    public class QueueThreadConfiguration : ConfigurationSection
    {
        /// <summary>
        /// The maximun size of the queue
        /// </summary>
        [ConfigurationProperty("MaxQueueSize", IsRequired = true)]
        public Int32 MaxQueueSize
        {
            get
            { return (Int32)this["MaxQueueSize"]; }
            set
            { this["MaxQueueSize"] = value; }
        }

        /// <summary>
        /// How many elements can be on the queue before trigerring 
        /// the launch o the dequeueing
        /// </summary>
        [ConfigurationProperty("MaxQueuedElementsOnIdle", IsRequired = true)]
        public Int32 MaxQueuedElementsOnIdle
        {
            get
            { return (Int32)this["MaxQueuedElementsOnIdle"]; }
            set
            { this["MaxQueuedElementsOnIdle"] = value; }
        }

        /// <summary>
        /// Interval between polling to the queue
        /// </summary>
        [ConfigurationProperty("PollingTimeMilliseconds", IsRequired = true)]
        public Int32 PollingTimeMilliseconds
        {
            get
            { return (Int32)this["PollingTimeMilliseconds"]; }
            set
            { this["PollingTimeMilliseconds"] = value; }
        }

        /// <summary>
        /// When the queue is full, how many elements needs to be backed up 
        /// per loop
        /// </summary>
        [ConfigurationProperty("ElementsToBackupPerLoop", IsRequired = true)]
        public Int32 ElementsToBackupPerLoop
        {
            get
            { return (Int32)this["ElementsToBackupPerLoop"]; }
            set
            { this["ElementsToBackupPerLoop"] = value; }
        }

        /// <summary>
        /// What is the maximun elements to be processed (dequeued) by loop 
        /// </summary>
        [ConfigurationProperty("MaxElementsPerLoop", IsRequired = true)]
        public Int32 MaxElementsPerLoop
        {
            get
            { return (Int32)this["MaxElementsPerLoop"]; }
            set
            { this["MaxElementsPerLoop"] = value; }
        }

        /// <summary>
        /// What is the maximun number of attempts to process/Dequeue
        /// before giving up and marking it as failed
        /// </summary>
        [ConfigurationProperty("MaxRetryCount", IsRequired = true)]
        public Int32 MaxRetryCount
        {
            get
            { return (Int32)this["MaxRetryCount"]; }
            set
            { this["MaxRetryCount"] = value; }
        }

        [ConfigurationProperty("MinRetrySeconds", IsRequired = true)]
        public Int32 MinRetrySeconds
        {
            get
            { return (Int32)this["MinRetrySeconds"]; }
            set
            { this["MinRetrySeconds"] = value; }
        }

        /// <summary>
        /// The name of the thread in which the QueueThread runs
        /// </summary>
        [ConfigurationProperty("QueueThreadName", IsRequired = false, DefaultValue="QueueThread")]
        public String QueueThreadName
        {
            get
            { return (String)this["QueueThreadName"]; }
            set
            { this["QueueThreadName"] = value; }
        }
    }
}
