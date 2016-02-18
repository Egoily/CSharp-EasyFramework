using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.creditlimit
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PostpaidCreditLimitIncreaseNotificationEvent : NotificationEvent
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal CreditLimit { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PostpaidCreditLimitDecreaseNotificationEvent : NotificationEvent
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal CreditLimit { get; set; }
    }
}
