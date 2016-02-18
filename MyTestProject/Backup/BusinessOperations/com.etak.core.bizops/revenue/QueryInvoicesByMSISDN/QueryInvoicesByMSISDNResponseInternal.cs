using System.Collections.Generic;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model;

namespace com.etak.core.bizops.revenue.QueryInvoicesByMSISDN
{
    /// <summary>
    /// Response in core model for ueryInvoicesByCustomerId, returns a list of invoices
    /// </summary>
    public class QueryInvoicesByMSISDNResponseInternal : ResponseBase, ICustomerBasedResponse
    {
        /// <summary>
        /// The customer that will be returned.
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary>
        /// List of invoices of the customer
        /// </summary>
        public virtual IList<KeyValuePair<Invoice, CustomerCharge>> Invoices { get; set; }
    }
}
