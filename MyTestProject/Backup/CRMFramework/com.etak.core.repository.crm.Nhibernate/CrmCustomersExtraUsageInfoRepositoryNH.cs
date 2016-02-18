using System;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for CrmCustomersExtraUsageInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TCrmCustomersExtraUsageInfo">the type of entity managed, is or extends CrmCustomersExtraUsageInfo</typeparam>
    public class CrmCustomersExtraUsageInfoRepositoryNH<TCrmCustomersExtraUsageInfo> : 
        NHibernateRepository<TCrmCustomersExtraUsageInfo, Int64>, 
        ICrmCustomersExtraUsageInfoRepository<TCrmCustomersExtraUsageInfo> where TCrmCustomersExtraUsageInfo:CrmCustomersExtraUsageInfo
    {
    }
}
