using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for CrmCustomersBonusRelationShipInfo
    /// </summary>
    /// <typeparam name="TCrmCustomersBonusRelationShipInfo">The entity managed by the repository, is or extends CrmCustomersBonusRelationShipInfo</typeparam>
    public interface ICrmCustomersBonusRelationShipInfoRepository<TCrmCustomersBonusRelationShipInfo> : 
        IRepository<TCrmCustomersBonusRelationShipInfo, Int32> where TCrmCustomersBonusRelationShipInfo : CrmCustomersBonusRelationShipInfo
    {
        /// <summary>
        /// Gets all the TCrmCustomersBonusRelationShipInfo for a package and an vmno
        /// </summary>
        /// <param name="VMNOId">vmno to filter the query</param>
        /// <param name="packageID">the owner of the relations</param>
        /// <returns>all the TCrmCustomersBonusRelationShipInfo for a package and an vmno</returns>
        IEnumerable<TCrmCustomersBonusRelationShipInfo> GetByVmnoAndPackage(Int32 VMNOId, Int32 packageID);
    }
}
