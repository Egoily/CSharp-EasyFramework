using System;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity AccountTime 
    /// </summary>
    /// <typeparam name="TAccountTime">Entity managed by the repository, is or extends AccountTime</typeparam>
    public class AccountTimeRepositoryNH<TAccountTime> : 
        NHibernateRepository<TAccountTime, Int64>, IAccountTimeRepository<TAccountTime> where TAccountTime : AccountTime
    {
    }
}
