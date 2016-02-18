using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for BundleInfo
    /// </summary>
    /// <typeparam name="TBundleInfo">The entity managed by the repository, is or extends BundleInfo</typeparam>
    public interface IBundleInfoRepository<TBundleInfo> : IRepository<TBundleInfo, Int32> where TBundleInfo : BundleInfo
    {
        /// <summary>
        /// Gets all Bundle Info
        /// </summary>
        /// <returns>all the BundleInfo</returns>
        IEnumerable<TBundleInfo> GetAll();
       
    }
}
