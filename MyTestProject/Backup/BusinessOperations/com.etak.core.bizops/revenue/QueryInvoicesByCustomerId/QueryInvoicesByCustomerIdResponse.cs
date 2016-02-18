using System;
using System.Collections.Generic;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.QueryInvoicesByCustomerId
{
    /// <summary>
    /// Response in core model for ueryInvoicesByCustomerId, returns a list of invoices
    /// </summary>
    public class QueryInvoicesByCustomerIdResponse : ResponseBase
    {
        /// <summary>
        /// List of invoices of the customer with the total amount of each
        /// </summary>
        public IList<KeyValuePair<Invoice, CustomerCharge>> Invoices { get; set; }
    }
}
