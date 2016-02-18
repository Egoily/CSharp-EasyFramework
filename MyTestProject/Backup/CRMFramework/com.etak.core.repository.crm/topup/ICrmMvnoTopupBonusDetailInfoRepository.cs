using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.topup
{
    /// <summary>
    /// Repository interface for CrmMvnoTopupBonusDetailInfo
    /// </summary>
    /// <typeparam name="TCrmMvnoTopupBonusDetailInfo">The entity managed by the repository, is or extends CrmMvnoTopupBonusDetailInfo</typeparam>
    public interface ICrmMvnoTopupBonusDetailInfoRepository<TCrmMvnoTopupBonusDetailInfo> : IRepository<TCrmMvnoTopupBonusDetailInfo, Int32> 
        where TCrmMvnoTopupBonusDetailInfo : CrmMvnoTopupBonusDetailInfo
    {
        /// <summary>
        /// Gets all the TCrmMvnoTopupBonusDetailInfo by the bonusId
        /// </summary>
        /// <param name="bonusId">the id of the bonus to filter</param>
        /// <returns></returns>
        IEnumerable<TCrmMvnoTopupBonusDetailInfo> GetBonusDetailListByBonusId(int bonusId);
    }
}
