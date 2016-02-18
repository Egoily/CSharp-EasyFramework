using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [Serializable]
    [DataContract]
    public class BalanceForAccount
    {
        /// <summary>
        /// SharedId from the Account
        /// </summary>
        public virtual Int64 Id { get; set; }

        /// <summary>
        /// Shared Id with the Account
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// Actual balance of the account
        /// </summary>
        public virtual Decimal Balance { get; set; }
    }
}