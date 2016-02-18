using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-9-16 16:54:46
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-9-16 16:54:46
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-9-16 16:54:46
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-9-16 16:54:46
    /// </summary>
    [DataContract]
    [Serializable]
    public class RmPromotionPlanInfo : PromotionModelBase
    {       
        public RmPromotionPlanInfo()
        {          
            CrmCustomersUnpaidFeeInfoList = new List<CrmCustomersUnpaidFeeInfo>();
            RmPromotionPlanDetailInfoList = new List<RmPromotionPlanDetailInfo>();
            RmPromotionPlanRuleInfoList = new List<RmPromotionPlanRuleInfo>();
            RmSpecificNumberGroupInfoList = new List<RmSpecificNumberGroupInfo>();
            ActiveWithoutCredit = 1;
        }
      
        public virtual int PromotionPlanId { get; set; }
        public virtual string PromotionPlanName { get; set; }
        public virtual int DealerId { get; set; }
        public virtual bool Exclusive { get; set; }
        public virtual int Periodic { get; set; }
        public virtual  int Priority { get; set; }
        public virtual  bool ResetLimit { get; set; }
        public virtual  decimal MonthFee { get; set; }
        public virtual  decimal SubscriptionFee { get; set; }
        public virtual  int SubscriptionPeriod { get; set; }
        public virtual  int SubscriptionPeriodUnit { get; set; }
        public virtual  DateTime? StartDate { get; set; }
        public virtual  DateTime? EndDate { get; set; }
        public virtual  int PromotionCategoryId { get; set; }
        public virtual  int RemoveImmediatelyFlag { get; set; }
        public virtual  IList<CrmCustomersUnpaidFeeInfo> CrmCustomersUnpaidFeeInfoList { get; set; }
        public virtual  IList<RmPromotionPlanDetailInfo> RmPromotionPlanDetailInfoList { get; set; }
        public virtual  IList<RmPromotionPlanRuleInfo> RmPromotionPlanRuleInfoList { get; set; }
        public virtual  IList<RmSpecificNumberGroupInfo> RmSpecificNumberGroupInfoList { get; set; }
        public virtual  int SelfCareVisible { get; set; }
        public virtual  int CustomerCareVisible { get; set; }
        public virtual  APIVisible APIVisible { get; set; }
        public virtual  int Prorate { get; set; }
        public virtual  int ResetPeriod { get; set; }
        public virtual  int ResetPeriodUnit { get; set; }
        public virtual  decimal DiscountMonthFeeForRenew { get; set; }
        public virtual  int TimePointForChargeFee { get; set; }
        public virtual  int SmsNotification { get; set; }
        public virtual  int NotificationSmsTemplateId { get; set; }
        public virtual  int RenewAutomatically { get; set; }
        public virtual  int DailyResetTime { get; set; }
        public virtual  PromotionType PromotionType { get; set; }
        public virtual  PromotionRestrictUnit RestrictUnit { get; set; }
        public virtual  int? RestrictDuration { get; set; }
        public virtual  bool Accumulative { get; set; }        
        public virtual int? PromotionGroupId { get; set; }      
        public virtual int? CrmMSISDNGroupTypeInfoID { get; set; }
        public virtual int? ActiveWithoutCredit { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            RmPromotionPlanInfo objBoxed = obj as RmPromotionPlanInfo;

            if (ReferenceEquals(null, objBoxed))
                return false;

            return (objBoxed.PromotionPlanId.Equals(this.PromotionPlanId));
        }

        public override int GetHashCode()
        {
            return PromotionPlanId.GetHashCode() + DealerId.GetHashCode();
        }


    }

    [DataContract]
    [Serializable]
    public class ExtendedFFCreditRule
    {
        /// <summary>
        /// how much it cost to recharge the FF credits.
        /// </summary>
        public decimal cost;
        /// <summary>
        /// how many credit to recharge the FF promotion limit into the customer's promotion record.
        /// </summary>
        public decimal credit;
        /// <summary>
        /// How many days to extend.
        /// </summary>
        public int extendedInDays;
    }

}
