using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.topup
{
    /// <summary>
    /// Respository for <typeparamref name="TCrmMvnoTopupBonusInfo"/> entity
    /// </summary>
    /// <typeparam name="TCrmMvnoTopupBonusInfo">The type of the managed entity, is or extends CrmMvnoTopupBonusInfo</typeparam>
    public interface IMvnoTopupBonusInfoRepository<TCrmMvnoTopupBonusInfo> : IRepository<TCrmMvnoTopupBonusInfo, Int32> where TCrmMvnoTopupBonusInfo : CrmMvnoTopupBonusInfo
    {
        /// <summary>
        /// Gets 
        /// </summary>
        /// <param name="dealerIdList"></param>
        /// <returns></returns>
        IEnumerable<TCrmMvnoTopupBonusInfo> GetByDealers(IList<int> dealerIdList);
    }
}
