using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Interface for repository of entity SmsLogQueue
    /// </summary>
    /// <typeparam name="TSmsLogQueue">The type of the managed entity is or extends SmsLogQueue</typeparam>
    public interface ISmsLogQueueRepository<TSmsLogQueue> : IRepository<TSmsLogQueue, int> where TSmsLogQueue : SmsLogQueue
    {
        /// <summary>
        /// Gets the list of TSmsLogQueue for the given list of dealers
        /// </summary>
        /// <param name="dealeridlst">the list of dealers</param>
        /// <returns>the list of SmsLogQueue</returns>
        IEnumerable<TSmsLogQueue> GetQueueByDealerList(IList<int> dealeridlst);
    }
}
