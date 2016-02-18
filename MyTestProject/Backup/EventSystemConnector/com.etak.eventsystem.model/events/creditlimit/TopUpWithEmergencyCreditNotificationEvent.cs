using System;

namespace com.etak.eventsystem.model.events.creditlimit
{
    public class TopUpWithEmergencyCreditNotificationEvent : NotificationEvent
    {
        public decimal CreditLimit { get; set; }
        public DateTime CreditExpireDate { get; set; }
        public decimal EmergencyCreditAndFee { get; set; }
    }
}
