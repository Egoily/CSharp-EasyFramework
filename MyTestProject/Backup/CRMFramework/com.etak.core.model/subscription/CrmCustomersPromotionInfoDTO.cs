using System;

namespace com.etak.core.model.subscription
{
    /// <summary>
    /// CRMCustomersPromotionInfo DTO object
    /// </summary>
    public class CrmCustomersPromotionInfoDTO 
    {
        /// <summary>
        /// The promotion ID
        /// </summary>
        virtual public long PromotionId { get; set; }

        /// <summary>
        /// WhiteList
        /// </summary>
        virtual public string WhiteList { get; set; }
        /// <summary>
        /// Customer Id of the promotion
        /// </summary>
        virtual public int CustomerId { get; set; }
        /// <summary>
        /// Is base promotion flag
        /// </summary>
        virtual public bool IsBasePromotion { get; set; }
        /// <summary>
        /// Current Limit of the Promotion
        /// </summary>
        virtual public decimal CurrentLimit { get; set; }
        /// <summary>
        /// Active flag to know if the Promotion is Active
        /// </summary>
        virtual public bool Active { get; set; }
        /// <summary>
        /// When the Promotion is started
        /// </summary>
        virtual public DateTime? StartDate { get; set; }
        /// <summary>
        /// When the promotion is finished
        /// </summary>
        virtual public DateTime? EndDate { get; set; }
        /// <summary>
        /// When has been used the promotion for the first time
        /// </summary>
        virtual public DateTime? FirstUsed { get; set; }
        /// <summary>
        /// Renewal Count
        /// </summary>
        virtual public int RenewalCount { get; set; }
        /// <summary>
        /// Renewed Automatically
        /// </summary>
        virtual public int RenewAutomatically { get; set; }
        /// <summary>
        /// DeActive Reason
        /// </summary>
        virtual public int DeActiveReason { get; set; }
        /// <summary>
        /// Batch number
        /// </summary>
        virtual public string BatchNo { get; set; }
        /// <summary>
        /// Batch Id
        /// </summary>
        virtual public long BatchId { get; set; }
        /// <summary>
        /// When will be renewed
        /// </summary>
        virtual public DateTime? RenewDate { get; set; }
        /// <summary>
        /// The priority of the Promotion
        /// </summary>
        virtual public int? Priority { get; set; }
        /// <summary>
        /// Active without Credit
        /// </summary>
        virtual public int? ActiveWithoutCredit { get; set; }
        /// <summary>
        /// Retry Times of trying to renew Customer promotion
        /// </summary>
        virtual public int? RetryTimes { get; set; }
        /// <summary>
        /// DTO Object corresponding to the Promotion Detail
        /// </summary>
        public virtual RmPromotionPlanDetailInfoDTO PromotionDetail { get; set; }
      
    }
}
