using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByCustomerId
{
    /// <summary>
    /// Request Internal of QueryCustomerByCustomerIdBizOp
    /// </summary>
    public class QueryCustomerByCustomerIdRequestInternal : RequestBase, ICustomerBasedRequest
    {
        /// <summary>
        /// Customer Info with certain Customer Id
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }
    }
}
