using System;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity AccountData 
    /// </summary>
    /// <typeparam name="TAccountData">Entity managed by the repository, is or extends AccountData</typeparam>
    public class AccountDataRepositoryNH<TAccountData> : 
        NHibernateRepository<TAccountData, Int64>, IAccountDataRepository<TAccountData> where TAccountData : AccountData
    {
    }
}
