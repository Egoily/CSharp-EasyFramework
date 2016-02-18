using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Dto Payment Object
    /// </summary>
    public class PaymentDataDTO
    {
        /// <summary>
        /// A integer value to represent the paymentMethod used.
        /// </summary>
        public int PaymentMethodId { get; set; }

        /// <summary>
        /// The amount payed.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The discount applyed to pay
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// The reference that relates the payment with a third party
        /// </summary>
        public String ExternalPaymentId { get; set; }

        /// <summary>
        /// More information related with the payment (if needed)
        /// </summary>
        public String PaymentInfo { get; set; }
        /// <summary>
        /// Tax information
        /// </summary>
        public String TaxInfo { get; set; }
    }
}
