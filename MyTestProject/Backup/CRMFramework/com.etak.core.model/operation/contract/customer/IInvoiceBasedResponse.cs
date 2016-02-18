using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// Interface for a Invoice Based Response
    /// </summary>
    public interface IInvoiceBasedResponse
    {
        /// <summary>
        /// The Invoice to be returned
        /// </summary>
        Invoice Invoice { get; set; }
    }
}
