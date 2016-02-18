using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.resource
{
    /// <summary>
    /// The respository interface for <typeparamref name="TDealerNumberInfo"/> entity
    /// </summary>
    /// <typeparam name="TDealerNumberInfo">The entity managed by the interface, is or extends DealerNumberInfo</typeparam>
    public interface IDealerNumberInfoRepository<TDealerNumberInfo> : IRepository<TDealerNumberInfo, Int32> where TDealerNumberInfo : DealerNumberInfo
    {
        /// <summary>
        /// Gets the dealer number Infor repository
        /// </summary>
        /// <param name="resource">the msisdn to look for</param>
        /// <returns>the list of entities that fullfil the condition</returns>
        IEnumerable<TDealerNumberInfo> GetByResource(string resource);
    }
}
