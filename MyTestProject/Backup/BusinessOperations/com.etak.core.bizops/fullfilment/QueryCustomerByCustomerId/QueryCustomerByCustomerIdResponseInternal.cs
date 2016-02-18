using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByCustomerId
{
    /// <summary>
    /// Gets a customer by it's customer in core model, this is almost implemented by the framework
    /// </summary>
    public class QueryCustomerByCustomerIdResponseInternal : ResponseBase, ICustomerBasedResponse,ISubscriptionBasedResponse
    {
        /// <summary>
        /// The customer that will be returned.
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }
        /// <summary>
        /// The Subscription of the customer that will be returned.
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
