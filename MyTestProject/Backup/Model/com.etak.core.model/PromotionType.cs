using System;
using System.Reflection;

namespace com.etak.core.model
{
    [Serializable]
    public class StringValue : Attribute
    {
        private readonly string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }

    [Serializable]
    public static class StringEnum
    {
        public static string GetStringValue(Enum value)
        {
            string output = null;
            var type = value.GetType();
            var fi = type.GetField(value.ToString());
            var attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            if (attrs != null && attrs.Length > 0)
                output = attrs[0].Value;
            return output;
        }
    }

    /// <summary>
    /// 未结费用的状态
    /// </summary>
    public enum UnpaidStatus
    {
        Pending = 0,
        Charged = 1,
        Expired = 2
    }
    /// <summary>
    /// promotion的类型
    /// 将所有的promotion进行分类，分类的目的是分别提供编辑界面
    /// 通过rm_promotionplan.promotioncategoryid来区分加载界面
    /// 该枚举将随着promotion种类的增加而增加，
    /// 同时该数据也会存放在dealer settings中，编号数据要保持一致
    /// Bright,2009/05/25
    /// </summary>
    public enum PromotionCategorys
    {
        XFreeCallsToCheckBalance = 101,
        FriendsFamily = 102,
        BonusPointOffer = 103,
        TopUpPresent = 104,
        PeriodicRebate = 105,
        DiscountOnRatePlan = 106,
        BuyOneGetXFree = 107,
        PreferentialWholesalePackage = 108,
        GenericPromotion = 109,
        Recommendation = 110,
        InBundlePromotion = 111,
        DoubleTriplePromotion = 112,
        PromotionDiscount = 113,
        SuperTopUp = 114  //added by neil at 2013/12/11
    }

    //Mahome, for promotion 2009-7-28
    public enum PromotionActivationType
    {
        NearRealtimeScan = 1,
        BusinessMonthlyAnalysis = 2,
        //UsageMonthlyAnalysis = 3,
        Realtime = 3
    }

    public enum PromotionBusinessType
    {
        TopUp = 1,
        SignUp = 2,
        PortIn = 3,
        MigrateToPostpaid = 4,
        MonthlyQuotaCharge = 5,
        SignUpRecommendation = 6,
        PackageSignUp = 7,
        Usage = 8,
        PortInRecommendation = 9
    }

    public enum PromotionCalculateType
    {
        Sum = 1,
        count = 2,
        CountConsecutiveMonth = 3
    }

    public enum PromotionCalculateUnit
    {
        Minutes = 1,
        Money = 2,
        Times = 3,
        Data = 4,
        Piece = 5
    }

    public enum TopUpPresentRule
    {
        PrepayFirstTopUp = 1,
        ConsecutiveMonths = 2,
        AnyTopUp = 3,
        MonthlyGrossPresent = 4
    }

    public enum PromotionReturnMethod
    {
        Fee = 1,
        Service = 2,
        Bonus = 3,
        Equipment = 4
    }

    public enum PromotionCycleType
    {
        Once = 1,
        Monthly = 2
    }

    public enum PromotionRebateValueUnit
    {
        Fixed = 1,
        Percentage = 2
    }

    public enum PromotionApplyStatus
    {
        Init = 1,
        InUsed = 2,
        Frozen = 3,
        Expired = 4
    }

    public enum PromotionRuleScanStatus
    {
        Idle = 1,
        Processing = 2
    }

    public enum PromotionRuleValidTime
    {
        ValidAtOnce = 1,
        ValidNextBillingCycle = 2
    }

    //public enum ChargeType
    //{
    //    PrepayToPostpayTransfer = 1,
    //    PromotionRebate = 2,
    //    PromotionDeduct = 3,
    //    Unknow = 99
    //}


    public enum PromotionLimitUnit
    {
        Money = 1,
        Minute = 2,
        Piece = 3,
        KBytes = 4,
        Second = 5,
        Byte = 6,
        Mbyte = 7,
        Gbtye = 8

    }

    public enum PromotionFeeType
    {
        InitialFee = 1,
        ChangeFee = 2,
        MonthlyFee = 3,
        BundleMonthlyFee = 4,
        BundleSubscriptionFee = 5,
    }

    public enum PromotionSubscriptUnit
    {
        Day = 1,
        Week = 2,
        Month = 3,
        Year = 4
    }
    //Mahome 2009-08-23
    public enum PromotionDateCategoryType
    {
        DayOfMonth = 1,
        DayOfWeek = 2
    }

    public enum IntroducerStatus
    {
        Init = 1,
        InUsed = 2
    }

    //for promotion James.Zhan 2009-08-26
    public enum PromotionPeriodic
    {
        NonPeriodic = 0,
        Periodic = 1
        //Monthly = 1,
        //Daily = 2
    }

    /// <summary>
    /// Promotion Reset Periodic Unit
    /// Jeffrey 2011-04-21
    /// </summary>
    public enum PromotionResetPeriodicUnit
    {
        Minute = 1,
        Day = 2,
        Week = 3,
        Month = 4,
        Year = 5
    }

    public enum PromotionDiscountMethodId
    {
        Percentage = 1,
        FixedValue = 2
    }

    public enum DiscountMethod
    {
        Percentage = 1,
        Fixed = 2
    }
    public enum SpecificNumberCategory
    {
        [StringValue("FF")]
        FriendFamilyNumber = 1,
        [StringValue("CN")]
        CompanyNumber = 2,
        [StringValue("PN")]
        PortedInNumber = 3,
        [StringValue("FN")]
        FreeNumber = 4
    }

    public static class SpecificNumberCategoryString
    {
        public static SpecificNumberCategory Get(string category)
        {
            switch (category)
            {
                case "CN":
                    return SpecificNumberCategory.CompanyNumber;
                case "PN":
                    return SpecificNumberCategory.PortedInNumber;
                case "FN":
                    return SpecificNumberCategory.FreeNumber;
                case "FF":
                    return SpecificNumberCategory.FriendFamilyNumber;
                default:
                    return SpecificNumberCategory.FriendFamilyNumber;
            }
        }

        public static string GetStringValue(SpecificNumberCategory enumeration)
        {
            Type type = enumeration.GetType();
            FieldInfo fieldInfo = type.GetField(enumeration.ToString());
            StringValue[] attribs = fieldInfo.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            return attribs.Length > 0 ? attribs[0].Value : enumeration.ToString();
        }
    }

    public enum CallDirection
    {
        In = 3001,
        Out = 3000
    }

    public enum PromotionMethod
    {
        // VoiceGenericPromotion = 1,
        //SmsGenericPromotion = 2,
        //MmsGenericPromotion = 3,
        //DataGenericPromotion = 4,
        //VoiceSessionBasedDiscount = 5,
        //VoiceSessionBasedPromotion = 6,
        //VoiceFreeCallsPromotion = 7,
        //FriendAndFamily = 8,
        //InbundlePromotion = 9,
        //PromotionalDiscount = 10,
        //VoiceDoublerPromotion = 11,
        //VoiceTrippl
        // VoiceDoublerPromotion = 11,
        //VoiceTripplerPromotion = 12,
        //SmsDoublerPromotion = 13,
        //SmsTripplerPromotion = 14
        PaySetup = 11,
        PayFirstSMS = 12,
        PayFirstMinutes = 13,
        Pay1SMS_SpeechTo60SMSPerDay = 14,
        FreeFirstMinutes = 15,
        //2010-02-22, DRE change database
        VoiceGenericPromotion = 1,
        SmsGenericPromotion = 2,
        MmsGenericPromotion = 3,
        DataGenericPromotion = 4,
        VoiceSessionBasedDiscount = 5,
        VoiceSessionBasedPromotion = 6,
        VoiceFreeCallsPromotion = 7,
        FriendAndFamily = 8,

        InBundlePromotion = 9,
        PromotionalDiscount = 10,
        VoiceDoublerPromotion = 11,
        VoiceTripplerPromotion = 12,
        SmsDoublerPromotion = 13,
        SmsTripplerPromotion = 14,
        MonthTicket = 15,
        DayTicket = 16,
    }
    public enum PromotionType
    {
        Promotion = 1,
        Discount = 2,
        Dayticket = 11,
        Monthticket = 12,
        BonusPromotion = 13,
        //added by damon 2013-09-13
        DailyResetPromotion = 14,
        //added by sam 2014-06-10
        ConvenienceBundles = 15,
        //added by benny 2014-08-27
        DeviceSpecific = 16,
        //added by winson 2014-09-26
        OneTimePromotion = 17
    }

    public enum DateCategory
    {
        All = 2,
        SpecificDate = 1
    }

    public enum NumberCategory
    {
        CustomerSpecificNumber = 1,
        SpecificTimesNumber = 2,
        SpecificNumber = 3,
        OnNet = 4,
        SuperOnNet = 5,
        NationalFixed = 6,
        NationalMobile = 7,
        International = 8,
        All = 0
    }

    public enum DeActiveReason
    {
        Deleted = 0,
        UnpaidFee = 1
    }

    public enum PromotionRestrictUnit
    {
        None = -1,
        Hour = 0,
        Day = 1
    }

}
