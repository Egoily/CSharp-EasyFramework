using System;
using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;
namespace com.etak.eventsystem.model.events.creditlimit
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class CreditLimitLeftNotificationEvent : NotificationEvent
    {
        [DataMember()]
        public virtual decimal CurrentLimit { get; set; }

        [DataMember]
        public DateTime? ActiveDeadlineDate { get; set; }
    }
}
