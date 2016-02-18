using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// NHibernate repository Impmenetation of ICrmCustomersBundleAssignmentHistoryInfoRepository
    /// </summary>
    /// <typeparam name="TCrmCustomersBundleAssignmentHistoryInfo">Entity managed by the repository is or extends CrmCustomersBundleAssignmentHistoryInfo</typeparam>
    public class CrmCustomersBundleAssignmentHistoryInfoRepositoryNH<TCrmCustomersBundleAssignmentHistoryInfo> : 
        NHibernateRepository<TCrmCustomersBundleAssignmentHistoryInfo, CrmCustomersBundleAssignmentHistoryPKInfo>, 
        ICrmCustomersBundleAssignmentHistoryInfoRepository<TCrmCustomersBundleAssignmentHistoryInfo> where TCrmCustomersBundleAssignmentHistoryInfo : CrmCustomersBundleAssignmentHistoryInfo
    {
    }
}
