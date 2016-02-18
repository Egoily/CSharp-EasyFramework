using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.drl;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.drl
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CustomerDataRoamingLimit 
    /// </summary>
    /// <typeparam name="TCustomerDataRoamingLimit">Entity managed by the repository, is or extends CustomerDataRoamingLimit</typeparam>
    public class CustomerDataRoamingLimitRepositoryNH<TCustomerDataRoamingLimit> : NHibernateRepository<TCustomerDataRoamingLimit, Int64>,
        ICustomerDataRoamingLimitRepository<TCustomerDataRoamingLimit> where TCustomerDataRoamingLimit : CustomerDataRoamingLimit
    {
        /// <summary>
        /// Gets all the TCustomerDataRoamingLimit for a given customer
        /// </summary>
        /// <param name="customerId">the customer of TCustomerDataRoamingLimit</param>
        /// <returns>the list of TCustomerDataRoamingLimit of the customer provided</returns>
        public IEnumerable<TCustomerDataRoamingLimit> GetByCustomerID(int customerId)
        {
           return(GetQuery().Where(x => x.Customer.CustomerID == customerId).Future());
        }
    }
}
