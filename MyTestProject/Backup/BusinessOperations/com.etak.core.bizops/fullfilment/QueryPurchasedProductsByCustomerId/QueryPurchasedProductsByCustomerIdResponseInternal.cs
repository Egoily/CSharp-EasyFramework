using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.fullfilment.QueryPurchasedProductsByCustomerId
{
    /// <summary>
    /// Internal response of QueryPurchasedProductsByCustomerId
    /// </summary>
    public class QueryPurchasedProductsByCustomerIdResponseInternal : ResponseBase,ISubscriptionBasedResponse
    {
       /// <summary>
       /// List of CustomerProductAssignment 
       /// </summary>
       public virtual IList<CustomerProductAssignment> Products { get; set; }
       /// <summary>
       /// Customer's Subscription
       /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
