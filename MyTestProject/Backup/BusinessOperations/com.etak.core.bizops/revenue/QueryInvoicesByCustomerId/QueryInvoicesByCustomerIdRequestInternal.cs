using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryInvoicesByCustomerId
{
    /// <summary>
    /// Request in core model for QueryInvoicesByCustomerId
    /// </summary>
    public class QueryInvoicesByCustomerIdRequestInternal : RequestBase, ICustomerBasedRequest
    {
        /// <summary>
        /// Customer that needs to be used to retrieve the invoices
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary>
        /// The number of invoices to retrieve
        /// </summary>
        public virtual int? NumberOfInvoices { get; set; }
    }
}
