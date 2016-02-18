using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity PricePlanPackageVodafoneFileMap 
    /// </summary>
    /// <typeparam name="TPricePlanPackageVodafoneFileMap">Entity managed by the repository, is or extends PricePlanPackageVodafoneFileMap</typeparam>
    public class PricePlanPackageVodafoneFileMapRepositoryNH<TPricePlanPackageVodafoneFileMap> :
        NHibernateRepository<TPricePlanPackageVodafoneFileMap, Int32>,
        IPricePlanPackageVodafoneFileMapRepository<TPricePlanPackageVodafoneFileMap>
        where TPricePlanPackageVodafoneFileMap : PricePlanPackageVodafoneFileMap
    {

        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all the price plan package mappings for a given dealer.
        /// </summary>
        /// <param name="MVNO">the vmno to get the mappigns of</param>
        /// <returns>the list of mappings</returns>
        public IEnumerable<TPricePlanPackageVodafoneFileMap> GetMappingsForVMNO(string MVNO)
        {
            return GetQuery().
                Where(x => x.VMNO == MVNO).
                Cacheable().CacheRegion(CacheRegion).
                Future();

        }

        /// <summary>
        /// Gets all the price plan package mappings for a given dealer.
        /// </summary>
        /// <param name="MVNO">the vmno to get the mappigns of</param>
        /// <param name="packageId">the id of the package to get the mapings</param>
        /// <returns>the list of mappings</returns>
        public IEnumerable<TPricePlanPackageVodafoneFileMap> GetMappingsForMVNOAndPackageId(string MVNO, int packageId)
        {
            return GetQuery().
               Where(x => x.VMNO == MVNO && x.PackageId == packageId).
               Cacheable().CacheRegion(CacheRegion).
               Future();
        }       
    }
}
