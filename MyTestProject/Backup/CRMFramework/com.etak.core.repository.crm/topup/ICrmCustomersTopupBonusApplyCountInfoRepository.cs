using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm.topup
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TCrmCustomersTopupBonusApplyCountInfo"/> entity
    /// </summary>
    /// <typeparam name="TCrmCustomersTopupBonusApplyCountInfo">The type of the entity managed is or extends CrmCustomersTopupBonusApplyCountInfo</typeparam>
    public interface ICrmCustomersTopupBonusApplyCountInfoRepository<TCrmCustomersTopupBonusApplyCountInfo> :
        IRepository<TCrmCustomersTopupBonusApplyCountInfo, Int32> where TCrmCustomersTopupBonusApplyCountInfo : CrmCustomersTopupBonusApplyCountInfo
    {
        /// <summary>
        /// Gets the TCrmCustomersTopupBonusApplyCountInfo
        /// </summary>
        /// <param name="resource">The resource to filter the TCrmCustomersTopupBonusApplyCountInfo</param>
        /// <param name="bonusId">The Id of the bonus to filter the TCrmCustomersTopupBonusApplyCountInfo</param>
        /// <param name="year">The year to filter the TCrmCustomersTopupBonusApplyCountInfo</param>
        /// <param name="month">The month to filter the TCrmCustomersTopupBonusApplyCountInfo</param>
        /// <returns>the matching TCrmCustomersTopupBonusApplyCountInfo</returns>
        TCrmCustomersTopupBonusApplyCountInfo GetApplyCount(string resource, int bonusId, string year, string month);
        
    }
}
