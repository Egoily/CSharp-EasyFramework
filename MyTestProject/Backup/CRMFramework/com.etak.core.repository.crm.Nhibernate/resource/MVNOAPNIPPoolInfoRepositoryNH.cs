using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.resource;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity MVNOAPNIPPoolInfo 
    /// </summary>
    /// <typeparam name="TMVNOAPNIPPoolInfo">Entity managed by the repository, is or extends MVNOAPNIPPoolInfo</typeparam>
    public class MVNOAPNIPPoolInfoRepositoryNH<TMVNOAPNIPPoolInfo>
        : NHibernateRepository<TMVNOAPNIPPoolInfo, Int32>,
          IMVNOAPNIPPoolInfoRepository<TMVNOAPNIPPoolInfo> where TMVNOAPNIPPoolInfo : MVNOAPNIPPoolInfo
    {
        /// <summary>
        /// Gets all the TMVNOAPNIPPoolInfo for the given msisdn
        /// </summary>
        /// <param name="msisdn">the msisdn to look for</param>
        /// <returns>the list of entities that fullfil the requiremets</returns>
        public IEnumerable<TMVNOAPNIPPoolInfo> GetByMsisdn(string msisdn)
        {
            return GetQuery()
                    .Where(e => e.MSISDN == msisdn)
                    .Future();
        }       
    }
}
