using System;
using System.Configuration;

namespace com.etak.core.queue.Common
{
    public class QueueThreadConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("MaxQueueSize", IsRequired = true)]
        public Int32 MaxQueueSize
        {
            get
            { return (Int32)this["MaxQueueSize"]; }
            set
            { this["MaxQueueSize"] = value; }
        }

        [ConfigurationProperty("MaxQueuedElementsOnIdle", IsRequired = true)]
        public Int32 MaxQueuedElementsOnIdle
        {
            get
            { return (Int32)this["MaxQueuedElementsOnIdle"]; }
            set
            { this["MaxQueuedElementsOnIdle"] = value; }
        }

        [ConfigurationProperty("PollingTimeMilliseconds", IsRequired = true)]
        public Int32 PollingTimeMilliseconds
        {
            get
            { return (Int32)this["PollingTimeMilliseconds"]; }
            set
            { this["PollingTimeMilliseconds"] = value; }
        }

        [ConfigurationProperty("ElementsToBackupPerLoop", IsRequired = true)]
        public Int32 ElementsToBackupPerLoop
        {
            get
            { return (Int32)this["ElementsToBackupPerLoop"]; }
            set
            { this["ElementsToBackupPerLoop"] = value; }
        }

        [ConfigurationProperty("MaxElementsPerLoop", IsRequired = true)]
        public Int32 MaxElementsPerLoop
        {
            get
            { return (Int32)this["MaxElementsPerLoop"]; }
            set
            { this["MaxElementsPerLoop"] = value; }
        }

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
