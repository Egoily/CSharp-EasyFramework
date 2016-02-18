using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for PackageInfo
    /// </summary>
    /// <typeparam name="TPackageInfo">The entity managed by the repository, is or extends PackageInfo</typeparam>
    public interface IPackageInfoRepository<TPackageInfo> : IRepository<TPackageInfo, Int32> 
        where TPackageInfo : PackageInfo
    {
        /// <summary>
        /// Gets all the PackagesInfo for a dealer id
        /// </summary>
        /// <param name="dealerId">the dealer owner of the package to retrieve</param>
        /// <returns>the list of packages of the dealer</returns>
        IEnumerable<TPackageInfo> GetPackageInfoForDealerId(int dealerId);       
    }
}
