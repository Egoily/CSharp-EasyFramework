using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The respository interface for <typeparamref name="TSettingInfo"/> entity
    /// </summary>
    /// <typeparam name="TSettingInfo">The entity managed by the interface, is or extends SettingInfo</typeparam>
    public interface ISettingExtendDetailInfoRepository<TSettingInfo> : IRepository<TSettingInfo, Int32> where TSettingInfo : SettingExtendDetailInfo
    {

        /// <summary>
        /// Gets the SettingExtendDetailInfo for a given dealer and category
        /// </summary>
        /// <param name="dealerId">the dealer of the entity</param>
        /// <param name="extendSettingCategory">the category of the entity</param>
        /// <returns>the list of entities that fulfill the conditions</returns>
        IEnumerable<TSettingInfo> GetByDealerIdAndCategoryId(int dealerId, ExtendSettingCategory extendSettingCategory);

        /// <summary>
        /// Gets the SettingExtendDetailInfo for a given dealer
        /// </summary>
        /// <param name="dealerId">The id of the dealer to filter the settings</param>
        /// <returns>The list of matching entities that fulfill the conditions</returns>
        IEnumerable<SettingExtendDetailInfo> GetByDealerId(int dealerId);
    }
}
