using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.topup
{
    /// <summary>
    /// Repository interface for HistoryInfo
    /// </summary>
    /// <typeparam name="THistoryInfo">The entity managed by the repository, is or extends HistoryInfo</typeparam>
    public interface IHistoryInfoRepository<THistoryInfo> 
        : IRepository<THistoryInfo, Int64>
        where THistoryInfo : HistoryInfo
    {
        /// <summary>
        /// The list of topups of a given ResourceMB in a TimeRange
        /// </summary>
        /// <param name="resourceInfo">The Resource where the topup was done</param>
        /// <param name="startDate">The start date of the range to filter the TopUps</param>
        /// <param name="endDate">The end date of the range to filter the TopUps</param>
        /// <returns>The list of matching topups</returns>
        IEnumerable<THistoryInfo> GetTopUpsWithInDatesOfResource(ResourceMBInfo resourceInfo, DateTime startDate, DateTime endDate);

        /// <summary>
        /// The list of topups of a given ResourceMB in a TimeRange
        /// </summary>
        /// <param name="customerId">The customer that performed the topup</param>
        /// <returns>The list of matching topups</returns>
        IEnumerable<THistoryInfo> GetTopUpHistoryByCustomerId(int customerId);
    }

     
}
