using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for LifecycleLogInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TLifecycleLogInfo">the type of entity managed, is or extends LifecycleLogInfo</typeparam>
    public class LifecycleLogInfoRepositoryNH<TLifecycleLogInfo>
        : NHibernateRepository<TLifecycleLogInfo, long>,
       ILifecycleLogInfoRepository<TLifecycleLogInfo> where TLifecycleLogInfo : LifecycleLogInfo
    {
        /// <summary>
        /// Gets last TLifecycleLogInfo for a given MSISDN
        /// </summary>
        /// <param name="msisdn">the msisdn to look for</param>
        /// <returns>the last TLifecycleLogInfo</returns>
        public IEnumerable<TLifecycleLogInfo> GetLastByMSISDN(string msisdn)
        {
            return (GetQuery().
                        Where(x => x.MSISDN == msisdn).
                        OrderBy(x => x.TransactionTime).Desc.
                        Take(1).                        
                        Future());
        }


        /// <summary>
        /// Gets the TLifecycleLogInfo for the given order code and msisdn
        /// </summary>
        /// <param name="msisdn">the msisdn to look for</param>
        /// <param name="orderCode">the order code</param>
        /// <returns>the entity that fullfill the requirements</returns>
        public TLifecycleLogInfo getByOrderCodeAndMSISDN(string msisdn, long orderCode)
        {
            return (GetQuery().Where(x => x.MSISDN == msisdn && x.OrderCode == orderCode).SingleOrDefault());
        }

       

       
    }
}
