using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity SmsLogQueue 
    /// </summary>
    /// <typeparam name="TSmsLogQueue">Entity managed by the repository, is or extends SmsLogQueue</typeparam>
    public class SmsLogQueueRepositoryNH<TSmsLogQueue> : NHibernateRepository<TSmsLogQueue, Int32>, ISmsLogQueueRepository<TSmsLogQueue> where TSmsLogQueue : SmsLogQueue
    {
        /// <summary>
        /// Gets the list of TSmsLogQueue for the given list of dealers
        /// </summary>
        /// <param name="dealeridlst">the list of dealers</param>
        /// <returns>the list of SmsLogQueue</returns>
        public IEnumerable<TSmsLogQueue> GetQueueByDealerList(IList<int> dealeridlst)
        {
            return GetQuery().Where(x => x.DealerID != null).AndRestrictionOn(x => x.DealerID).IsIn(dealeridlst.ToArray()).Future();
        }
    }
}
