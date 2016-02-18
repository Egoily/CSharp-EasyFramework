using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.topup
{
    /// <summary>
    /// The repository interface for CrmMvnoTopupBonusPromotionInfo
    /// </summary>
    /// <typeparam name="TCrmMvnoTopupBonusPromotionInfo">The type of the entity managed CrmMvnoTopupBonusPromotionInfo</typeparam>
    public interface ICrmMvnoTopupBonusPromotionInfoRepository<TCrmMvnoTopupBonusPromotionInfo> : IRepository<TCrmMvnoTopupBonusPromotionInfo, Int32> 
        where TCrmMvnoTopupBonusPromotionInfo : CrmMvnoTopupBonusPromotionInfo
    {
        /// <summary>
        /// Gets all the bonus promotions for a bonus id
        /// </summary>
        /// <param name="bonusId">the id of the bonus</param>
        /// <returns>The list of CrmMvnoTopupBonusPromotionInfo</returns>
        IEnumerable<TCrmMvnoTopupBonusPromotionInfo> GetBonusPromotionListByBonusId(int bonusId);        
    }
}
