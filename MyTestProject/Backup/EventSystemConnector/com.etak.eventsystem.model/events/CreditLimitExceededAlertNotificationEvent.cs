using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events
{
    // add by Ben 2014-7-14
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class CreditLimitExceededNotificationEvent : NotificationEvent
    {
        [DataMember]
        public virtual decimal ExceededAmount { get; set; }

    }
}
