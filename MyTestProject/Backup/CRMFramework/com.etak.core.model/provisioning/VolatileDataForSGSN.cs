using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    [DataContract, Serializable]
    public class VolatileDataForSGSN
    {
        [DataMember]
        public Boolean? IsGPRSAttached { get; set; }

        [DataMember]
        public String CurrentSGSN { get; set; }

        [DataMember]
        public String PreviousSGSN { get; set; }
       
        [DataMember]
        public Boolean? IsMTSMSupportedBySGSN { get; set; }

        [DataMember]
        public Boolean? IsPurgedForGPRS { get; set; }

        [DataMember]
        public Boolean? IsSentGPRSSubscriptionRestrictData { get; set; }

        [DataMember]
        public Boolean? IsSGSNAreaRestricted { get; set; }

        [DataMember]
        public Boolean? IsSGSNInformPreviousforSuperCharger { get; set; }

        [DataMember]
        public Boolean? IsSGSNSupportsGPRSEnhancements { get; set; }

        [DataMember]
        public String SGSNAgeIndicator { get; set; }

        [DataMember]
        public String GPRSStateChangeTime { get; set; }
    }
}
