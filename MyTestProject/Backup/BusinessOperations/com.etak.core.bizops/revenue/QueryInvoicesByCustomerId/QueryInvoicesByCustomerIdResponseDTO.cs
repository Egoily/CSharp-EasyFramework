using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.revenue.QueryInvoicesByCustomerId
{
    /// <summary>
    /// Response of QueryInvoicesByCustomerId returns a list of invoices
    /// </summary>
    public class QueryInvoicesByCustomerIdResponseDTO : ResponseBaseDTO, ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// The list of invoices of the customer
        /// </summary>
        public IList<InvoiceDTO> Invoices { get; set; }
        
        /// <summary>
        /// Subscription of the customer
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
