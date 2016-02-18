using System;
using System.Collections.Generic;

namespace com.etak.core.model.revenueManagement
{
    public enum InvoiceStatus
    {
        Drafted = 0,
        Finished = 1,
        Test = 2,
        Validated = 10,
        Ready = 20,
        Open = 30,
        Closed = 40,
        Cancelled = 50,
    }

    [Serializable]
    public class Invoice
    {
        /// <summary>
        /// Unique Id of the invoice
        /// </summary>
        public virtual  Int64 Id { get; set; }

        /// <summary>
        /// Current status of the invoice
        /// </summary>
        public virtual InvoiceStatus? Status { get; set; }

        /// <summary>
        /// The account that is charged with this Invoice
        /// </summary>
        public virtual Account ChargingAccount { get; set; }

        /// <summary>
        /// The customer against whom the invoice is issued against.
        /// </summary>
        public virtual CustomerInfo ChargedCustomer { get; set; }

        /// <summary>
        /// the legal invoice number, the number of the invoice as presented on the bill. This number is the actual reference of the legal document that an invoice is.
        /// There are different legal requirements to be applied when generating those numbers, depending on the country laws.
        /// </summary>
        public virtual String LegalInvoiceNumber { get; set; }

        /// <summary>
        /// Set of charges invoiced by this invoice
        /// </summary>
        public virtual IList<CustomerCharge> Charges { get; set; }

        /// <summary>
        /// The BillRun that generated this Invoice
        /// </summary>
        public virtual BillRun GeneratingBillRun { get; set; }

        /// <summary>
        /// the first day that is billed through the invoice.
        /// </summary>
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// the last day that is billed through the invoice.
        /// </summary>
        public virtual DateTime? EndDate { get; set; }
        public virtual string InvoiceFileName { get; set; }

    }
}