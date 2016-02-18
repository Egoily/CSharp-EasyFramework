using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TMVNONotificationSettingInfo"/> entity
    /// </summary>
    /// <typeparam name="TMVNONotificationSettingInfo">The type of the entity managed is or extends MVNONotificationSettingInfo</typeparam>
    public interface IMVNONotificationSettingInfoRepository<TMVNONotificationSettingInfo> : IRepository<TMVNONotificationSettingInfo, int> where TMVNONotificationSettingInfo : MVNONotificationSettingInfo
    {
        /// <summary>
        /// Gets all MVNONotificationSettingInfo settings 
        /// </summary>
        /// <returns>all the MVNONotificationSettingInfo</returns>
        IEnumerable<TMVNONotificationSettingInfo> GetAllSettings();
    }
}
