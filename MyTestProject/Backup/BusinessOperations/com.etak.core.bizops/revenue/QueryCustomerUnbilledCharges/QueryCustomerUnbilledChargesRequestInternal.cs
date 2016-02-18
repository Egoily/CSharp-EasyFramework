using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.QueryCustomerUnbilledCharges
{
    /// <summary>
    /// Request internal of QueryCustomerUnbilledCharges
    /// </summary>
    public class QueryCustomerUnbilledChargesRequestInternal : RequestBase, ICustomerBasedRequest
    {
        /// <summary>
        /// CustomerInfo get by framework
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }
        /// <summary>
        /// Listo of CustomerCharge
        /// </summary>
        public virtual List<CustomerCharge> CustomerCharges { get; set; }
    }
}
