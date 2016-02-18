using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity MVNONotificationSettingInfo 
    /// </summary>
    /// <typeparam name="TMVNONotificationSettingInfo">Entity managed by the repository, is or extends MVNONotificationSettingInfo</typeparam>
    public class MVNONotificationSettingInfoRepositoryNH<TMVNONotificationSettingInfo> : 
        NHibernateRepository<TMVNONotificationSettingInfo, Int32>, 
        IMVNONotificationSettingInfoRepository<TMVNONotificationSettingInfo> 
        where TMVNONotificationSettingInfo : MVNONotificationSettingInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all MVNONotificationSettingInfo settings 
        /// </summary>
        /// <returns>all the MVNONotificationSettingInfo</returns>
        public IEnumerable<TMVNONotificationSettingInfo> GetAllSettings()
        {
            return GetQuery().Cacheable().CacheRegion(CacheRegion).Future();
        }
    }
}
