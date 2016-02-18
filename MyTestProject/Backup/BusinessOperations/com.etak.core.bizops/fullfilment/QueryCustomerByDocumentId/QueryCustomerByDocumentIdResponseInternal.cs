using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByDocumentId
{
    /// <summary>
    /// Response Internal of QueryCustomerByDocumentIdBizOp
    /// </summary>
    public class QueryCustomerByDocumentIdResponseInternal : ResponseBase,ICustomerBasedResponse,ISubscriptionBasedResponse
    {
        /// <summary>
        /// Customer Info with certain document id
        /// </summary>
        public virtual IEnumerable<CustomerInfo> Customers { get; set; }
        /// <summary>
        /// Customer Info of the first customer in the list
        /// </summary>
        public CustomerInfo Customer { get; set; }
        /// <summary>
        /// Resource Info of the first customer in the list
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
