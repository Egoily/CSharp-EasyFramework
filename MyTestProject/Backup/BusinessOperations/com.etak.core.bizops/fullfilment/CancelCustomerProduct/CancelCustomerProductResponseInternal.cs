using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.product;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.bizops.fullfilment.CancelCustomerProduct
{
    /// <summary>
    /// Input response for CancelCustomerProduct output parameters in CORE model 
    /// </summary>
    public class CancelCustomerProductResponseInternal : CreateNewOrderResponse,ICustomerBasedResponse,ISubscriptionBasedResponse, IProductOfferingBasedResponse
    {
        /// <summary>
        /// Customer owning the product
        /// </summary>
        public CustomerInfo Customer { get; set; }
        /// <summary>
        /// Subscription owning the product
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
        /// <summary>
        /// Product cancelled
        /// </summary>
        public ProductOffering ProductOffering { get; set; }
    }
}
