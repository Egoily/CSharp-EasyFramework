using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.QueryCustomerUnbilledCharges
{
    /// <summary>
    /// Response of QueryCustomerUnbilledCharges
    /// </summary>
    public class QueryCustomerUnbilledChargesResponseInternal : ResponseBase, ISubscriptionBasedResponse
    {
        /// <summary>
        /// IList of CustomerCharge that has been filtered
        /// </summary>
        public virtual IList<CustomerCharge> CustomerCharges { get; set; }
        /// <summary>
        /// subscription of the customer
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
