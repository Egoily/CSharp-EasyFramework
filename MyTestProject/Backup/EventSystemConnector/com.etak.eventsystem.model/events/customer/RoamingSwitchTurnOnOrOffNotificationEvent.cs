using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;


namespace com.etak.eventsystem.model.events.customer
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class RoamingSwitchTurnOnNotificationEvent : NotificationEvent
    {
        public decimal CreditLimit { get; set; }
        public string CustomerName { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class RoamingSwitchTurnOffNotificationEvent : NotificationEvent
    {
        public string CustomerName { get; set; }
    }
}
