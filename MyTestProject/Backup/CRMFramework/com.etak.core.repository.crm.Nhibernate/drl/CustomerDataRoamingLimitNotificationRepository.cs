using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.drl;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.drl
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CustomerDataRoamingLimitNotification 
    /// </summary>
    /// <typeparam name="TCustomerDataRoamingLimitNotification">Entity managed by the repository, is or extends CustomerDataRoamingLimitNotification</typeparam>
    public class CustomerDataRoamingLimitNotificationRepositoryNH<TCustomerDataRoamingLimitNotification> :
        NHibernateRepository<TCustomerDataRoamingLimitNotification, Int64>,
        ICustomerDataRoamingLimitNotificationRepository<TCustomerDataRoamingLimitNotification> 
        where TCustomerDataRoamingLimitNotification : CustomerDataRoamingLimitNotification
    {
        /// <summary>
        /// Gets all the TCustomerDataRoamingLimitNotification of the given customer
        /// </summary>
        /// <param name="customerId">the Id of the customer to get</param>
        /// <returns>the list of TCustomerDataRoamingLimitNotification of the customer</returns>
        public IEnumerable<TCustomerDataRoamingLimitNotification> GetByCustomerID(int customerId)
        {
           return(GetQuery().Where(x => x.Customer.CustomerID == customerId).Future());
        }
    }
}
