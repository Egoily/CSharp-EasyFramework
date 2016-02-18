using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity Charge 
    /// </summary>
    /// <typeparam name="TCharge">Entity managed by the repository, is or extends Charge</typeparam>
    public class ChargeRepositoryNH<TCharge> : NHibernateRepository<TCharge, Int32>, IChargeRepository<TCharge> where TCharge : Charge
    {
        /// <summary>
        /// Gets all the charges of a category
        /// </summary>
        /// <param name="category">the id of the category that the charges must have</param>
        /// <returns>the list of charges</returns>
        public IEnumerable<TCharge> GetByCategoryId(Int32 category)
        {
            var ret = GetQuery().Where(ee => ee.Category == category).Cacheable().CacheRegion(CacheRegions.CatalogCacheRegion).Future();
            return ret;
        }
    }
}
