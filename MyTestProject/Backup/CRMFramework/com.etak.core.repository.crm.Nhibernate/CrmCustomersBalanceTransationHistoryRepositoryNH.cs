using System;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for CrmCustomersBalanceTransationHistory entity inheritance tree
    /// </summary>
    /// <typeparam name="TCrmCustomersBalanceTransationHistory">the type of entity managed, is or extends CrmCustomersBalanceTransationHistory</typeparam>
    public class CrmCustomersBalanceTransationHistoryRepositoryNH<TCrmCustomersBalanceTransationHistory> : 
        NHibernateRepository<TCrmCustomersBalanceTransationHistory, Int64>,
        ICrmCustomersBalanceTransationHistoryRepository<TCrmCustomersBalanceTransationHistory> where TCrmCustomersBalanceTransationHistory : CrmCustomersBalanceTransationHistory
    {
    }
}
