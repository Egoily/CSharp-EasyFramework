using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.customer
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class AlElmRetryRegisterRejectEvent : NotificationEvent
    {
        [DataMember]
        public string MSISDN { get; set; }

        [DataMember]
        public string IDNumber { get; set; }
    }
}
