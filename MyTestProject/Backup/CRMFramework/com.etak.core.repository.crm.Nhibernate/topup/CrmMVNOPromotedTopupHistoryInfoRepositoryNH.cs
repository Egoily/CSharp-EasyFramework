using System;
using com.etak.core.model;
using com.etak.core.repository.crm.topup;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for CrmMVNOPromotedTopupHistoryInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TCrmMVNOPromotedTopupHistoryInfo">the type of entity managed, is or extends CrmMVNOPromotedTopupHistoryInfo</typeparam>
    public class CrmMVNOPromotedTopupHistoryInfoRepositoryNH<TCrmMVNOPromotedTopupHistoryInfo> :
        NHibernateRepository<TCrmMVNOPromotedTopupHistoryInfo, Int64>,
        ICrmMVNOPromotedTopupHistoryInfoRepository<TCrmMVNOPromotedTopupHistoryInfo> 
        where TCrmMVNOPromotedTopupHistoryInfo : CrmMVNOPromotedTopupHistoryInfo
    {


        
    }
}
