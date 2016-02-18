using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.QueryCustomerRecurringCharges
{
    /// <summary>
    /// QueryCustomerRecurringChargesResponseInternal of QueryCustomerRecurringChargesBizOp
    /// </summary>
    public class QueryCustomerRecurringChargesResponseInternal : ResponseBase, ISubscriptionBasedResponse
    {
        /// <summary>
        /// List CustomerCharge of Customer 
        /// </summary>
        public virtual IList<CustomerCharge> RecurringCharges { get; set; }
        /// <summary>
        /// Subscription of Customer 
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
