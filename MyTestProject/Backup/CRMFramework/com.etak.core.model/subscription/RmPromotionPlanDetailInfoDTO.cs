using System;

namespace com.etak.core.model.subscription
{
    /// <summary>
    /// DTO Object corresponding to the Promotion Catalog
    /// </summary>
    public class RmPromotionPlanDetailInfoDTO
    { 
        /// <summary>
        /// Promotion Plan Detail Id
        /// </summary>
        virtual public int PromotionPlanDetailId {get;set;}
        /// <summary>
        /// DTO Object corresponding with the Plan Info parent
        /// </summary>
        virtual public RmPromotionPlanInfoDTO RmPromotionPlanInfo{get;set;}

        /// <summary>
        /// The type of the Promotion
        /// </summary>
        virtual public int PromotionTypeId{get;set;}

        /// <summary>
        /// The Type Id of the Service
        /// </summary>
        virtual public int ServiceTypeId{get;set;}

        /// <summary>
        /// The Subtype Id of the Service
        /// </summary>
        virtual public int SubServiceTypeId{get;set;}

        /// <summary>
        /// The category Id of the number
        /// </summary>
        virtual public int NumberCategoryId{get;set;}
        /// <summary>
        /// The Date Category
        /// </summary>
        virtual public int DateCategoryId{get;set;}
        /// <summary>
        /// Country Code
        /// </summary>
        virtual public string CountryCode{get;set;}

        /// <summary>
        /// Call Direction Id
        /// </summary>
        virtual public int CallDirectionId{get;set;}

        /// <summary>
        /// Limit per call of the Promotion
        /// </summary>
        virtual public decimal LimitPerCall{get;set;}

        /// <summary>
        /// Limit per Day 
        /// </summary>
        virtual public decimal LimitPerDay{get;set;}
        /// <summary>
        /// Limit of the Promotion
        /// </summary>
        virtual public decimal Limit{get;set;}
        /// <summary>
        /// Which is the Unit of the Limit
        /// </summary>
        virtual public int LimitUnit{get;set;}
        /// <summary>
        /// The currency Id
        /// </summary>
        virtual public int CurrencyId{get;set;}
        /// <summary>
        /// Method Id of the Promotion
        /// </summary>
        virtual public int PromotionMethodId{get;set;}
        /// <summary>
        /// The Setup
        /// </summary>
        virtual public decimal Setup{get;set;}
        /// <summary>
        /// Prompt
        /// </summary>
        virtual public decimal Prompt{get;set;}
        /// <summary>
        /// Tariff number 1
        /// </summary>
        virtual public decimal Tariff1{get;set;}
        /// <summary>
        /// Tariff number 2
        /// </summary>
        virtual public decimal Tariff2{get;set;}
        /// <summary>
        /// Unit Category Id
        /// </summary>
        virtual public int UnitCategoryId{get;set;}
        /// <summary>
        /// Discount Method Id
        /// </summary>
        virtual public int DiscountMethodId{get;set;}
        /// <summary>
        /// Start Date of the Promotion
        /// </summary>
        virtual public DateTime? StartDate {get;set;}
        /// <summary>
        /// End Date of the Promotion
        /// </summary>
        virtual public DateTime? EndDate {get;set;}
        /// <summary>
        /// Date Category Type Id
        /// </summary>
        virtual public int DateCategoryTypeId {get;set;}
        /// <summary>
        /// WhiteList
        /// </summary>
        virtual public string WhiteList {get;set;}
        /// <summary>
        /// Usage Fee
        /// </summary>
        virtual public decimal UsageFee {get;set;}
        /// <summary>
        /// Apply On Roaming
        /// </summary>
        virtual public int ApplyOnRoaming {get;set;}
        /// <summary>
        /// Apply on Super on Net
        /// </summary>
        virtual public bool ApplyOnSuperOnNet {get;set;}
        /// <summary>
        /// Rate Plan Id
        /// </summary>
        virtual public int? RatePlanId {get;set;}
        /// <summary>
        /// Base Promotion Plan Detail Id
        /// </summary>
        virtual public int? BasePromotionPlanDetailId {get;set;}
        /// <summary>
        /// Name of the promotion plan detail
        /// </summary>
        virtual public string PromotionPlanDetailName {get;set;}
        /// <summary>
        /// Delete flag
        /// </summary>
        virtual public bool DeleteFlag {get;set;}
        /// <summary>
        /// Over Limit Rate Plan Id
        /// </summary>
        virtual public int OverLimitRateplanId {get;set;}
        /// <summary>
        /// Black List Id
        /// </summary>
        virtual public int BlackListId {get;set;}
       /// <summary>
       /// Maximum allowed Balance
       /// </summary>
        virtual public decimal? MaximumAllowedBalance {get;set;}
        /// <summary>
        /// Wallet type Id
        /// </summary>
        virtual public Int32? WalletTypeId { get; set; }
       

  
    }
}
