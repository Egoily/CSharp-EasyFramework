using System;
using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.promotion
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PrepaidPromotionActivatedEvent : NotificationEvent
    {
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
        public string PromotionPlanName { get; set; }

        [DataMember]
        public DateTime PromotionPlanValidityDate { get; set; }

        [DataMember]
        public string PromotionPlanPrice { get; set; }

        [DataMember]
        public string CreditBalance { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class DataOnlyPromotionActivatedEvent : NotificationEvent
    {
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
        public string PromotionPlanName { get; set; }

        [DataMember]
        public DateTime PromotionPlanValidityDate { get; set; }

        [DataMember]
        public string PromotionPlanPrice { get; set; }

        [DataMember]
        public string CreditBalance { get; set; }

        [DataMember]
        public string TotalData { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PostPaidPromotionActivatedEvent : NotificationEvent
    {
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
        public string PromotionPlanName { get; set; }

        [DataMember]
        public DateTime PromotionPlanValidityDate { get; set; }

        [DataMember]
        public string PromotionPlanPrice { get; set; }

        [DataMember]
        public string CreditBalance { get; set; }

        [DataMember]
        public DateTime FirstDayOfNextMonth { get; set; }
    }
}