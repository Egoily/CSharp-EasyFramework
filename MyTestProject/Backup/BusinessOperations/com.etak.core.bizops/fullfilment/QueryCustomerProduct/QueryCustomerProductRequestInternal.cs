using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerProduct
{
    /// <summary>
    /// QueryCustomerProductRequestInternal  internal request 
    /// </summary>
    public class QueryCustomerProductRequestInternal  : RequestBase, ICustomerBasedRequest
    {
        /// <summary>
        /// Customer
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }
    }
    
}
