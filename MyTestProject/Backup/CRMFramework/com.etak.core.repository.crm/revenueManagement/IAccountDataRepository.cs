using System;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Interface for repository of entity AccountData
    /// </summary>
    /// <typeparam name="TAccountData">The type of the managed entity is or extends AccountData</typeparam>
    public interface IAccountDataRepository<TAccountData> : IRepository<TAccountData, Int64> where TAccountData : AccountData
    {
    }
}
