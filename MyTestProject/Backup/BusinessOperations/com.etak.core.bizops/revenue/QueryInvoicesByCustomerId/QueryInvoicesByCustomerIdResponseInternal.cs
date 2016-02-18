using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.QueryInvoicesByCustomerId
{
    /// <summary>
    /// Response in core model for ueryInvoicesByCustomerId, returns a list of invoices
    /// </summary>
    public class QueryInvoicesByCustomerIdResponseInternal : ResponseBase,ISubscriptionBasedResponse
    {
        /// <summary>
        /// List of invoices of the customer with the total amount of each
        /// </summary>
        public virtual IList<KeyValuePair<Invoice, CustomerCharge>> Invoices { get; set; }
        /// <summary>
        /// Subscription of the customer
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
