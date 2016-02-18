using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.promotion
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class ApplyPromotionAfterMNPAcceptedEvent : StrongTypedEvent
    {
        [DataMember]
        public Customer Customer { get; set; }     
    }
}
