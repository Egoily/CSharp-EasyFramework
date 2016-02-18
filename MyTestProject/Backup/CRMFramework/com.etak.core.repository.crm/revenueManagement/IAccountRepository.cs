using System;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Interface for repository of entity Account
    /// </summary>
    /// <typeparam name="TAccount">The type of the managed entity is or extends Account</typeparam>
    public interface IAccountRepository<TAccount> : IRepository<TAccount, Int64> where TAccount : Account
    {
    }
}
