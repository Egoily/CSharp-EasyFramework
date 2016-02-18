using System;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate implementation of IAccountCurrencyRepository
    /// </summary>
    /// <typeparam name="TAccountCurrency">The type of entity managed by the repository, is or extends  AccountCurrency</typeparam>
    public class AccountCurrencyRepositoryNH<TAccountCurrency> : 
        NHibernateRepository<TAccountCurrency, Int64>,
        IAccountCurrencyRepository<TAccountCurrency> where TAccountCurrency : AccountCurrency
    {
    }
}
