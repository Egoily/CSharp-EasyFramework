using System;
using com.etak.core.model;
using com.etak.core.operation.contract;

namespace com.etak.core.bizops.fullfilment.CreateCustomerAccountCurrency
{
    /// <summary>
    /// Configuration with the settings required to perform CreateCustomerAccountCurrency
    /// </summary>
    public class CreateCustomerAccountCurrencyConfiguration : BasicOperationConfiguration
    {
        /// <summary>
        /// The multilingual's Id to be used when creating an account
        /// </summary>
        public Int32 AccountDescriptionId { get; set; }

        /// <summary>
        /// The multilingual's Id to be used when creating an account
        /// </summary>
        public Int32 AccountNameId { get; set; }

        /// <summary>
        /// Value that corresponds to the Currency to be used in the Customer's account
        /// </summary>
        public ISO4217CurrencyCodes AccountCurrency { get; set; }

        /// <summary>
        /// The Billcycle Id to be set to the customer if it's not passed as a parameter
        /// </summary>
        public Int32 BillcycleId { get; set; }
    }
}
