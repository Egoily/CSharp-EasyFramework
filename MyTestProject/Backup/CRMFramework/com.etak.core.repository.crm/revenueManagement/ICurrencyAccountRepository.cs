using System;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Interface for repository of entity AccountCurrency
    /// </summary>
    /// <typeparam name="TAccountCurrency">The type of the managed entity is or extends AccountCurrency</typeparam>
    public interface IAccountCurrencyRepository<TAccountCurrency> : IRepository<TAccountCurrency, Int64> where TAccountCurrency : AccountCurrency
    {
    }
}
