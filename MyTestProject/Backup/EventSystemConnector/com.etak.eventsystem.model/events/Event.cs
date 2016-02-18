using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;
using com.etak.eventsystem.model.events.creditlimit;
using com.etak.eventsystem.model.events.customer;
using com.etak.eventsystem.model.events.CustomerStatusChange;
using com.etak.eventsystem.model.events.dre;
using com.etak.eventsystem.model.events.PortIn;
using com.etak.eventsystem.model.events.promotion;

namespace com.etak.eventsystem.model.events
{        
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public abstract class Event
    {
        [DataMember(IsRequired = true, Order = 1)]
        public String EventId { get; set; }

        [DataMember(IsRequired = true, Order = 2)]
        public DateTime EventDate { get; set; }
     
        [DataMember(EmitDefaultValue = false, IsRequired = false, Order = 3)]
        public DateTime ? ScheduledTime { get; set; }

        [DataMember(IsRequired = false, Order = 4)]
        public Int32 MVNOID { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class CustomPayloadEvent : Event
    {
        [DataMember(Name = "EventType", IsRequired = true, Order = 0)]
        public  string EventType { get; set; }

        [DataMember(Name = "Payload", IsRequired = false, Order = Int32.MaxValue)]
        public String Payload { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public abstract class StrongTypedEvent : Event
    {
        // maybe something common for Strong typed events
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class TestEvent : StrongTypedEvent
    {
        [DataMember()]
        public MobileLineService line { get; set; }

        [DataMember()]
        public Product product { get; set; }

        [DataMember()]
        public Service service { get; set; }

        [DataMember()]
        public CallDetailRecord CDR { get; set; }

        [DataMember()]
        public TopUp topup { get; set; }

        [DataMember()]
        public CustomerProperty customerProperty { get; set; }

        [DataMember()]
        public Dealer dealer { get; set; }

        [DataMember()]
        public Package package { get; set; }

        [DataMember()]
        public PromotionPlan promotionPlan { get; set; }

        [DataMember]
        public Customer customer { get; set; }
    }
}