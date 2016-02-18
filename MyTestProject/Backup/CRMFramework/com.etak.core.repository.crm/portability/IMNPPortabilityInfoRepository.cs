using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.portability
{
    /// <summary>
    /// Repository interface for MNPPortabilityInfo
    /// </summary>
    /// <typeparam name="TMNPPortabilityInfo">The entity managed by the repository, is or extends MNPPortabilityInfo</typeparam>
    public interface IMNPPortabilityInfoRepository<TMNPPortabilityInfo> : IRepository<TMNPPortabilityInfo, String>
        where TMNPPortabilityInfo : MNPPortabilityInfo
    {
        /// <summary>
        /// Gets all the MNPPortabilityInfo of the msisdn
        /// </summary>
        /// <param name="msisdn">the msidn to filter the MNPPortabilityInfo</param>
        /// <returns>List of MNPPortabilityInfo of the msisdn</returns>
        IEnumerable<TMNPPortabilityInfo> GetByMSISDN(String msisdn);

        /// <summary>
        /// Gets the latest MNPPortabilityInfo of the msisdn
        /// </summary>
        /// <param name="msisdn">the msidn to filter the MNPPortabilityInfo</param>
        /// <returns>Last MNPPortabilityInfo of the msisdn</returns>
        TMNPPortabilityInfo GetLatestIncomingByMsisdn(string msisdn);
    }
}
