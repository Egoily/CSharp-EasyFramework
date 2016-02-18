using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.topup;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for HistoryInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="THistoryInfo">the type of entity managed, is or extends HistoryInfo</typeparam>
    public class HistoryInfoRepositoryNH<THistoryInfo>
        : NHibernateRepository<THistoryInfo, Int64>,
        IHistoryInfoRepository<THistoryInfo> where THistoryInfo : HistoryInfo
    {
        /// <summary>
        /// The list of topups of a given ResourceMB in a TimeRange
        /// </summary>
        /// <param name="resourceInfo">The Resource where the topup was done</param>
        /// <param name="startDate">The start date of the range to filter the TopUps</param>
        /// <param name="endDate">The end date of the range to filter the TopUps</param>
        /// <returns>The list of matching topups</returns>
        public IEnumerable<THistoryInfo> GetTopUpsWithInDatesOfResource(ResourceMBInfo resourceInfo, DateTime startDate, DateTime endDate)
        {
            return GetQuery().Where(ee => ee.CustomerID == resourceInfo.CustomerInfo.CustomerID 
                    && ee.StatusDate > startDate && ee.StatusDate < endDate)
                .TransformUsing(global::NHibernate.Transform.Transformers.DistinctRootEntity).Future();
        }

        /// <summary>
        /// The list of topups of a given ResourceMB in a TimeRange
        /// </summary>
        /// <param name="customerId">The customer that performed the topup</param>
        /// <returns>The list of matching topups</returns>
        public IEnumerable<THistoryInfo> GetTopUpHistoryByCustomerId(int customerId)
        {

            IEnumerable<THistoryInfo> enumResult  = GetQuery().
                Where(his => his.CustomerID == customerId)
                .OrderBy(his => his.StatusDate).Asc.Future();

            return enumResult;
          
        }
    }
}
