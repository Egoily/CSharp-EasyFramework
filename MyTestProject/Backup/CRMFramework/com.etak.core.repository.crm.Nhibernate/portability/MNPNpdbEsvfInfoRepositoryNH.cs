using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.repository.crm.portability;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity TMNPNpdbEsvfInfo 
    /// </summary>
    /// <typeparam name="TMNPNpdbEsvfInfo">Entity managed by the repository, is or extends TMNPNpdbEsvfInfo</typeparam>
    public class MNPNpdbEsvfInfoRepositoryNH<TMNPNpdbEsvfInfo> :
        NHibernateRepository<TMNPNpdbEsvfInfo, Int64>,
         IMNPNpdbEsvfInfoRepository<TMNPNpdbEsvfInfo> where TMNPNpdbEsvfInfo : MNPNpdbEsvfInfo
    {
        /// <summary>
        /// Gets all the portabilities of a given MSISDN
        /// </summary>
        /// <param name="msisdn">the msisdn to filter the portabilities</param>
        /// <returns>all the portabilities of a given MSISDN</returns>
        public IEnumerable<TMNPNpdbEsvfInfo> GetByMsisdn(string msisdn)
        {
            return (GetQuery().Where(x => x.Msisdn == msisdn).Future());
        }
    }
}
