using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.customer
{
    public class AlElmRegisterFailureScheduleEvent : StrongTypedEvent
    {
        [DataMember]
        public Customer ToCustomer { get; set; }

        [DataMember]
        public bool IsDelete { get; set; }

        [DataMember]
        public int BlockCode { get; set; }

        [DataMember]
        public string BlockComment { get; set; }
    }
}
