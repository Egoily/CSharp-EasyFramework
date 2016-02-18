using com.etak.eventsystem.model.dto;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.events.promotion
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PromotionXDayResetNotificationEvent : NotificationEvent
    {
        [DataMember]
        public int Day { get; set; }

        [DataMember]
        public string PromotionPlanName { get; set; }

        [DataMember]
        public decimal PromotionPlanFee { get; set; }

        public string CustomerName
        {
            get
            {
                if (ToCustomer == null)
                    return string.Empty;

                const string format = "{0} {1} {2}";
                return string.Format(format, ToCustomer.Initials, ToCustomer.MiddleName, ToCustomer.LastName);
            }
        }

        [DataMember]
        public decimal CreditLimit { get; set; }
    }
}