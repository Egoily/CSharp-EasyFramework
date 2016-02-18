using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.network
{
    /// <summary>
    /// Interface for repository of entity RMOperatorsInfo
    /// </summary>
    /// <typeparam name="TRMOperatorsInfo">The type of the managed entity is or extends RMOperatorsInfo</typeparam>
    public interface IRMOperatorsInfoRepository<TRMOperatorsInfo> : IRepository<TRMOperatorsInfo, string>
            where TRMOperatorsInfo : RMOperatorsInfo
    {
        /// <summary>
        /// Gets all the operators with a given CN operator code, should return only one
        /// </summary>
        /// <param name="cnOperatorCode">the operator code in the central node</param>
        /// <returns>all the operators with a given CN operator code</returns>
        IEnumerable<TRMOperatorsInfo> GetByCNOperatorCode(String cnOperatorCode);
    }
}
