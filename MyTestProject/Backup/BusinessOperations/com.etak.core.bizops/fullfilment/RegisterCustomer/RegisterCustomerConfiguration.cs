using System;
using com.etak.core.model;
using com.etak.core.operation.contract;

namespace com.etak.core.bizops.fullfilment.RegisterCustomer
{
    /// <summary>
    /// Configuration class for RegisterCustomer Operation
    /// </summary>
    public class RegisterCustomerConfiguration : BasicOperationConfiguration
    {
        /*******************
         * RESOURCEMB
         ******************/
        
        /// <summary>
        /// Value for CBSuboption to create the Subscription
        /// Default Value = -1
        /// </summary>
        public Int32? ResourceCBSuboption { get; set; }

        /// <summary>
        /// Value for CBPassword to create the Subscription
        /// Default Value = "0000"
        /// </summary>
        public String ResourceCBPassword { get; set; }


        /// <summary>
        /// Value for CBWrongAttemps to create the Subscription
        /// Default Value = -1
        /// </summary>
        public Int32? ResourceCBWrongAttemps { get; set; }

        /// <summary>
        /// Value for TeleServiceList to create the Subscription
        /// Default Value = "TS11,TS12,TS21,TS22"
        /// </summary>
        public String ResourceTeleServiceList { get; set; }

        /// <summary>
        /// Value for BearerServiceList to create the Subscription
        /// Default Value = "BS1F,BS26,BS2F"
        /// </summary>
        public String ResourceBearerServiceList { get; set; }

        /*******************
         * Account Info
         *******************/

        /// <summary>
        /// Value that corresponds to the Currency to be used in the Customer's account
        /// </summary>
        public ISO4217CurrencyCodes AccountCurrency { get; set; }

        /// <summary>
        /// The multilingual's Id to be used when creating an account
        /// </summary>
        public Int32 AccountDescriptionId { get; set; }

        /// <summary>
        /// The multilingual's Id to be used when creating an account
        /// </summary>
        public Int32 AccountNameId { get; set; }

        /// <summary>
        /// The Billcycle Id to be set to the customer if it's not passed as a parameter
        /// </summary>
        public Int32 BillcycleId { get; set; }

        /// <summary>
        /// LanguageId to be set to the created Customers
        /// </summary>
        public int? LanguageId { get; set; }
    }
}
