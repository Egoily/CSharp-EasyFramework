using System;
using System.Collections.Generic;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.QueryInvoicesByMSISDN
{
    /// <summary>
    /// Response in core model for ueryInvoicesByCustomerId, returns a list of invoices
    /// </summary>
    public class QueryInvoicesByMSISDNResponse : ResponseBase
    {
        /// <summary>
        /// List of invoices of the customer
        /// </summary>
        public IList<KeyValuePair<Invoice, CustomerCharge>> Invoices { get; set; }
    }
}
