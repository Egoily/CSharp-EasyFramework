using com.etak.core.model;

namespace com.etak.core.repository.crm.services
{
    /// <summary>
    /// The respository interface for <typeparamref name="TTTHistoryInfo"/> entity
    /// </summary>
    /// <typeparam name="TTTHistoryInfo"></typeparam>
    public interface ITTHistoryInfoRepository<TTTHistoryInfo> : IRepository<TTTHistoryInfo,int> where TTTHistoryInfo : TTHistoryInfo
    {
    }
}
