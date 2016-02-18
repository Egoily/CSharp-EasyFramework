using System;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity BalanceForAccount 
    /// </summary>
    /// <typeparam name="TBalanceForAccount">Entity managed by the repository, is or extends BalanceForAccount</typeparam>
    public class BalanceForAccountRepositoryNH<TBalanceForAccount> : 
        NHibernateRepository<TBalanceForAccount, Int64>, 
        IBalanceForAccountRepository<TBalanceForAccount> where TBalanceForAccount : BalanceForAccount
    {
    }
}
