using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum PromotionType
    {
        [EnumMember()] 
        Unknown = 0,
        [EnumMember()] 
        Promotion = 1,
        [EnumMember()]
        Discount = 2,
        [EnumMember()]
        Dayticket = 11,
        [EnumMember()]
        Monthticket = 12,
        [EnumMember()]
        BonusPromotion = 13,
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum PromotionRestrictUnit
    {
        [EnumMember()] 
        None = -1,
        [EnumMember()] 
        Hour = 0,
        [EnumMember()] 
        Day = 1
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum PromotionCategorys
    {
        [EnumMember()]
        Unknown = 0,
        [EnumMember()] 
        XFreeCallsToCheckBalance = 101,
        [EnumMember()] 
        FriendsFamily = 102,
        [EnumMember()] 
        BonusPointOffer = 103,
        [EnumMember()] 
        TopUpPresent = 104,
        [EnumMember()] 
        PeriodicRebate = 105,
        [EnumMember()] 
        DiscountOnRatePlan = 106,
        [EnumMember()] 
        BuyOneGetXFree = 107,
        [EnumMember()] 
        PreferentialWholesalePackage = 108,
        [EnumMember()] 
        GenericPromotion = 109,
        [EnumMember()] 
        Recommendation = 110,
        [EnumMember()] 
        InBundlePromotion = 111,
        [EnumMember()] 
        DoubleTriplePromotion = 112,
        [EnumMember()] 
        PromotionDiscount = 113,
    }

    //add by damon 2013-11-12
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PromotionPlan : LoadeableEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public virtual int PromotionPlanId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string PromotionPlanName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual int SubscriptionPeriod { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool Exclusive{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int Periodic {get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int Priority {get;set;}

        [DataMember(EmitDefaultValue = false)]
        public bool ResetLimit{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public decimal MonthFee{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public decimal SubscriptionFee {get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int SubscriptionPeriodUnit {get;set;}

        [DataMember(EmitDefaultValue = false)]
        public DateTime? StartDate {get;set;}

        [DataMember(EmitDefaultValue = false)]
        public DateTime? EndDate{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public PromotionCategorys PromotionCategoryId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int RemoveImmediatelyFlag {get;set;}

    
        //public IList<RmSpecificNumberGroupInfo> RmSpecificNumberGroupInfoList{get;set;}     

        [DataMember(EmitDefaultValue = false)]
        public int? ActiveWithoutCredit {get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int SelfCareVisible {get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int CustomerCareVisible {get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int APIVisible{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int Prorate {get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int ResetPeriod {get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int ResetPeriodUnit{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public decimal DiscountMonthFeeForRenew{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int TimePointForChargeFee{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int SmsNotification{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int NotificationSmsTemplateId{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int RenewAutomatically{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int DailyResetTime{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public PromotionType PromotionType{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public PromotionRestrictUnit RestrictUnit{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public int? RestrictDuration{get;set;}

        [DataMember(EmitDefaultValue = false)]
        public bool Accumulative{get;set;}
    }
}
