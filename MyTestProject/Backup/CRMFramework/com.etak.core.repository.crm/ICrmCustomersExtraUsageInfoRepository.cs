using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Interface for repository of entity CrmCustomersExtraUsageInfo
    /// </summary>
    /// <typeparam name="TCrmCustomersExtraUsageInfo">The type of the managed entity is or extends CrmCustomersExtraUsageInfo</typeparam>
    public interface ICrmCustomersExtraUsageInfoRepository<TCrmCustomersExtraUsageInfo> 
        : IRepository<TCrmCustomersExtraUsageInfo, long> where TCrmCustomersExtraUsageInfo : CrmCustomersExtraUsageInfo
    {
    }
}
