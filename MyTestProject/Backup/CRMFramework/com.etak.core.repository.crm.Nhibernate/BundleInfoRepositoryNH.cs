using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Implementation of IBundleInfoRepository based on Nhibernate
    ///  </summary>
    /// <typeparam name="TBundleInfo">Type that is or extends BundleInfo</typeparam>
    public class BundleInfoRepositoryNH<TBundleInfo> : NHibernateRepository<TBundleInfo, Int32>, IBundleInfoRepository<TBundleInfo> where TBundleInfo : BundleInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all TBundleInfo
        /// </summary>
        /// <returns>A future enumerable with all the TBundleInfo</returns>
        public IEnumerable<TBundleInfo> GetAll()
        {
            return GetQuery().Cacheable().CacheRegion(CacheRegion).Future();
        }
    }
}
