using System.Collections.Generic;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.bizops.fullfilment.CheckPurchaseProductForCustomer
{
    /// <summary>
    /// Response DTO of CheckPurchaseProductForCustomerResponseBizop
    /// </summary>
    public class CheckPurchaseProductForCustomerResponseDTO: ResponseBaseDTO,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// value that inform if credit limit is enough
        /// </summary>
        public bool IsCreditEnough { get; set; }

        /// <summary>
        /// value that inform if limit is reached
        /// </summary>
        public bool IsLimitReached { get; set; }

        /// <summary>
        /// If limit is reached set the amount required
        /// </summary>
        public decimal AmountRequired { get; set; }

        /// <summary>
        /// value that inform if the list is compatible with the current customer products
        /// </summary>
        public bool IsListCompatibleWithCustomerProducts { get; set; }

        /// <summary>
        /// value that informs if the list has its requirements satisfied with the customer products
        /// </summary>
        public bool AreListRequirementsSatisfiedForCustomer { get; set; }

        /// <summary>
        /// List of deprovision relations containing the products that should be substituted
        /// </summary>
        public List<ProductOfferingSpecificationOption> DeprovisionList { get; set; }
        /// <summary>
        /// SubscriptionDTO of the customer
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
