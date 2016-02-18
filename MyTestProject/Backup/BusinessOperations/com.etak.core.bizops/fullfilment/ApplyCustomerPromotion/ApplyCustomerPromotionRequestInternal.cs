using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.ApplyCustomerPromotion
{
    /// <summary>
    /// Request Internal for Apply customer Promotion BizOp
    /// </summary>
    public class ApplyCustomerPromotionRequestInternal : CreateNewOrderRequest, ICustomerBasedRequest
    {
        /// <summary>
        /// Customer Info to apply the Promotion
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary>
        /// PromotionPlanIds to be applied
        /// </summary>
        public IList<RmPromotionPlanInfo> PromotionPlans { get; set; }

        /// <summary>
        /// Factor to be applied to the Credit Limit (if it's set)
        /// </summary>
        public decimal? FactorToCreditLimit { get; set; }

        /// <summary>
        /// Start Date of the Promotion
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Promotion's end date
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
