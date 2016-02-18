using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.portability;
using com.etak.core.repository.NHibernate;
using NHibernate.Criterion;

namespace com.etak.core.repository.crm.Nhibernate.portability
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity MNPPortabilityInfo 
    /// </summary>
    /// <typeparam name="TMNPPortabilityInfo">Entity managed by the repository, is or extends MNPPortabilityInfo</typeparam>
    public class MNPPortabilityInfoRepositoryNH<TMNPPortabilityInfo> : NHibernateRepository<TMNPPortabilityInfo, String>,
         IMNPPortabilityInfoRepository<TMNPPortabilityInfo> where TMNPPortabilityInfo : MNPPortabilityInfo
    {
        /// <summary>
        /// Gets all the MNPPortabilityInfo of the msisdn
        /// </summary>
        /// <param name="msisdn">the msidn to filter the MNPPortabilityInfo</param>
        /// <returns>List of MNPPortabilityInfo of the msisdn</returns>
        public IEnumerable<TMNPPortabilityInfo> GetByMSISDN(string msisdn)
        {
            return (GetQuery().Where(x => x.Msisdn == msisdn).Future());
        }

        /// <summary>
        /// Gets the latest MNPPortabilityInfo of the msisdn
        /// </summary>
        /// <param name="msisdn">the msidn to filter the MNPPortabilityInfo</param>
        /// <returns>Last MNPPortabilityInfo of the msisdn</returns>
        public TMNPPortabilityInfo GetLatestIncomingByMsisdn(string msisdn)
        {
            //TODO: Performance defect: islike 
            TMNPPortabilityInfo tt= GetQuery().AndRestrictionOn(x => x.Msisdn).IsLike(msisdn,MatchMode.Anywhere)
                .Where(x => x.PortabilityDirection == (int)PortabilityDirection.Incoming)
                .OrderBy(e => e.CreateDate)
                .Desc.Future()
                .FirstOrDefault();
            return tt;
        }
    }
}
