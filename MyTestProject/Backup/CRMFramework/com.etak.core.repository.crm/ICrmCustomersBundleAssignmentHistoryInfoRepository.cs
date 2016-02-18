using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    ///TODO: API IMPROVEMENTS how to write composite key's repository
    public interface ICrmCustomersBundleAssignmentHistoryInfoRepository<TCrmCustomersBundleAssignmentHistoryInfo> :
            IRepository<TCrmCustomersBundleAssignmentHistoryInfo, CrmCustomersBundleAssignmentHistoryPKInfo> where TCrmCustomersBundleAssignmentHistoryInfo : CrmCustomersBundleAssignmentHistoryInfo
    {
    }
}
