using System;
using System.Collections.Generic;
using com.etak.core.model.subscription;

namespace com.etak.core.model.resource
{
    /// <summary>
    /// GSM Subscription object DTO
    /// </summary>
    public class GSMSubscriptionDTO
    {
        /// <summary>
        /// Msisdn of the Subscription
        /// </summary>
        public String MSISDN { get; set; }
        /// <summary>
        /// IMSI of the Subscription
        /// </summary>
        public String IMSI { get; set; }
        /// <summary>
        /// The Status of the Subscription
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Unbilled Balance of the Subscription
        /// </summary>
        public decimal UnbilledBalance { get; set; }
        /// <summary>
        /// A list of DTO Promotion's
        /// </summary>
        public IList<CustomerPromotionDTO> Promotions { get; set; }
        /// <summary>
        /// The Credit Limit
        /// </summary>
        public decimal CreditLimit { get; set; }
    }
}
