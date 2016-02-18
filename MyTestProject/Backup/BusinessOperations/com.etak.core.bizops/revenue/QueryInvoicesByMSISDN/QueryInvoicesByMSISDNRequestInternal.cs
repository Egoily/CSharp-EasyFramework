using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryInvoicesByMSISDN
{
    /// <summary>
    /// Request in core model for QueryInvoicesByCustomerId
    /// </summary>
    public class QueryInvoicesByMSISDNRequestInternal : RequestBase, ISubscriptionLastActiveBasedRequest
    {
        /// <summary>
        /// The number of invoices to retrieve
        /// </summary>
        public virtual int? NumberOfInvoices { get; set; }

        /// <summary>
        /// MSISDN of the suscription used to retrieve the invoices
        /// </summary>
        public virtual string MSISDN { get; set; }

        /// <summary>
        /// Suscription used to retrieve the invoices
        /// </summary>
        public virtual ResourceMBInfo Subscription { get; set; }
    }
}
