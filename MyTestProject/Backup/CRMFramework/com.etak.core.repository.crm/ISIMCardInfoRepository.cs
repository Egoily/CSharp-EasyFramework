using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The respository interface for <typeparamref name="TSIMCardInfo"/> entity
    /// </summary>
    /// <typeparam name="TSIMCardInfo">The entity managed by the interface, is or extends SIMCardInfo</typeparam>
    public interface ISIMCardInfoRepository<TSIMCardInfo> : IRepository<TSIMCardInfo, String> where TSIMCardInfo : SIMCardInfo
    {
        /// <summary>
        /// Gets the simcard by the ICCID
        /// </summary>
        /// <param name="ICCID">The ICCID of the simcard to look for</param>
        /// <returns>the list of simcards</returns>
        IEnumerable<TSIMCardInfo> GetByICCID(string ICCID);

        /// <summary>
        /// Gets the simcard by the imsi
        /// </summary>
        /// <param name="imsi">the imsi of the simcard to look for</param>
        /// <returns>the list of simcards with that imsi</returns>
        IEnumerable<TSIMCardInfo> GetByIMSI(string imsi);

    }
}
