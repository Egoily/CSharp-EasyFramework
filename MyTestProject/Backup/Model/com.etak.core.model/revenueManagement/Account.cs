using System;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Account associated to a customer
    /// </summary>
    [Serializable]
    public class Account
    {
        /// <summary>
        /// The unique Id of the entity
        /// </summary>
        public virtual Int64 Id { get; set; }

        /// <summary>
        /// The type of the account
        /// </summary>
        public virtual AccountType Type { get; set; }

        /// <summary>
        /// The name (short name) of the account 
        /// </summary>
        public virtual MultiLingualDescription Name { get; set; }

        /// <summary>
        /// The description of the account 
        /// </summary>
        public virtual MultiLingualDescription Description { get; set; } 

        /// <summary>
        /// The billing cycle that triggers the billing for this account.
        /// </summary>
        public virtual BillCycle BillingCycle { get; set; }

        /// <summary>
        /// The last bill run that was executed, billing the charges for this accout.
        /// </summary>
        public virtual BillRun LastBillRun { get; set; }

        /// <summary>
        /// The customer that is currently responsible of handling the payment of the charges applied to this account. (also there will be a matching row in CUSTOMER ACCOUNT ASSN.
        /// </summary>
        public virtual CustomerInfo CurrentAsignedCustomer { get; set; }

        /// <summary>
        /// A separate class to storing the balance so we suffer less concurrency problems.
        /// </summary>
        public virtual BalanceForAccount Balance { get; set; }      
    }
}