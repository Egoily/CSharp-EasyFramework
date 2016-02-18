using System;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity Account 
    /// </summary>
    /// <typeparam name="TAccount">Entity managed by the repository, is or extends Account</typeparam>
    public class AccountRepositoryNH<TAccount> : 
        NHibernateRepository<TAccount, Int64>, 
        IAccountRepository<TAccount> where TAccount : Account
    {
    }
}
