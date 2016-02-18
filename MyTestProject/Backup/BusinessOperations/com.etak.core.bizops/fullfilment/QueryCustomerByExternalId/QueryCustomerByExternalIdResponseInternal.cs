using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByExternalId
{
    /// <summary>
    /// Response Internal of QueryCustomerByExternalIdBizOp
    /// </summary>
    public class QueryCustomerByExternalIdResponseInternal : ResponseBase,ISubscriptionBasedResponse, ICustomerBasedResponse
    {
        /// <summary>
        /// IEnumerable(CustomerInfo) with certain external id
        /// </summary>
        public virtual IEnumerable<CustomerInfo> CustomerInfos { get; set; }

        /// <summary>
        /// Subscription of the first customer on the list
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
        /// <summary>
        /// First customer on the list
        /// </summary>
        public CustomerInfo Customer { get; set; }
    }
}
