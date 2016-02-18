using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.portability;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.portability
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity MNPPortabilityCustomerInfo 
    /// </summary>
    /// <typeparam name="TMNPPortabilityCustomerInfo">Entity managed by the repository, is or extends MNPPortabilityCustomerInfo</typeparam>
    public class MNPPortabilityCustomerInfoRepositoryNH<TMNPPortabilityCustomerInfo> :
        NHibernateRepository<TMNPPortabilityCustomerInfo, Int32>,
         IMNPPortabilityCustomerInfoRepository<TMNPPortabilityCustomerInfo> where TMNPPortabilityCustomerInfo : MNPPortabilityCustomerInfo
    {
        /// <summary>
        /// Gets all the MNPPortabilityCustomerInfo of a given customer
        /// </summary>
        /// <param name="customerId">The id of the customer to which the MNPPortabilityCustomerInfo is associated</param>
        /// <returns>The list of MNPPortabilityCustomerInfo of the customer</returns>
        public IEnumerable<TMNPPortabilityCustomerInfo> GetByCustomerId(int customerId)
        {
            return GetQuery().Where(x => x.CustomerID == customerId).Future();
        }
    }
}
