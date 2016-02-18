using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.CancelCustomerAndSubscriptions
{
    /// <summary>
    /// Input response for CancelCustomerAndSubscriptions output parameters in CORE model 
    /// </summary>
    public class CancelCustomerAndSubscriptionsResponseInternal : CreateNewOrderResponse,ICustomerBasedResponse
    {
        /// <summary>
        /// Customer to be cancelled
        /// </summary>
        public CustomerInfo Customer { get; set; }
    }
}
