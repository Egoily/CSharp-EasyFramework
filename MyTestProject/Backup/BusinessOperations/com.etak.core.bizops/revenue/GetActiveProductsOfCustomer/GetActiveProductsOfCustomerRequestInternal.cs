using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.operation.contract.customer;

namespace com.etak.core.bizops.revenue.GetActiveProductsOfCustomer
{
    /// <summary>
    /// The internal request for GetActiveProducsOfCustomerBizOp
    /// </summary>
    public class GetActiveProductsOfCustomerRequestInternal : RequestBase, ICustomerBasedRequest
    {
        /// <summary>
        /// Customer id
        /// </summary>
        public CustomerInfo Customer { get; set; }
    }
}
