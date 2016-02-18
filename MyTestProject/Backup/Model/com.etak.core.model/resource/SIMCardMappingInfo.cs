using System;

namespace com.etak.core.model
{
    [Serializable]
    public class SIMCardMappingInfo
    {
        public virtual string IMSI_T { get; set; }
        public virtual string IMSI_P { get; set; }
        public virtual string MSISDN_T { get; set; }
    }
}
