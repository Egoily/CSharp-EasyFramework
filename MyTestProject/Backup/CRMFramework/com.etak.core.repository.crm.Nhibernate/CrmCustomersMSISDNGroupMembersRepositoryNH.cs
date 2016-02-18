using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CrmCustomersMSISDNGroupMembers 
    /// </summary>
    /// <typeparam name="TCrmCustomersMSISDNGroupMembers">Entity managed by the repository, is or extends CrmCustomersMSISDNGroupMembers</typeparam>
    public class CrmCustomersMSISDNGroupMembersRepositoryNH<TCrmCustomersMSISDNGroupMembers>
                    : NHibernateRepository<TCrmCustomersMSISDNGroupMembers, Int32>,                         //Extends and gets basic CRUD operations
                      ICrmCustomersMSISDNGroupMembersRepository<TCrmCustomersMSISDNGroupMembers> where TCrmCustomersMSISDNGroupMembers : CrmCustomersMSISDNGroupMembers //Implementes 
    {
        /// <summary>
        /// Gets all TCrmCustomersMSISDNGroupMembers of a given customer and gategory
        /// </summary>
        /// <param name="customerId">the id of the customer to which the TCrmCustomersMSISDNGroupMembers associated</param>
        /// <param name="numberCategory">the Number category of the Number</param>
        /// <returns>the list of TCrmCustomersMSISDNGroupMembers</returns>
        public IEnumerable<TCrmCustomersMSISDNGroupMembers> GetFFNumbersByCustomerId(int customerId, SpecificNumberCategory numberCategory)
        {
            return (GetQuery().
                        Where(x => x.CustomerID == customerId).
                        And(x => x.MSISDNGroupTypeId == (int)numberCategory).
                        Future());
        }

        /// <summary>
        /// Gets all TCrmCustomersMSISDNGroupMembers of a given customer and gategory
        /// </summary>
        /// <param name="customerId">the id of the customer to which the TCrmCustomersMSISDNGroupMembers associated</param>
        /// <returns>the list of TCrmCustomersMSISDNGroupMembers</returns>
        public IEnumerable<TCrmCustomersMSISDNGroupMembers> GetAllFFNumbersByCustomerId(int customerId)
        {
            return (GetQuery().
                        Where(x => x.CustomerID == customerId).
                        Future());
        }

        /// <summary>
        /// Deletes N (quantity) TCrmCustomersMSISDNGroupMembers of a given customer and category
        /// </summary>
        /// <param name="customerId">the id of the customer to which the TCrmCustomersMSISDNGroupMembers associated</param>
        /// <param name="quantity">the number of TCrmCustomersMSISDNGroupMembers to delete</param>
        /// <param name="numberCategory">the SpecificNumberCategory of the TCrmCustomersMSISDNGroupMembers delete</param>
        public void TruncateFFNumbersByCustomerId(int customerId, int quantity, SpecificNumberCategory numberCategory)
        {
            var numbers = GetFFNumbersByCustomerId(customerId, numberCategory).ToList();
            if (numbers.Count < quantity)
                return;

            foreach (var number in numbers.OrderByDescending(x => x.StartDate).Take(numbers.Count - quantity))
            {
                Delete(number);
            }
        }
    }
}
