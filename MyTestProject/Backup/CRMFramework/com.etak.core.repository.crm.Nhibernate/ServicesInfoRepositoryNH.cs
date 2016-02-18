using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity ServicesInfo 
    /// </summary>
    /// <typeparam name="TServicesInfo">Entity managed by the repository, is or extends ServicesInfo</typeparam>
    public class ServicesInfoRepositoryNH<TServicesInfo> : 
        NHibernateRepository<TServicesInfo, Int32>, 
        IServicesInfoRepository<TServicesInfo> where TServicesInfo : ServicesInfo
    {
        /// <summary>
        /// Gets all the services for a given customer
        /// </summary>
        /// <param name="customerId">The id of the customer from which the services needs to be retreived</param>
        /// <returns>the list of matching services</returns>
        public IEnumerable<TServicesInfo> GetByCustomerId(int customerId)
        {
            return (GetQuery().Where(x => x.CustomerInfo.CustomerID == customerId).Future());
        }

        /// <summary>
        /// Gets the Service where the main balance is stored.
        /// </summary>
        /// <param name="customerId">the customer where the</param>
        /// <returns>A list with 0 or 1 TServicesInfo holding the main balance</returns>
        public IEnumerable<TServicesInfo> GetCustomerMainService(int customerId)
        {
            DateTime now = DateTime.Now;
            return GetQuery()
                .Where(x => x.CustomerInfo.CustomerID == customerId
                  && x.StartDate < now
                  && (x.EndDate==null || x.EndDate > now)
                  && x.CREDITLIMITBASEBUNDLEID == x.BundleDefinition.BundleID)
                .Future();

        }
    }
}
