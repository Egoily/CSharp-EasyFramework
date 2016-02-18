using com.etak.eventsystem.model.dto;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.events.customer
{
    
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class LanguageChangedNotificationEvent : NotificationEvent
    {
    }

    //added by damon 2014-07-04 for select language
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SelectLanguageNotificationEvent : NotificationEvent
    {
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class ConfirmLanguageNotificationEvent : NotificationEvent
    {
        [DataMember]
        public string Language { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SelectOtherLanguageNotificationEvent : NotificationEvent
    {
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class WithoutSelectLanguageNotificationEvent : NotificationEvent
    {
    }
}
