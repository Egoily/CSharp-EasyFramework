using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class NoDocumentValidationReceivedInWDaysEvent : StrongTypedEvent
    {
        [DataMember()]
        public Customer CustomerInfo { get; set; }
        [DataMember()]
        public MobileLineService Service { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class NoDocumentValidationReceivedInXDaysEvent : StrongTypedEvent
    {
        [DataMember()]
        public Customer CustomerInfo { get; set; }
        [DataMember()]
        public MobileLineService Service { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class NoDocumentValidationReceivedInYDaysEvent : StrongTypedEvent
    {
        [DataMember()]
        public Customer CustomerInfo { get; set; }
        [DataMember()]
        public MobileLineService Service { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class NoDocumentValidationReceivedInZDaysEvent : StrongTypedEvent
    {
        [DataMember()]
        public Customer CustomerInfo { get; set; }
        [DataMember()]
        public MobileLineService Service { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class DocumentValidatedEvent : StrongTypedEvent
    {
        [DataMember()]
        public Customer CustomerInfo { get; set; }
        [DataMember()]
        public MobileLineService Service { get; set; }
		[DataMember()]
		public virtual Dealer Dealer { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class DocumentInValidatedEvent : StrongTypedEvent
    {
        [DataMember()]
        public Customer CustomerInfo { get; set; }
        [DataMember()]
        public MobileLineService Service { get; set; }
    }
}