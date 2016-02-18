using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The respository interface for <typeparamref name="TSettingInfo"/> entity
    /// </summary>
    /// <typeparam name="TSettingInfo">The entity managed by the interface, is or extends SettingInfo</typeparam>
    public interface ISettingInfoRepository<TSettingInfo> : IRepository<TSettingInfo, Int32> where TSettingInfo : SettingInfo
    {
        /// <summary>
        /// Gets the sms configuration of a dealer
        /// </summary>
        /// <param name="dealerId">the dealer for which the sms configuration is</param>
        /// <returns>the list of sms settings</returns>
        IEnumerable<TSettingInfo> GetByDealerId(int dealerId);

        /// <summary>
        /// Gets the sms configuration of a dealer loading the details association
        /// </summary>
        /// <param name="dealerId">the dealer for which the sms configuration is</param>
        /// <returns>the list of sms settings</returns>
        IEnumerable<TSettingInfo> GetSettingInfoWithDetailByDealerId(int dealerId);

        /// <summary>
        /// Gets the sms configuration of a dealer and an event
        /// </summary>
        /// <param name="dealerId">the dealer for which the sms configuration is</param>
        /// <param name="eventId">the id of the event to which the sms configuration applies</param>
        /// <returns>the list of sms settings</returns>
        IEnumerable<TSettingInfo> GetSettingInfoWithDetailByDealerIdAndEventID(int dealerId, int eventId);
    }
}
