using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.topup
{
    /// <summary>
    /// Repository interface for MVNOTopupBundleBasedInfo
    /// </summary>
    /// <typeparam name="TMVNOTopupBundleBasedInfo">The entity managed by the repository, is or extends MVNOTopupBundleBasedInfo</typeparam>
    public interface IMVNOTopupBundleBasedInfoRepository<TMVNOTopupBundleBasedInfo> : IRepository<TMVNOTopupBundleBasedInfo, MVNOTopupBundleBasedKey> 
        where TMVNOTopupBundleBasedInfo : MVNOTopupBundleBasedInfo
    {
        /// <summary>
        /// Gets the All the Topup Bundles of a given package id
        /// </summary>
        /// <param name="packageId">the id of the package associated to TopUpBundles</param>
        /// <returns>the list of TMVNOTopupBundleBasedInfo </returns>
        IEnumerable<TMVNOTopupBundleBasedInfo> GetByPackageId(Int32 packageId);
    }
}
