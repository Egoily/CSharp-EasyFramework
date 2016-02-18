
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.UnFreezeCustomer
{
    /// <summary>
    /// Input response for UnFreezeCustomer output parameters in CORE model 
    /// </summary>
    public class UnFreezeCustomerResponseInternal : CreateNewOrderResponse, ICustomerBasedResponse
    {
        /// <summary>
        /// ICustoemrBased
        /// </summary>
        public model.CustomerInfo Customer { get; set; }
    }
}
