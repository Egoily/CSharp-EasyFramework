using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The respository interface for <typeparamref name="TLifecycleLogInfo"/> entity
    /// </summary>
    /// <typeparam name="TLifecycleLogInfo">The entity managed by the interface, is or extends LifecycleLogInfo</typeparam>
    public interface ILifecycleLogInfoRepository<TLifecycleLogInfo> : IRepository<TLifecycleLogInfo, Int64> where TLifecycleLogInfo : LifecycleLogInfo
    {
        /// <summary>
        /// Gets last TLifecycleLogInfo for a given MSISDN
        /// </summary>
        /// <param name="msisdn">the msisdn to look for</param>
        /// <returns>the last TLifecycleLogInfo</returns>
        IEnumerable<TLifecycleLogInfo> GetLastByMSISDN(string msisdn);

        /// <summary>
        /// Gets the TLifecycleLogInfo for the given order code and msisdn
        /// </summary>
        /// <param name="msisdn">the msisdn to look for</param>
        /// <param name="orderCode">the order code</param>
        /// <returns>the entity that fullfill the requirements</returns>
        TLifecycleLogInfo getByOrderCodeAndMSISDN(string msisdn, long orderCode);
    }
}
