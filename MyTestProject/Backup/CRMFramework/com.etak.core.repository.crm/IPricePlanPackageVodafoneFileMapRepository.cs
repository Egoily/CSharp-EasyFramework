using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for PricePlanPackageVodafoneFileMap
    /// </summary>
    /// <typeparam name="TPricePlanPackageVodafoneFileMap">The entity managed by the repository, is or extends PricePlanPackageVodafoneFileMap</typeparam>
    public interface IPricePlanPackageVodafoneFileMapRepository<TPricePlanPackageVodafoneFileMap> : IRepository<TPricePlanPackageVodafoneFileMap, Int32> 
        where TPricePlanPackageVodafoneFileMap : PricePlanPackageVodafoneFileMap
    {
        /// <summary>
        /// Gets all the price plan package mappings for a given dealer.
        /// </summary>
        /// <param name="MVNO">the vmno to get the mappigns of</param>
        /// <returns>the list of mappings</returns>
        IEnumerable<TPricePlanPackageVodafoneFileMap> GetMappingsForVMNO(string MVNO);

        /// <summary>
        /// Gets all the price plan package mappings for a given dealer.
        /// </summary>
        /// <param name="MVNO">the vmno to get the mappigns of</param>
        /// <param name="packageId">the id of the package to get the mapings</param>
        /// <returns>the list of mappings</returns>
        IEnumerable<TPricePlanPackageVodafoneFileMap> GetMappingsForMVNOAndPackageId(string MVNO, int packageId);
    }
}
