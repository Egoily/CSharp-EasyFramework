using System;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.fullfilment.CancelCustomerProduct
{
    /// <summary>
    /// Input request for CancelCustomerProduct input in CORE model
    /// </summary>
    public class CancelCustomerProductRequestInternal : CreateNewOrderRequest
    {
        /// <summary>
        /// The realtion between customer and product to be cancelled
        /// </summary>
        public CustomerProductAssignment CustomerProductAssignment { get; set; }

        /// <summary>
        /// Cancel date
        /// </summary>
        public DateTime CancelDate { get; set; }

        /// <summary>
        /// Next bill run date
        /// </summary>
        public DateTime NextBillRunDate { get; set; }

        /// <summary>
        /// Is it using nexbillcycle for enddate or cancelDate
        /// </summary>
        public bool UseNextBillCycleEndDateInRecurring { get; set; }

        /// <summary>
        /// Used for different behavior between PurchaseProduct and CancelCusotmerAndSubscription
        /// if PurchaseProduct:
        ///             we need to cancel ONLY the newest customer promotion with list of same promotionplandetail.
        ///             CancelCustomerPromotionsWithSamePromotionPlanDetail = false;
        /// if CancelCustomerAndSUbscription:
        ///             we need to cancel ALL the customer prmotions with list of same promotionplanDetail.
        ///             CancelCustomerPromotionsWithSamePromotionPlanDetail = true
        /// 
        /// DefaultValue to false
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        public bool CancelCustomerPromotionsWithSamePromotionPlanDetail { get; set; }
    }
}
