using System;
using System.Collections.Generic;

namespace com.etak.core.model.subscription
{
    /// <summary>
    /// DTO Object for the Plan Info entity
    /// </summary>
    public class RmPromotionPlanInfoDTO 
    {
        /// <summary>
        /// The promotion plan Id
        /// </summary>
        public virtual int PromotionPlanId { get; set; }
        /// <summary>
        /// The name of the promotion
        /// </summary>
        public virtual string PromotionPlanName { get; set; }
        /// <summary>
        /// Dealer id of the promotion
        /// </summary>
        public virtual int DealerId { get; set; }
        /// <summary>
        /// Flag to know if the promotion plan is exclusive
        /// </summary>
        public virtual bool Exclusive { get; set; }
        /// <summary>
        /// Promotion Plan periodic
        /// </summary>
        public virtual int Periodic { get; set; }
        /// <summary>
        /// Promotion Plan's priority
        /// </summary>
        public virtual int Priority { get; set; }
        /// <summary>
        /// Reset Limit
        /// </summary>
        public virtual bool ResetLimit { get; set; }
        /// <summary>
        /// Month Fee of the promotion
        /// </summary>
        public virtual decimal MonthFee { get; set; }
        /// <summary>
        /// Subscription Fee of the promotion
        /// </summary>
        public virtual decimal SubscriptionFee { get; set; }
        /// <summary>
        /// Subscription Period of the promotion
        /// </summary>
        public virtual int SubscriptionPeriod { get; set; }
        /// <summary>
        /// Subscription Period Unit of the promotion
        /// </summary>
        public virtual int SubscriptionPeriodUnit { get; set; }
        /// <summary>
        /// Date to know when the promotion starts
        /// </summary>
        public virtual DateTime? StartDate { get; set; }
        /// <summary>
        /// Date to know when the promotion finishes
        /// </summary>
        public virtual DateTime? EndDate { get; set; }
        /// <summary>
        /// The category of the Promotion
        /// </summary>
        public virtual int PromotionCategoryId { get; set; }
        /// <summary>
        /// Remove Immediately Flag
        /// </summary>
        public virtual int RemoveImmediatelyFlag { get; set; }
      
        /// <summary>
        /// A list of DTO objects with plan detail info
        /// </summary>
        public virtual IList<RmPromotionPlanDetailInfoDTO> RmPromotionPlanDetailInfoList { get; set; }
        /// <summary>
        /// To know if the Promotion is visible in Selfcare
        /// </summary>
        public virtual int SelfCareVisible { get; set; }
        /// <summary>
        /// To know if the Promotion is visible in CustomerCare
        /// </summary>
        public virtual int CustomerCareVisible { get; set; }
        /// <summary>
        /// To know if it's visible
        /// </summary>
        public virtual APIVisible APIVisible { get; set; }
        /// <summary>
        /// Prorate unit of the promotion
        /// </summary>
        public virtual int Prorate { get; set; }
        /// <summary>
        /// Period to reset the promotion
        /// </summary>
        public virtual int ResetPeriod { get; set; }
        /// <summary>
        /// Unit period for the reset time
        /// </summary>
        public virtual int ResetPeriodUnit { get; set; }
        /// <summary>
        /// Discount Month Fee For Renew
        /// </summary>
        public virtual decimal DiscountMonthFeeForRenew { get; set; }
        /// <summary>
        /// Time Point For Charge Fee
        /// </summary>
        public virtual int TimePointForChargeFee { get; set; }
        /// <summary>
        /// Sms notification of the Promotion
        /// </summary>
        public virtual int SmsNotification { get; set; }
        /// <summary>
        /// The Template Id of the Notification
        /// </summary>
        public virtual int NotificationSmsTemplateId { get; set; }
        /// <summary>
        /// Renew Automatically flag
        /// </summary>
        public virtual int RenewAutomatically { get; set; }
        /// <summary>
        /// Reset daily
        /// </summary>
        public virtual int DailyResetTime { get; set; }
        /// <summary>
        /// The type of the Promotion
        /// </summary>
        public virtual PromotionType PromotionType { get; set; }
        /// <summary>
        /// Promotion Restrict Unit
        /// </summary>
        public virtual PromotionRestrictUnit RestrictUnit { get; set; }
        /// <summary>
        /// The duration of the restriction
        /// </summary>
        public virtual int? RestrictDuration { get; set; }
        /// <summary>
        /// Accumulative flag of the Promotion
        /// </summary>
        public virtual bool Accumulative { get; set; }
        /// <summary>
        /// Promotion Group Id of the Promotion
        /// </summary>
        public virtual int? PromotionGroupId { get; set; }
        /// <summary>
        /// CrmMSISDNGroup Type Info ID
        /// </summary>
        public virtual int? CrmMSISDNGroupTypeInfoID { get; set; }
        /// <summary>
        /// To know if the promotion is Active without Credit
        /// </summary>
        public virtual int? ActiveWithoutCredit { get; set; }
    }
}
