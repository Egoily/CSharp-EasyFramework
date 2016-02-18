using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.customer
{
    //added by damon 2014-07-04 for select language
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class FirstActivationNotificationEvent : NotificationEvent
    {
        [DataMember]
        public int NewStatus { get; set; }

        [DataMember]
        public int OldStatus { get; set; }
    }
}