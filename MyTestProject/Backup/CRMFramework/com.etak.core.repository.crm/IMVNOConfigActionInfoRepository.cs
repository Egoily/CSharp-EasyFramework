using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for MVNOConfigActionInfo
    /// </summary>
    /// <typeparam name="TMVNOConfigActionInfo">The entity managed by the repository, is or extends MVNOConfigActionInfo</typeparam>
    public interface IMVNOConfigActionInfoRepository<TMVNOConfigActionInfo> : IRepository<TMVNOConfigActionInfo, Int32> 
        where TMVNOConfigActionInfo : MVNOConfigActionInfo
    {
        //TMVNOConfigActionInfo GetByItemAndDealerId(string item, int dealerId);

        /// <summary>
        /// Retrieve the all the MVNO config which are for all MVNOs.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TMVNOConfigActionInfo> GetDefaultMVNOConfigs();

        /// <summary>
        /// Retrieve the MVNO config according to the MVNO ID. 
        /// </summary>
        /// <param name="MVNOId">the id to filter the MVNO</param>
        /// <returns>the list of matchin TMVNOConfigActionInfo by MVNOId</returns>
        IEnumerable<TMVNOConfigActionInfo> GetMVNOConfigsById(Int32 MVNOId);

        /// <summary>
        /// Retrieve the MVNO config according to the MVNO IDs.
        /// </summary>
        /// <param name="MVNOIDs">the ids to filter the MVNO</param>
        /// <returns>the list of matching TMVNOConfigActionInfo by MVNOIds</returns>
        IEnumerable<TMVNOConfigActionInfo> GetMVNOConfigsByIds(IList<Int32> MVNOIDs);

        /// <summary>
        /// Retrieve the MVNO config according to the MVNO IDs, and category and statusId
        /// </summary>
        /// <param name="MVNOId">the id to filter the MVNO</param>
        /// <param name="categoryName">the category to filter</param>
        /// <param name="statusId">the statud in which the config needs to be</param>
        /// <returns>the list of matching TMVNOConfigActionInfo by MVNOIds</returns>
        IEnumerable<TMVNOConfigActionInfo> GetMVNOConfigsByIDAndName(int MVNOId, string categoryName, int statusId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MVNOId">the id to filter the MVNO</param>
        /// <param name="categoryId">the category to filter</param>
        /// <param name="statusId">the statud in which the config needs to be</param>
        /// <returns>the list of matching TMVNOConfigActionInfo by MVNOIds</returns>
        IEnumerable<TMVNOConfigActionInfo> GetMVNOConfigsByMvnoIdAndCategoryId(int MVNOId, int categoryId, int statusId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MVNOId">the id to filter the MVNO</param>
        /// <param name="categoryId">the category to filter</param>
        /// <param name="item">The item to filter</param>
        /// <param name="statusId">the statud in which the config needs to be</param>
        /// <returns>the list of matching TMVNOConfigActionInfo by MVNOIds</returns>
        IEnumerable<TMVNOConfigActionInfo> GetMVNOConfigsByMvnoIdAndCategoryIdAndItem(int MVNOId, int categoryId, string item, int statusId);
    }
}
