using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.topup;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CrmMvnoTopupBonusPromotionInfo 
    /// </summary>
    /// <typeparam name="TCrmMvnoTopupBonusPromotionInfo">Entity managed by the repository, is or extends CrmMvnoTopupBonusPromotionInfo</typeparam>
    public class CrmMvnoTopupBonusPromotionInfoRepositoryNH<TCrmMvnoTopupBonusPromotionInfo> : 
        NHibernateRepository<TCrmMvnoTopupBonusPromotionInfo, Int32>, 
        ICrmMvnoTopupBonusPromotionInfoRepository<TCrmMvnoTopupBonusPromotionInfo> where TCrmMvnoTopupBonusPromotionInfo : CrmMvnoTopupBonusPromotionInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all the bonus promotions for a bonus id
        /// </summary>
        /// <param name="bonusId">the id of the bonus</param>
        /// <returns>The list of CrmMvnoTopupBonusPromotionInfo</returns>
        public IEnumerable<TCrmMvnoTopupBonusPromotionInfo> GetBonusPromotionListByBonusId(int bonusId)
        {
            return GetQuery().Where(ee => ee.BonusId == bonusId).Cacheable().CacheRegion(CacheRegion).Future();
        }
    }
}
