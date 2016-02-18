using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.GetActiveProductsOfCustomer
{
    /// <summary>
    /// The internal response for GetActiveProducsOfCustomerBizOp
    /// </summary>
    public class GetActiveProductsOfCustomerResponseInternal : ResponseBase,ISubscriptionBasedResponse
    {
        /// <summary>
        /// All the active products for the customer, within a time
        /// </summary>
        public IEnumerable<CustomerProductAssignment> CustomerProductAssignments { get; set; }
        /// <summary>
        /// Subscription of the customer
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
