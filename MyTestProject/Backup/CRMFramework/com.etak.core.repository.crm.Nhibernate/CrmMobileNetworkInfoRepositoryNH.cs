using System;
using com.etak.core.model;
using com.etak.core.repository.crm.subscription;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CrmMobileNetWorkInfo 
    /// </summary>
    /// <typeparam name="TCrmMobileNetworkInfo">Entity managed by the repository, is or extends CrmMobileNetWorkInfo</typeparam>
    public class CrmMobileNetworkInfoRepositoryNH<TCrmMobileNetworkInfo> :
        NHibernateRepository<TCrmMobileNetworkInfo,Int32>,
        ICrmMobileNetworkInfoRepository<TCrmMobileNetworkInfo> where TCrmMobileNetworkInfo : CrmMobileNetWorkInfo
    {
    }
}
