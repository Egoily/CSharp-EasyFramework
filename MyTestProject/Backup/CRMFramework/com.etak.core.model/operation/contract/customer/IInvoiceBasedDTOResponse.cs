using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// Interface to return an Invoice DTO object
    /// </summary>
    public interface IInvoiceBasedDTOResponse
    {
        /// <summary>
        /// The Invoice to be returned
        /// </summary>
        InvoiceDTO InvoiceDto { get; set; }
    }
}
