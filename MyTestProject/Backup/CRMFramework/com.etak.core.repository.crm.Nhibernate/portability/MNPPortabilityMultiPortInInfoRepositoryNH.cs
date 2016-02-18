using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.portability;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.portability
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity MNPPortabilityMultiPortInInfo 
    /// </summary>
    /// <typeparam name="TMNPPortabilityMultiPortInInfo">Entity managed by the repository, is or extends MNPPortabilityMultiPortInInfo</typeparam>
    public class MNPPortabilityMultiPortInInfoRepositoryNH<TMNPPortabilityMultiPortInInfo> :
        NHibernateRepository<TMNPPortabilityMultiPortInInfo, Int64>,
         IMNPPortabilityMultiPortInInfoRepository<TMNPPortabilityMultiPortInInfo> where TMNPPortabilityMultiPortInInfo : MNPPortabilityMultiPortInInfo
    {
        /// <summary>
        /// Gets the MNPPortabilityMultiPortInInfo of a given MSISDN
        /// </summary>
        /// <param name="msisdn">the list of msisdn to filter the MNPPortabilityMultiPortInInfo</param>
        /// <returns>the matching MNPPortabilityMultiPortInInfo of the msisdn</returns>
        public IEnumerable<TMNPPortabilityMultiPortInInfo> GetByMsisdn(string msisdn)
        {
            return GetQuery().Where(x => x.Msisdn == msisdn).Future();
        }
    }
}
