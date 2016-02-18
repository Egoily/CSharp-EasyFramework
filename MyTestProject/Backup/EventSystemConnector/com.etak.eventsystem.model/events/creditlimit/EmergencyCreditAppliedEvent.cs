using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.creditlimit
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class EmergencyCreditAppliedEvent : StrongTypedEvent
    {
        [DataMember]
        public Customer AppliedCustomer { get; set; }
    }
}
