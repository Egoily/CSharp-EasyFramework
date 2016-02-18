using System;
using com.etak.core.model;
using com.etak.core.repository.crm.subscription;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CrmMobileMultipleImsiInfo 
    /// </summary>
    /// <typeparam name="TCrmMobileMultipleImsiInfo">Entity managed by the repository, is or extends CrmMobileMultipleImsiInfo</typeparam>
    public class CrmMobileMultipleImsiInfoRepositoryNH<TCrmMobileMultipleImsiInfo> :
        NHibernateRepository<TCrmMobileMultipleImsiInfo,Int32>,
        ICrmMobileMultipleImsiInfoRepository<TCrmMobileMultipleImsiInfo> where TCrmMobileMultipleImsiInfo: CrmMobileMultipleImsiInfo
    {
    }
}
