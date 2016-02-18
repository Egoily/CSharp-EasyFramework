using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.bizops.fullfilment.PurchaseProductForCustomer
{
    /// <summary>
    /// PurchaseProductForCustomerResponseInternal
    /// </summary>
    public class PurchaseProductForCustomerResponseInternal: CreateNewOrderResponse,ISubscriptionBasedResponse
    {
        /// <summary>
        /// productPurchaseList
        /// </summary>
        public virtual List<CustomerProductAssignment> productPurchaseList { get; set; }

        /// <summary>
        /// List of deprovisoned products
        /// </summary>
        public virtual List<ProductOffering> DeprovisionedProductList { get; set; }
        /// <summary>
        /// Subscription of the purchaser
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
