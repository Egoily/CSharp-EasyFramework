using System;
using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.promotion
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class AxiomSocialMediaBundleApplyEvent : StrongTypedEvent
    {
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public PromotionPlan PromotionPlan { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SocialMediaBundleEnabledInXDaysEvent : StrongTypedEvent
    {
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public PromotionPlan PromotionPlan { get; set; }
        [DataMember]
        public int Days { get; set; }
        [DataMember]
        public decimal PromotionThreshold { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SocialMediaBundleDisabledAfterYDaysEvent : StrongTypedEvent
    {
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public PromotionPlan PromotionPlan { get; set; }
        [DataMember]
        public int Days { get; set; }
        [DataMember]
        public decimal PromotionThreshold { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SocialMediaBundleDisabledEvent : StrongTypedEvent
    {
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public PromotionPlan PromotionPlan { get; set; }
        [DataMember]
        public DateTime PromotionStartDate { get; set; }
        [DataMember]
        public decimal PromotionThreshold { get; set; }
        [DataMember]
        public Package Package { get; set; }
    }
}
