using System;

namespace com.etak.core.model.subscription
{
    /// <summary>
    /// Customer DTO Promotion
    /// </summary>
    public class CustomerPromotionDTO
    {
        /// <summary>
        /// The ProductId of the promotion
        /// </summary>
        public Int32 ProductId { get; set; }
        /// <summary>
        /// Name of the product
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Promotion Plan Id
        /// </summary>
        public Int32 PromotionPlanId { get; set; }
        /// <summary>
        /// The Current Limit of the Promotion
        /// </summary>
        virtual public decimal CurrentLimit { get; set; }
        /// <summary>
        /// Determines if the Promotion is Active or not
        /// </summary>
        public virtual Boolean Active { get; set; }

    }
}
