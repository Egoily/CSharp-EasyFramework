using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.resource
{
    /// <summary>
    /// The respository interface for <typeparamref name="TMVNOAPNIPPoolInfo"/> entity
    /// </summary>
    /// <typeparam name="TMVNOAPNIPPoolInfo">The entity managed by the interface, is or extends MVNOAPNIPPoolInfo</typeparam>
    public interface IMVNOAPNIPPoolInfoRepository<TMVNOAPNIPPoolInfo> : IRepository<TMVNOAPNIPPoolInfo, Int32> where TMVNOAPNIPPoolInfo : MVNOAPNIPPoolInfo
    {
        /// <summary>
        /// Gets all the TMVNOAPNIPPoolInfo for the given msisdn
        /// </summary>
        /// <param name="msisdn">the msisdn to look for</param>
        /// <returns>the list of entities that fullfil the requiremets</returns>
        IEnumerable<TMVNOAPNIPPoolInfo> GetByMsisdn(string msisdn);
    }
}
