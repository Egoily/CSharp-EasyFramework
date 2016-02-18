using com.etak.core.model;
using com.etak.core.repository.crm.services;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.services
{
    /// <summary>
    /// Repository based on NHibernate for TTHistoryInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TTTHistoryInfo">the type of entity managed, is or extends TTHistoryInfo</typeparam>
    public class TTHistoryInfoRepositoryNH<TTTHistoryInfo> : NHibernateRepository<TTTHistoryInfo, int>,
        ITTHistoryInfoRepository<TTTHistoryInfo> where TTTHistoryInfo : TTHistoryInfo
    {

    }
}
