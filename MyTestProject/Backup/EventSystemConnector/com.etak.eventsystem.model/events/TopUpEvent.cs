using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class TopUpEvent : StrongTypedEvent
    {
        [DataMember()]
        public Dealer CustomersDealer { get; set; }

        [DataMember()]
        public Customer Customer { get; set; }

        [DataMember()]
        public TopUp TopUpData { get; set; }

        [DataMember()]
        public NumberInfo NumberInfo { get; set; }
    }
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PostPaidPayEvent : TopUpEvent
    {
 
    }
}