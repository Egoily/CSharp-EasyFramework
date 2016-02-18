using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm.topup
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TCrmMVNOPromotedTopupHistoryInfo"/> entity
    /// </summary>
    /// <typeparam name="TCrmMVNOPromotedTopupHistoryInfo">The type of the entity managed is or extends CrmMVNOPromotedTopupHistoryInfo</typeparam>
    public interface ICrmMVNOPromotedTopupHistoryInfoRepository<TCrmMVNOPromotedTopupHistoryInfo> 
        : IRepository<TCrmMVNOPromotedTopupHistoryInfo, Int64> 
        where TCrmMVNOPromotedTopupHistoryInfo : CrmMVNOPromotedTopupHistoryInfo
    {

    }
}
