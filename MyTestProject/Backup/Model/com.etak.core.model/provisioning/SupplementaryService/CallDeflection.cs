using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.provisioning
{
    [Serializable]
    public class CallDeflection : SuplementaryService
    {
        /// <summary>
        /// Calling subscriber receives notification that her call has been deflected
        /// </summary>
        [DataMember]
        public Boolean ReceiveNotification { get; set; }

        /// <summary>
        /// MSISDN of the served subscriber can be presented to the deflected-to subscriber
        /// </summary>
        [DataMember]
        public Boolean MSISDNForwarded { get; set; }
    }
}
