using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// The price for a charge in a time range and a currency
    /// </summary>
    [DataContract]
    public class ChargePriceCatalogDTO
    {
        /// <summary>
        /// The start date of the period in which this price applies
        /// </summary>
        [DataMember]
        public DateTime StartDate;

        /// <summary>
        ///  The end date of the period in which this price applies
        /// </summary>
        [DataMember]
        public DateTime? EndDate;

        /// <summary>
        /// The currency of the Amount
        /// </summary>
        [DataMember]
        public ISO4217CurrencyCodes Currency;

        /// <summary>
        /// The amount expressed in Currency of this price
        /// </summary>
        [DataMember]
        public Decimal Amount;

    }
}
