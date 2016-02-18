using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    [DataContract, Serializable]
    public class VolatileDataForVLR
    {
        [DataMember]
        public Boolean? IsGSMAttached { get; set; }

        [DataMember]
        public String CurrentVLR { get; set; }
        [DataMember]
        public String PreviousVLR { get; set; }

        [DataMember]
        public String CurrentMSC { get; set; }
        [DataMember]
        public String PreviousMSC { get; set; }


        // AnnaM: 20140519
        [DataMember]
        public Boolean? IsMSCAreaRestricted { get; set; }
        [DataMember]
        public Boolean? IsMTSMSupportedbyVLR { get; set; }
        [DataMember]
        public Boolean? IsPurgedforGSM { get; set; }
        [DataMember]
        public Boolean? IsSentGSMSubscriptionRestrictData { get; set; }
        [DataMember]
        public Boolean? IsSubscriberTraceSupportedbyVLR { get; set; }
        [DataMember]
        public Boolean? IsVLRInformPreviousforSuperCharger { get; set; }
        [DataMember]
        public String VLRAgeIndicator { get; set; }
        [DataMember]
        public String VLRStateChangeTime { get; set; }
    }
}
