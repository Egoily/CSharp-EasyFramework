using System;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TBalanceForAccount"/> entity
    /// </summary>
    /// <typeparam name="TBalanceForAccount">The type of the entity managed is or extends BalanceForAccount</typeparam>
    public interface IBalanceForAccountRepository<TBalanceForAccount> : IRepository<TBalanceForAccount, Int64> where TBalanceForAccount : BalanceForAccount
    {
    }
}
