using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Respository for <typeparamref name="TCrmCustomersMSISDNGroupMembers"/> entity
    /// </summary>
    /// <typeparam name="TCrmCustomersMSISDNGroupMembers">The type of the managed entity, is or extends CrmCustomersMSISDNGroupMembers</typeparam>
    public interface ICrmCustomersMSISDNGroupMembersRepository<TCrmCustomersMSISDNGroupMembers> :
            IRepository<TCrmCustomersMSISDNGroupMembers, String> where TCrmCustomersMSISDNGroupMembers : CrmCustomersMSISDNGroupMembers
    {
        /// <summary>
        /// Gets all TCrmCustomersMSISDNGroupMembers of a given customer and gategory
        /// </summary>
        /// <param name="customerId">the id of the customer to which the TCrmCustomersMSISDNGroupMembers associated</param>
        /// <param name="numberCategory">the Number category of the Number</param>
        /// <returns>the list of TCrmCustomersMSISDNGroupMembers</returns>
        IEnumerable<TCrmCustomersMSISDNGroupMembers> GetFFNumbersByCustomerId(int customerId, SpecificNumberCategory numberCategory);

        /// <summary>
        /// Gets all TCrmCustomersMSISDNGroupMembers of a given customer and gategory
        /// </summary>
        /// <param name="customerId">the id of the customer to which the TCrmCustomersMSISDNGroupMembers associated</param>
        /// <returns>the list of TCrmCustomersMSISDNGroupMembers</returns>
        IEnumerable<TCrmCustomersMSISDNGroupMembers> GetAllFFNumbersByCustomerId(int customerId);

        /// <summary>
        /// Deletes N (quantity) TCrmCustomersMSISDNGroupMembers of a given customer and category
        /// </summary>
        /// <param name="customerId">the id of the customer to which the TCrmCustomersMSISDNGroupMembers associated</param>
        /// <param name="quantity">the number of TCrmCustomersMSISDNGroupMembers to delete</param>
        /// <param name="numberCategory">the SpecificNumberCategory of the TCrmCustomersMSISDNGroupMembers delete</param>
        void TruncateFFNumbersByCustomerId(Int32 customerId, int quantity, SpecificNumberCategory numberCategory);
    }
}
