using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity BillCycle 
    /// </summary>
    /// <typeparam name="TBillCycle">Entity managed by the repository, is or extends BillCycle</typeparam>
    public class BillCycleRepositoryNH<TBillCycle> : 
        NHibernateRepository<TBillCycle, Int32>,
        IBillCycleRepository<TBillCycle> where TBillCycle : BillCycle
    {
        /// <summary>
        /// Gets all the billing cycles for a mvno
        /// </summary>
        /// <param name="dealer">the vmno owning the billing cycles</param>
        /// <returns>the list of billing cycles</returns>
        public IEnumerable<TBillCycle> GetBillingCyclesForVMNO(DealerInfo dealer)
        {
            return (GetQuery().Where(x => x.VMNO == dealer).Cacheable().CacheRegion(CacheRegions.CatalogCacheRegion).Future());
        }
    }
}
