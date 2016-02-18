using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity BillRun 
    /// </summary>
    /// <typeparam name="TBillRun">Entity managed by the repository, is or extends BillRun</typeparam>
    public class BillRunRepositoryNH<TBillRun> :
        NHibernateRepository<TBillRun, Int32>, 
        IBillRunRepository<TBillRun> where TBillRun : BillRun
    {
        /// <summary>
        /// gets all the bill runs for a billing cycle
        /// </summary>
        /// <param name="billcycle">the owner of the bill runs to recover</param>
        /// <returns>the list of bill runs for a billing cycle</returns>
        public IEnumerable<TBillRun> GetBillRunsForBillCycle(BillCycle billcycle)
        {
            return (GetQuery().Where(x => x.BillingCycle == billcycle).Cacheable().CacheRegion(CacheRegions.CatalogCacheRegion).Future());
        }


        /// <summary>
        /// gets all the bill runs for a billing cycle
        /// </summary>
        /// <param name="billCycle">the owner of the bill runs to recover</param>
        /// <param name="dateRange">The date in which the bill run start/end date must be between</param>
        /// <returns>the list of bill runs for a billing cycle</returns>
        public IEnumerable<TBillRun> GetBillRunInDatesForBillCycle(BillCycle billCycle, DateTime dateRange)
        {
            DateTime trimmedDate = new DateTime(dateRange.Year, dateRange.Month, dateRange.Day);

            return (GetQuery().Where(x => x.BillingCycle == billCycle
                && x.StarteDate <= trimmedDate && x.EndDate >= trimmedDate)
                .Cacheable().
                CacheRegion(CacheRegions.CatalogCacheRegion).Future());
      
        }
    }
}
