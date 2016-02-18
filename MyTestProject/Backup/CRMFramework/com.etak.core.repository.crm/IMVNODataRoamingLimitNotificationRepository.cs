using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for MVNODataRoamingLimitNotification
    /// </summary>
    /// <typeparam name="TMVNODataRoamingLimitNotification">The entity managed by the repository, is or extends MVNODataRoamingLimitNotification</typeparam>
    public interface IMVNODataRoamingLimitNotificationRepository<TMVNODataRoamingLimitNotification> : IRepository<TMVNODataRoamingLimitNotification, Int32>
        where TMVNODataRoamingLimitNotification : MVNODataRoamingLimitNotification
    {
        /// <summary>
        /// Gets all MVNODataRoamingLimitNotification notifications 
        /// </summary>
        /// <returns>all the MVNODataRoamingLimitNotification</returns>
        IEnumerable<TMVNODataRoamingLimitNotification> GetAllNotifications();
    }
}
