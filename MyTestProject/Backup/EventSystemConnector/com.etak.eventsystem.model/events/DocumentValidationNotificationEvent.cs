
using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SubmitDocumentInWDaysNotificationEvent : NotificationEvent
    {
        [DataMember]
        public int Days { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SubmitDocumentInXDaysNotificationEvent : NotificationEvent
    {
        [DataMember]
        public int Days { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SubmitDocumentInYDaysNotificationEvent : NotificationEvent
    {
        [DataMember]
        public int Days { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SubmitDocumentInZDaysNotificationEvent : NotificationEvent
    {
        [DataMember]
        public int Days { get; set; }
    }
}
