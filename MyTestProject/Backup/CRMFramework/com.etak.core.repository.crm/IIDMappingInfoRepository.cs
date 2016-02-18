using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for IDMappingInfo
    /// </summary>
    /// <typeparam name="TIDMappingInfo">The type of the entity managed by the repository, is or extends IDMappingInfo</typeparam>
    public interface IIDMappingInfoRepository<TIDMappingInfo> : IRepository<TIDMappingInfo, Int32> where TIDMappingInfo : IDMappingInfo
    {
        /// <summary>
        /// Gets the TIDMappingInfo for the vmno
        /// </summary>
        /// <param name="Vmno">the vmno that is mapped</param>
        /// <returns>the list of mappings</returns>
        IEnumerable<TIDMappingInfo> GetIdMappingByVmno(Int32 Vmno);

        /// <summary>
        /// Gets all the TIDMappingInfo for a given MVNO and promotion plan name
        /// </summary>
        /// <param name="MVNO">the id of the MVNO/FiscalUnit</param>
        /// <param name="promotionPlanName">the prmotion plan name of the TIDMappingInfo</param>
        /// <returns>the list of matching TIDMappingInfo</returns>
        IEnumerable<TIDMappingInfo> GetByVMNOAndETID(Int32 MVNO, string promotionPlanName);
    }
}
