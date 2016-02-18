using com.etak.core.model;
using com.etak.core.repository.crm.dealer;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.repository.crm.Nhibernate.dealer
{
    /// <summary>
    /// Implementation of IBundleInfoRepository based on Nhibernate
    /// </summary>
    /// <typeparam name="TMVNORetailMargin">Type that is or extends MVNORetailMargin</typeparam>
    public class MVNORetailMarginRepositoryNH<TMVNORetailMargin> : NHibernateRepository<TMVNORetailMargin, Int32>,
        IMVNORetailMarginRepository<TMVNORetailMargin> where TMVNORetailMargin : MVNORetailMargin
    {
        private const String CacheRegion = CacheRegions.SystemSettingsCacheRegion;

        /// <summary>
        /// Gets all TMVNORetailMargin
        /// </summary>
        /// <returns>A future enumerable with all the TBundleInfo</returns>
        public IEnumerable<TMVNORetailMargin> GetAll()
        {
            return GetQuery().Cacheable().CacheRegion(CacheRegion).Future();
        }
    }
}
