using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.drl
{

    /// <summary>
    /// Respository for <typeparamref name="TRoamingBlackListInfo"/> entity
    /// </summary>
    /// <typeparam name="TRoamingBlackListInfo">The type of the managed entity, is or extends RoamingBlackListInfo</typeparam>
    public interface IRoamingBlackListInfoRepository<TRoamingBlackListInfo> : IRepository<TRoamingBlackListInfo, Int32>
        where TRoamingBlackListInfo : RoamingBlackListInfo
    {
        /// <summary>
        /// Gets all TRoamingBlackListInfo of a given customer
        /// </summary>
        /// <param name="customerId">the id of the customer associated to the TRoamingBlackListInfo</param>
        /// <returns>all TRoamingBlackListInfo of a given customer</returns>
        IEnumerable<TRoamingBlackListInfo> GetByCustomerID(Int32 customerId);
    }
}
