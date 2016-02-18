using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.dre
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class BlockCallOutEvent : StrongTypedEvent
    {
        [DataMember]
        public Customer BlockedCustomer { get; set; }
    }
}
