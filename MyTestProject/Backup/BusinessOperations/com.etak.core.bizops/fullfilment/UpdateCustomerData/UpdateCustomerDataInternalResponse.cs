using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.UpdateCustomerData
{
    /// <summary>
    /// Response after updating customer
    /// </summary>
    public class UpdateCustomerDataInternalResponse : CreateNewOrderResponse,ISubscriptionBasedResponse
    {
        /// <summary>
        /// CustomerInfo Object with updated customer information
        /// </summary>
        public virtual CustomerInfo UpdatedCustomer { get; set; }
        /// <summary>
        /// Subscription related to customer
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
