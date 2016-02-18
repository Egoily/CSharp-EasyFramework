using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.creditlimit
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class EmergencyCreditApplyNotificationEvent : NotificationEvent
    {
        [DataMember]
        public virtual decimal EmergencyCredit { get; set; }
        [DataMember]
        public virtual decimal EmergencyCreditFee { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class EmergencyCreditApplySuccessNotificationEvent : NotificationEvent
    {
        [DataMember]
        public virtual decimal EmergencyCredit { get; set; }
        [DataMember]
        public virtual decimal EmergencyCreditFee { get; set; }  
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class EmergencyCreditApplyFailNotificationEvent : NotificationEvent
    {
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class EmergencyCreditLeftNotificationEvent : NotificationEvent
    {
        public virtual decimal EmergencyCreditLeft { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class EmergencyCreditUnPaidNotificationEvent : NotificationEvent
    {

    }
}
