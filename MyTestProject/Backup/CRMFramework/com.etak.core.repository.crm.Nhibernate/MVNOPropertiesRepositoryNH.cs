using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity MVNOPropertiesInfo 
    /// </summary>
    /// <typeparam name="TMVNOPropertiesInfo">Entity managed by the repository, is or extends MVNOPropertiesInfo</typeparam>
    public class MVNOPropertiesRepositoryNH<TMVNOPropertiesInfo>
        : NHibernateRepository<TMVNOPropertiesInfo, Int32>,
       IMVNOPropertiesRepository<TMVNOPropertiesInfo> where TMVNOPropertiesInfo : MVNOPropertiesInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets of TCrmCustomersBonusRelationShipInfo
        /// </summary>
        /// <param name="VMNOId">The filter </param>
        /// <returns>The list of matching TCrmCustomersBonusRelationShipInfo</returns>
        public IEnumerable<TMVNOPropertiesInfo> GetByVMNOId(string VMNOId)
        {

            return (GetQuery().
                                Where( x=> x.VMO == VMNOId).                                
                                Cacheable().
                                CacheRegion(CacheRegion).
                                Future());
        }
    }
}
