using com.etak.eventsystem.model.dto;
using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.events.promotion
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class AxiomSocialMediaBundleApplyNotificationEvent : NotificationEvent
    {
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public int PromotionPlanId { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SocialMediaBundleEnabledInXDaysNotificationEvent : NotificationEvent
    {
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public int Days { get; set; }
        [DataMember]
        public int PromotionPlanId { get; set; }
        [DataMember]
        public decimal PromotionThreshold { get; set; }
        [DataMember]
        public DateTime EndDay { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SocialMediaBundleDisabledAfterYDaysNotificationEvent : NotificationEvent
    {
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public int Days { get; set; }
        [DataMember]
        public int PromotionPlanId { get; set; }
        [DataMember]
        public decimal LeftTopupAmount { get; set; }
        [DataMember]
        public DateTime EndDay { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SocialMediaBundleDisabledNotificationEvent : NotificationEvent
    {
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public int PromotionPlanId { get; set; }
        [DataMember]
        public decimal PromotionThreshold { get; set; }
    }
}
