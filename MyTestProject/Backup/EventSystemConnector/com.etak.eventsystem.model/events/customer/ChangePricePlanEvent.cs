using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.customer
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PricePlanChangedNotificationEvent : NotificationEvent
    {
        [DataMember]
        public string NewPackage { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PostpaidToPostpaidNotificationEvent : PricePlanChangedNotificationEvent
    {
        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public decimal NextBill { get; set; }

        [DataMember]
        public decimal CreditLimit { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PrepaidToPostpaidNotificationEvent : PostpaidToPostpaidNotificationEvent
    {
        [DataMember]
        public decimal MonthFree { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PostpaidToPrepaidNotificationEvent : PricePlanChangedNotificationEvent
    {
        [DataMember]
        public string CustomerName { get; set; }
    }
}
