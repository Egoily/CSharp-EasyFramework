using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryInvoicesByMSISDN
{
    /// <summary>
    /// Request for QueryInvoicesByCustomerId receives a customer id
    /// </summary>
    public class QueryInvoicesByMSISDNRequestDTO : RequestBaseDTO , IMsisdnBasedDTORequest
    {
        /// <summary>
        /// MSISDN requested
        /// </summary>
        public string MSISDN { get; set; }

        /// <summary>
        /// The number of invoices to retrieve
        /// </summary>
        public int? NumberOfInvoices { get; set; }

    }
}
