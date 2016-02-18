using System;
using System.Collections.Generic;
using com.etak.core.operation.contract;

namespace com.etak.core.bizops.revenue.BenefitTransfer
{
    /// <summary>
    /// Configuration with the settings requierd to perfrom BenefitTransfer
    /// </summary>
    public class BenefitTransferConfiguration : BasicOperationConfiguration
    {
        /// <summary>
        /// the product of the purchase for the source customer
        /// </summary>
        public Int32 BenefitSourceTransferProductId { get; set; }

        /// <summary>
        /// represents the product of the purchase for the destination customer
        /// </summary>
        public Int32 BenefitDestinationTransferProductId { get; set; }

        /// <summary>
        /// ORDERED List of valid promotion details id  where the source benefits can be retrieved.
        /// (The order of the list represents the priority of the promotions to substract balance from)
        /// </summary>
        public IList<Int32> ValidSourcePromotions { get; set; }

        /// <summary>
        /// This value represents the maximum amount that a customer will be allowed to transfer to other customers during the Bill Cycle
        /// </summary>
        public Decimal BenefitTransferSenderLimit { get; set; }

        /// <summary>
        /// Defines the maximum number of different customers to whom each customer can transfer Balance per month.
        /// </summary>
        /// <example>
        /// if the number is set to 5, each customer will be allowed only to transfer Data to five different customers every month
        /// </example>
        public Decimal MaxTransferDestinationLimit { get; set; }
  
        /// <summary>
        ///  This value represents the maximum amount that a customer can receive during a billing cycle. 
        /// If any transference will exceed this limit, the transference will be rejected.
        /// </summary>
        public Decimal BenefitTransferReceiverLimit { get; set; }

        /// <summary>
        /// This limit represents the maximum value that a customer can have concurrently during a bill cycle considering the sum of his Transferred Promotions+Accumulation Promotion.
        /// </summary>
        public Decimal TotalBenefitLimit { get; set; }
    }
}
