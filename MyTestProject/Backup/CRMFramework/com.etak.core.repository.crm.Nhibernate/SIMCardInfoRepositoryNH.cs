using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository for entity SIMCardInfo
    /// </summary>
    /// <typeparam name="TSIMCardInfo">the type managed by the repository is or extends SIMCardInfo</typeparam>
    public class SIMCardInfoRepositoryNH<TSIMCardInfo>
        : NHibernateRepository<TSIMCardInfo, String>,
       ISIMCardInfoRepository<TSIMCardInfo> where TSIMCardInfo : SIMCardInfo
    {
        /// <summary>
        /// Gets the simcard by the ICCID
        /// </summary>
        /// <param name="ICCID">The ICCID of the simcard to look for</param>
        /// <returns>the list of simcards</returns>
        public IEnumerable<TSIMCardInfo> GetByICCID(string ICCID)
        {
            return (GetQuery().Where(x => x.ICCID == ICCID).Future());
        }

        /// <summary>
        /// Gets the simcard by the imsi
        /// </summary>
        /// <param name="imsi">the imsi of the simcard to look for</param>
        /// <returns>the list of simcards with that imsi</returns>
        public IEnumerable<TSIMCardInfo> GetByIMSI(string imsi)
        {
            return (GetQuery().Where(x => x.IMSI1 == imsi).Future());
        }    
    }
}
