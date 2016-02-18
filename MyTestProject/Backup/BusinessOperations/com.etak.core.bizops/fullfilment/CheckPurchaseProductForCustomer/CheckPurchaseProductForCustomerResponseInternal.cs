using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.bizops.fullfilment.CheckPurchaseProductForCustomer
{
    /// <summary>
    /// Response Internal of CheckPurchaseProductForCustomerResponseBizOp
    /// </summary>
    public class CheckPurchaseProductForCustomerResponseInternal: ResponseBase,ISubscriptionBasedResponse
    {
        /// <summary>
        /// value that inform if credit is enough
        /// </summary>
        public virtual bool IsCreditEnough { get; set; }

        /// <summary>
        /// value that inform if limit is reached
        /// </summary>
        public virtual bool IsLimitReached { get; set; }

        /// <summary>
        /// If limit is reached set the amount required
        /// </summary>
        public virtual decimal AmountRequired { get; set; }

        /// <summary>
        /// value that inform if the list is compatible with the current customer products
        /// </summary>
        public virtual bool IsListCompatibleWithCustomerProducts { get; set; }

        /// <summary>
        /// value that informs if the list has its requirements satisfied with the customer products
        /// </summary>
        public virtual bool AreListRequirementsSatisfiedForCustomer { get; set; }

        /// <summary>
        /// List of deprovision relations containing the products that should be substituted
        /// </summary>
        public virtual List<ProductOfferingSpecificationOption> DeprovisionList { get; set; }
        /// <summary>
        /// Subscription of the customer
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
