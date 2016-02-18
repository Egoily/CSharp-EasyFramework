using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity IDMappingInfo 
    /// </summary>
    /// <typeparam name="TIDMappingInfo">Entity managed by the repository, is or extends IDMappingInfo</typeparam>
    public class IDMappingInfoRepositoryNH<TIDMappingInfo>
        : NHibernateRepository<TIDMappingInfo, Int32>,
       IIDMappingInfoRepository<TIDMappingInfo> where TIDMappingInfo : IDMappingInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets the TIDMappingInfo for the vmno
        /// </summary>
        /// <param name="MVNO">the vmno that is mapped</param>
        /// <returns>the list of mappings</returns>
        public IEnumerable<TIDMappingInfo> GetIdMappingByVmno(int MVNO)
        {
            return (GetQuery().
                        Where(x => x.MvnoId == MVNO).
                        Cacheable().CacheRegion(CacheRegion).
                        Future());
        }

        /// <summary>
        /// Gets all the TIDMappingInfo for a given MVNO and promotion plan name
        /// </summary>
        /// <param name="MVNO">the id of the MVNO/FiscalUnit</param>
        /// <param name="promotionPlanName">the prmotion plan name of the TIDMappingInfo</param>
        /// <returns>the list of matching TIDMappingInfo</returns>
        public IEnumerable<TIDMappingInfo> GetByVMNOAndETID(int MVNO, string promotionPlanName)
        {
            return (GetQuery().
                        Where(x => x.MvnoId == MVNO && x.ETID1 == promotionPlanName).
                        Cacheable().CacheRegion(CacheRegion).
                        Future());
        }
    }
}
