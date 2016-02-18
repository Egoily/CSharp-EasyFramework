using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.provisioning
{
    [DataContract, Serializable]
    public class VolatileDataForMME
    {
        [DataMember]
        public Boolean? IsPurgedInMME { get; set; }
        [DataMember]
        public String MMEAttachedStatus { get; set; }
        [DataMember]
        public String MMEAttachedTimestamp { get; set; }
        [DataMember]
        public String VisitedPLMNMCC { get; set; }
        [DataMember]
        public String VisitedPLMNMNC { get; set; }
        [DataMember]
        public String  MMERATType { get; set; }
        [DataMember]
        public String MMEAPNScreeningCOS { get; set; }
        [DataMember]
        public String ULRFlags { get; set; }
        [DataMember]
        public String ULRFeatureList { get; set; }
        [DataMember]
        public String CurrentMMENumber { get; set; }
        [DataMember]
        public String CurrentMMEName { get; set; }
        [DataMember]
        public String CurrentMMEDestRealm { get; set; }
        [DataMember]
        public String PreviousMMENumber { get; set; }
        [DataMember]
        public String PreviousMMEName { get; set; }
    }
}
