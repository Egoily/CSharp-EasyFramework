using System;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Invoice DTO for invoice operations
    /// </summary>
    /// <author>IgnasiG</author>
    /// <datetime>11/20/2014-4:44 PM</datetime>
    public class InvoiceDTO
    {
        /// <summary>
        /// Invoice Id of the object
        /// </summary>
        public long InvoiceId { get; set; }
        /// <summary>
        /// BillingCycle of the Invoice
        /// </summary>
        public int BillingCycle { get; set; }
        /// <summary>
        /// Amount corresponding to the Amount
        /// </summary>
        public Decimal Amount { get; set; }
    }
}
