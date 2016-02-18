using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.customer
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PrepaidReferralActivedToReferralEvent : NotificationEvent
    {
        [DataMember]
        public decimal Threshold { get; set; }

        [DataMember]
        public string PromotionPlanName { get; set; }

        [DataMember]
        public string SponsorMsisdn { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PrepaidReferralActivedToSponsorEvent : NotificationEvent
    {
        [DataMember]
        public string PromotionPlanName { get; set; }

        [DataMember]
        public string ReferredCustomerName { get; set; }

        [DataMember]
        public string ReferralMsisdn { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PrepaidReferralTopUpEnoughToSponsorEvent : NotificationEvent
    {
        [DataMember]
        public string ReferralName { get; set; }

        [DataMember]
        public string ReferralMsisdn { get; set; }

        [DataMember]
        public string PromotionPlanName { get; set; }

        [DataMember]
        public decimal Threshold { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PrepaidReferralTopUpEnoughToReferralEvent : NotificationEvent
    {
        [DataMember]
        public decimal Threshold { get; set; }

        [DataMember]
        public string PromotionPlanName { get; set; }

        [DataMember]
        public string SponsorMsisdn { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PostpaidReferralActivedToReferralEvent : NotificationEvent
    {
        [DataMember]
        public decimal Threshold { get; set; }

        [DataMember]
        public string SponsorMsisdn { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PostpaidReferralActivedToSponsorEvent : NotificationEvent
    {
        [DataMember]
        public string ReferralName { get; set; }

        [DataMember]
        public string ReferralMsisdn { get; set; }

        [DataMember]
        public string PromotionPlanName { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PostpaidReferralConsumeEnoughToSponsorEvent : NotificationEvent
    {
        [DataMember]
        public string PromotionPlanName { get; set; }

        [DataMember]
        public string ReferralName { get; set; }

        [DataMember]
        public string ReferralMsisdn { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PostpaidReferralConsumeEnoughToReferralEvent : NotificationEvent
    {
        [DataMember]
        public decimal Threshold { get; set; }

        [DataMember]
        public string PromotionPlanName { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PostpaidReferralConsumeEnoughToPrepaidSponsorEvent : NotificationEvent
    {
        [DataMember]
        public string PromotionPlanName { get; set; }

        [DataMember]
        public string ReferralName { get; set; }

        [DataMember]
        public string ReferralMsisdn { get; set; }

        [DataMember]
        public decimal Threshold { get; set; }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PrepaidReferralTopUpEnoughToPostpaidSponsorEvent : NotificationEvent
    {
        [DataMember]
        public string ReferralName { get; set; }

        [DataMember]
        public string ReferralMsisdn { get; set; }

        [DataMember]
        public string PromotionPlanName { get; set; }

        [DataMember]
        public decimal Threshold { get; set; }
    }
}