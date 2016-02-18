using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.fullfilment.QueryCustomerProduct
{
    /// <summary>
    /// QueryCustomerProductResponseInternal used in QueryCustomerProductBizOp
    /// </summary>
    public class QueryCustomerProductResponseInternal : ResponseBase,ISubscriptionBasedResponse
    {
        /// <summary>
        /// Enumerable of CustomerProductAssignment entities
        /// </summary>
        public virtual IList<CustomerProductAssignment> CustomerProductAssignments { get; set; }
        /// <summary>
        /// Subscription of the customer
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
