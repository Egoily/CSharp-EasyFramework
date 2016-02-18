using System;
using System.Runtime.Serialization;

namespace com.etak.simpleeventsystem.model.events
{
    [KnownType(typeof(SimpleEvent))]
    [DataContract(Name = "SimpleEvent", Namespace = "http://com.etak.simpleeventsystem")]
    public class SimpleEvent
    {
        [DataMember(Name = "EventType", IsRequired = true, Order = 0)]
        public String EventType { get; set; }

        [DataMember(Name = "EventDate", IsRequired = false, Order = 1)]
        public DateTime EventDate { get; set; }
        
        [DataMember(Name = "EventId", IsRequired = false, Order = 2)]
        public String EventId { get; set; }

        [DataMember(Name = "Payload", IsRequired = false, Order = 3)]
        public String Payload { get; set; }

        [DataMember(Name = "MVNOID", IsRequired = false, Order = 4)]
        public Int32 MVNOID { get; set; }        
    }

}