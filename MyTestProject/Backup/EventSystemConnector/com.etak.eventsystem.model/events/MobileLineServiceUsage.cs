using System;
using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class MobileLineServiceUsage : StrongTypedEvent
    {
        [DataMember()]
        public MobileLineService Resource { get; set; }
        [DataMember()]
        public Customer Customer {get;set;}
        [DataMember()]
        public Dealer Daler { get; set; }
        [DataMember()]
        public String ResourceID { get; set; }
        [DataMember()]
        public Int64 CDRId { get; set; }
        [DataMember()]
        public Boolean IsFirstUsed { get; set; }

    }
}