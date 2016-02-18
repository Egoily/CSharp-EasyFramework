using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Interface for repository of entity PackageDealerMappingInfo
    /// </summary>
    /// <typeparam name="TPackageDealerMappingInfo">The type of the managed entity is or extends PackageDealerMappingInfo</typeparam>
    public interface IPackageDealerMappingInfoRepository<TPackageDealerMappingInfo> : IRepository<TPackageDealerMappingInfo, int> 
        where TPackageDealerMappingInfo : PackageDealerMappingInfo
    {
        /// <summary>
        /// Get all the mappings for a given package ID
        /// </summary>
        /// <param name="packageId">the Id of the package</param>
        /// <returns>an enumerable with the packages mapping</returns>
        IEnumerable<PackageDealerMappingInfo> GetByPackageId(int packageId);
        
    }
}
