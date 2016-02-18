
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.FreezeCustomer
{
    /// <summary>
    /// Input response for FreezeCustomer output parameters in CORE model 
    /// </summary>
    public class FreezeCustomerResponseInternal : CreateNewOrderResponse, ICustomerBasedResponse
    {
        /// <summary>
        /// CustomerBased Response
        /// </summary>
        public model.CustomerInfo Customer { get; set; }
    }
}
