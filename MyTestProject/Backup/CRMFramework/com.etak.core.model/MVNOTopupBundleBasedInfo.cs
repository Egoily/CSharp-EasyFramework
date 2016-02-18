using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MVNOTopupBundleBasedInfo
    {
        public MVNOTopupBundleBasedInfo()
        { 
        }

        public MVNOTopupBundleBasedInfo(MVNOTopupBundleBasedKey key, int daysValid, int daysFrozenAfterExpiration)
        {
            this.MVNOTopupBundleBasedKey = key;
            this.DaysFrozenAfterExpiration = daysFrozenAfterExpiration;
            this.DaysValid = daysValid;
        }

        public virtual MVNOTopupBundleBasedKey MVNOTopupBundleBasedKey { get; set; }
        public virtual int DaysValid { get; set; }
        public virtual int DaysFrozenAfterExpiration { get; set; }
        public virtual int DaysToAccumulateTopups { get; set; }

    }
}
