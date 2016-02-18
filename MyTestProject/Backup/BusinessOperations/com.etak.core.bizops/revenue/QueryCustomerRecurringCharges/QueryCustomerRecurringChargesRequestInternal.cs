using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.QueryCustomerRecurringCharges
{
    /// <summary>
    /// QueryCustomerRecurringChargesRequestInternal of QueryCustomerRecurringChargesBizOp
    /// </summary>
    public class QueryCustomerRecurringChargesRequestInternal : RequestBase, ICustomerBasedRequest
    {
        /// <summary>
        /// CustomerInfo entity to get data customer
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }
        /// <summary>
        /// DealerInfo entity to store Dealer info in BizOp
        /// </summary>
        public virtual DealerInfo DealerInfo { get; set; }
        /// <summary>
        /// Enum CustomerCharge to store CustomerCHarge info
        /// </summary>
        public virtual IList<CustomerCharge> CustomerChargeInfo { get; set; }
        
    }
}
