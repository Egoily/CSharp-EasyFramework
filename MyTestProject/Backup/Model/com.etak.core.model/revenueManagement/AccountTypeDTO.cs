using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// The account 
    /// </summary>
    [DataContract]
    public class AccountTypeDTO
    {
        /// <summary>
        /// Unique id of the account
        /// </summary>
        [DataMember] public String Id { get; set; }

        /// <summary>
        /// The id of the customer that is currently assigned to this account
        /// </summary>
        [DataMember]
        public Int64 CurrentCustomerId { get; set; }

        /// <summary>
        /// The status of the account
        /// </summary>
        [DataMember] public AccountStatus Status { get; set; }
    }
}