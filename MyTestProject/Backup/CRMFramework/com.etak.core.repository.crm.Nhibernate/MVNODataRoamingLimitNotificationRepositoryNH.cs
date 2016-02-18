using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity MVNODataRoamingLimitNotification 
    /// </summary>
    /// <typeparam name="TMVNODataRoamingLimitNotification">Entity managed by the repository, is or extends MVNODataRoamingLimitNotification</typeparam>
    public class MVNODataRoamingLimitNotificationRepositoryNH<TMVNODataRoamingLimitNotification> :
        NHibernateRepository<TMVNODataRoamingLimitNotification, Int32>,
        IMVNODataRoamingLimitNotificationRepository<TMVNODataRoamingLimitNotification> where TMVNODataRoamingLimitNotification : MVNODataRoamingLimitNotification
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all MVNODataRoamingLimitNotification notifications 
        /// </summary>
        /// <returns>all the MVNODataRoamingLimitNotification</returns>
        public IEnumerable<TMVNODataRoamingLimitNotification> GetAllNotifications()
        {
            return GetQuery().Cacheable().CacheRegion(CacheRegion).Future();
        }
    }
}
