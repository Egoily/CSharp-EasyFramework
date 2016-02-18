using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.topup;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CrmMvnoTopupBonusDetailInfo 
    /// </summary>
    /// <typeparam name="TCrmMvnoTopupBonusDetailInfo">Entity managed by the repository, is or extends CrmMvnoTopupBonusDetailInfo</typeparam>
    public class CrmMvnoTopupBonusDetailInfoRepositoryNH<TCrmMvnoTopupBonusDetailInfo> : 
        NHibernateRepository<TCrmMvnoTopupBonusDetailInfo, Int32>, 
        ICrmMvnoTopupBonusDetailInfoRepository<TCrmMvnoTopupBonusDetailInfo> where TCrmMvnoTopupBonusDetailInfo : CrmMvnoTopupBonusDetailInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all the TCrmMvnoTopupBonusDetailInfo by the bonusId
        /// </summary>
        /// <param name="bonusId">the id of the bonus to filter</param>
        /// <returns></returns>
        public  IEnumerable<TCrmMvnoTopupBonusDetailInfo> GetBonusDetailListByBonusId(int bonusId)
        {
            return GetQuery().Where(x => x.BonusId == bonusId).Cacheable().CacheRegion(CacheRegion).Future();
        }
    }
}
