using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByMsisdn
{
    /// <summary>
    /// Response Internal of QueryCustomerByMsisdn
    /// </summary>
    public class QueryCustomerByMsisdnResponseInternal : ResponseBase, ICustomerBasedResponse
    {
        /// <summary>
        /// Customer with given msisdn
        /// </summary>
        public CustomerInfo Customer { get; set; }
    }
}
