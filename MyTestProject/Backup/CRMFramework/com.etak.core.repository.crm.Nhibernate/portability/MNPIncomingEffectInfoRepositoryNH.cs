using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.portability;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.portability
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity MNPIncomingEffectInfo 
    /// </summary>
    /// <typeparam name="TMNPIncomingEffectInfo">Entity managed by the repository, is or extends MNPIncomingEffectInfo</typeparam>
    public class MNPIncomingEffectInfoNH<TMNPIncomingEffectInfo> :
        NHibernateRepository<TMNPIncomingEffectInfo, Int64>,
         IMNPIncomingEffectInfoRepository<TMNPIncomingEffectInfo> where TMNPIncomingEffectInfo : MNPIncomingEffectInfo
    {
        /// <summary>
        /// Gets all the portabilities of a given MSISDN
        /// </summary>
        /// <param name="msisdn">the msisdn to filter the portabilities</param>
        /// <returns>all the portabilities of a given MSISDN</returns>
        public IEnumerable<TMNPIncomingEffectInfo> GetByMsisdn(string msisdn)
        {
            return (GetQuery().Where(x=> x.TempNO == msisdn).Future());
        }

        /// <summary>
        /// Gets latest portabilities of a given MSISDN
        /// </summary>
        /// <param name="msisdn">the msisdn to filter the portabilities</param>
        /// <returns>the latest portabilities of a given MSISDN</returns>
        public TMNPIncomingEffectInfo GetLatestByPortInMsisdn(string msisdn)
        {
            return GetQuery()
                .Where(x => x.PortedNO == msisdn)
                .OrderBy(e => e.CreateDate).Desc
                .Future()
                .FirstOrDefault();
        }
    }
}
