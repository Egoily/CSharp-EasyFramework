using System;
using com.etak.core.operation.contract;

namespace com.etak.core.bizops.revenue.QueryInvoicesByCustomerId
{
    /// <summary>
    /// JSON configuration for QueryInvoicesByCustomerId
    /// </summary>
    public class QueryInvoicesByCustomerIdConfiguration : BasicOperationConfiguration
    {
        /// <summary>
        /// The id of the charge that will be used to present as total.
        /// </summary>
        public Int32 AggregateTotalInvoiceChargeId { get; set; }

        /// <summary>
        /// Number of Invoices
        /// </summary>
        public int? NumberOfInvoices { get; set; }
    }
}
