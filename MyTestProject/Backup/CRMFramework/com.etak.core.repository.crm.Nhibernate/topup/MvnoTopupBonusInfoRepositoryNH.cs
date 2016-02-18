using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.topup;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CrmMvnoTopupBonusInfo 
    /// </summary>
    /// <typeparam name="TCrmMvnoTopupBonusInfo">Entity managed by the repository, is or extends CrmMvnoTopupBonusInfo</typeparam>
    public class MvnoTopupBonusInfoRepositoryNH<TCrmMvnoTopupBonusInfo> : 
        NHibernateRepository<TCrmMvnoTopupBonusInfo, Int32>,
        IMvnoTopupBonusInfoRepository<TCrmMvnoTopupBonusInfo> where TCrmMvnoTopupBonusInfo:CrmMvnoTopupBonusInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets 
        /// </summary>
        /// <param name="dealerIdList"></param>
        /// <returns></returns>
        public IEnumerable<TCrmMvnoTopupBonusInfo> GetByDealers(IList<int> dealerIdList)
        {
            var list =  GetQuery().Cacheable().CacheRegion(CacheRegion).Future();
            return list.Where(x => dealerIdList.Contains(x.DealerId));
        }
    }
}
