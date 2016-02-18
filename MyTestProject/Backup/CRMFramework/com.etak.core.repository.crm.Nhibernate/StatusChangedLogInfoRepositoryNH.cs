
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for StatusChangedLogInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TStatusChangedLogInfo">the type of entity managed, is or extends StatusChangedLogInfo</typeparam>
    public class StatusChangedLogInfoRepositoryNH<TStatusChangedLogInfo> :
        NHibernateRepository<TStatusChangedLogInfo, long>,  
                      IStatusChangedLogInfoRepository<TStatusChangedLogInfo> 
                        where TStatusChangedLogInfo : StatusChangedLogInfo
    {
    }
}
