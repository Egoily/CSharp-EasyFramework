using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TCrmCustomersBonusRelationShipInfo"/> entity
    /// </summary>
    /// <typeparam name="TCrmCustomersBonusRelationShipInfo">The type of the entity managed is or extends CrmCustomersBonusRelationShipInfo</typeparam>
    public interface IMVNOPropertiesRepository<TCrmCustomersBonusRelationShipInfo> : IRepository<TCrmCustomersBonusRelationShipInfo, Int32> where TCrmCustomersBonusRelationShipInfo : MVNOPropertiesInfo
    {
        /// <summary>
        /// Gets of TCrmCustomersBonusRelationShipInfo
        /// </summary>
        /// <param name="VMNOId">The filter </param>
        /// <returns>The list of matching TCrmCustomersBonusRelationShipInfo</returns>
        IEnumerable<TCrmCustomersBonusRelationShipInfo> GetByVMNOId(String VMNOId);
    }
}
