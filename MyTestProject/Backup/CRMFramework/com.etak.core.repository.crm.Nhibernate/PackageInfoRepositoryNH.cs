using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity PackageInfo 
    /// </summary>
    /// <typeparam name="TPackageInfo">Entity managed by the repository, is or extends PackageInfo</typeparam>
    public class PackageInfoRepositoryNH<TPackageInfo>
                    : NHibernateRepository<TPackageInfo, Int32>,   //Extends and gets basic CRUD operations
                      IPackageInfoRepository<TPackageInfo> //Implementes 
                        where TPackageInfo : PackageInfo
    {

        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all the PackagesInfo for a dealer id
        /// </summary>
        /// <param name="dealerId">the dealer owner of the package to retrieve</param>
        /// <returns>the list of packages of the dealer</returns>
        public IEnumerable<TPackageInfo> GetPackageInfoForDealerId(int dealerId)
        {
            return (GetQuery().Where(x => x.DealerID == dealerId)
                .Cacheable().CacheRegion(CacheRegion).
                Future());
        }      
    }
}
