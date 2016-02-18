using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity PackageDealerMappingInfo 
    /// </summary>
    /// <typeparam name="TPackageDealerMappingInfo">Entity managed by the repository, is or extends PackageDealerMappingInfo</typeparam>
    public class PackageDealerMappingInfoRepositoryNH<TPackageDealerMappingInfo> : 
        NHibernateRepository<TPackageDealerMappingInfo, Int32>, 
        IPackageDealerMappingInfoRepository<TPackageDealerMappingInfo> where TPackageDealerMappingInfo : PackageDealerMappingInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Get all the mappings for a given package ID
        /// </summary>
        /// <param name="packageId">the Id of the package</param>
        /// <returns>an enumerable with the packages mapping</returns>
        public IEnumerable<PackageDealerMappingInfo> GetByPackageId(int packageId)
        {
            return GetQuery().Where(x => x.PackageID == packageId).Cacheable().CacheRegion(CacheRegion).Future();
        }
    }
}
