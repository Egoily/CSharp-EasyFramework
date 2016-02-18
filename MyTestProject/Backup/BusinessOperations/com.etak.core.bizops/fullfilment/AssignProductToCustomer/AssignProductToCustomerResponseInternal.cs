using com.etak.core.model;
using com.etak.core.model.operation.contract.product;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.bizops.fullfilment.AssignProductToCustomer
{
    /// <summary>
    /// PurchaseIndividualProductForCustomerResponseInternal for Individual product purchase
    /// </summary>
    public class AssignProductOfferingToCustomerResponseInternal : CreateNewOrderResponse, IProductOfferingBasedResponse, IProductBasedResponse, ISubscriptionBasedResponse
    {
        /// <summary>
        /// productPurchased
        /// </summary>
        public virtual CustomerProductAssignment productPurchased { get; set; }

        /// <summary>
        /// IProductOffering based: product offering assigned
        /// </summary>
        public virtual ProductOffering ProductOffering { get; set; }

        /// <summary>
        /// ISUbscription based
        /// </summary>
        public virtual model.ResourceMBInfo Subscription { get; set; }

        /// <summary>
        /// IproductBasedResponse: The product assigned
        /// </summary>
        public Product Product { get; set; }
    }
}
