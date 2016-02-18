using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.subscription
{
    /// <summary>
    /// Repository interface for CrmCustomersResourceMbInfo
    /// </summary>
    /// <typeparam name="TCrmCustomersResourceMbInfo">The entity managed by the repository, is or extends CrmCustomersResourceMbInfo</typeparam>
    public interface ICrmCustomersResourceMbInfoRepository<TCrmCustomersResourceMbInfo> : IRepository<TCrmCustomersResourceMbInfo, int>
        where TCrmCustomersResourceMbInfo : CrmCustomersResourceMbInfo
    {
        /// <summary>
        /// Loads the TCrmCustomersResourceMbInfo of a given resourceId
        /// </summary>
        /// <param name="resourceId">the unique id of the resourceMB</param>
        /// <returns>List with 0 or 1 TCrmCustomersResourceMbInfo</returns>
        IEnumerable<TCrmCustomersResourceMbInfo> LoadResourceMBAsociations(Int32 resourceId);
        /// <summary>
        /// Get TCrmCustomersResourceMbInfo of a given msisdn
        /// </summary>
        /// <param name="msisdn">MSISDN</param>
        /// <returns>List of TCrmCustomersResourceMbInfo</returns>
        IEnumerable<TCrmCustomersResourceMbInfo> GetActiveSubscriptionByMsisdn(string msisdn);
    }
}
