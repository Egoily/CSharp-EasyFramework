using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.CreateCustomer
{
    /// <summary>
    /// Response for Create Customer
    /// </summary>
    public class CreateCustomerResponseInternal : CreateNewOrderResponse, ICustomerBasedResponse,ISubscriptionBasedResponse
    {
        /// <summary>
        /// The CustomerInfo Created
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }
        /// <summary>
        /// The Subscription Created
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
