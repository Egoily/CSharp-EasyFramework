using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class RegistrationEvent : StrongTypedEvent
    {
        [DataMember()]
        public virtual Customer Customer { get; set; }
		[DataMember()]
		public virtual Dealer Dealer { get; set; }
        [DataMember()]
        public virtual Customer ReferralCustomer { get; set; }
    }
}