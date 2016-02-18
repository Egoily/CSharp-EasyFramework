using System;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// CustomerCharge DTO object
    /// </summary>
    public class CustomerChargeDTO
    {
        /// <summary>
        /// Unique id of the Invoice
        /// </summary>

        public Int64 Id { get; set; }

        /// <summary>
        /// The Customer that is associated with this charge
        /// </summary>
        public Int64 CustomerId { get; set; }

        /// <summary>
        /// The charge catalog Id that is being charged to the account.
        /// </summary>
        public Int64 ChargeId { get; set; }

        /// <summary>
        /// The date in which the charge was created
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Amount of the charge
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Currency of the charge
        /// </summary>
        public ISO4217CurrencyCodes Currency { get; set; }

        /// <summary>
        /// Optional field used in case of one time charges.
        /// </summary>
        public String ReferenceCode { get; set; }

        /// <summary>
        /// Id of the invoice that invoiced this charge, null in case of un invoiced charges 
        /// </summary>
        public Int64? InvoiceId { get; set; }

        /// <summary>
        /// Id of the purchase that produced this charge, null in case of charge not associated with a product
        /// </summary>
        public Int64? ProductPurchaseId { get; set; }

    }
}
