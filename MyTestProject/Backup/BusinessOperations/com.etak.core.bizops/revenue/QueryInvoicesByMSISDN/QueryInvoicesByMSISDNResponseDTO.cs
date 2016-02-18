using System.Collections.Generic;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.dto;

namespace com.etak.core.bizops.revenue.QueryInvoicesByMSISDN
{
    /// <summary>
    /// Response of QueryInvoicesByCustomerId returns a list of invoices
    /// </summary>
    public class QueryInvoicesByMSISDNResponseDTO : ResponseBaseDTO, ICustomerBasedDTOResponse
    {
        /// <summary>
        /// Customer in dto model information in the response
        /// </summary>
        public CustomerDTO Customer { get; set; }

        /// <summary>
        /// The list of invoices of the customer
        /// </summary>
        public IList<InvoiceDTO> Invoices { get; set; }
    }
}
