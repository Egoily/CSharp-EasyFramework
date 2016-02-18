using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.PortIn
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PortInRejectedEvent : NotificationEvent
    {
        [DataMember]
        public string Reason { get; set; }
    }
}