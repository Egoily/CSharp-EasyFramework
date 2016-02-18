using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for CrmCustomersBalanceTransationHistory
    /// </summary>
    /// <typeparam name="TCrmCustomersBalanceTransationHistory">The type of the entity managed by the repository, is or extends CrmCustomersBalanceTransationHistory</typeparam>
    public interface ICrmCustomersBalanceTransationHistoryRepository<TCrmCustomersBalanceTransationHistory> :
        IRepository<TCrmCustomersBalanceTransationHistory, Int64> where TCrmCustomersBalanceTransationHistory : CrmCustomersBalanceTransationHistory
    {
    }
}
