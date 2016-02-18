using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.portability
{
    /// <summary>
    /// Repository interface for MNPIncomingEffectInfo
    /// </summary>
    /// <typeparam name="TMNPIncomingEffectInfo">The entity managed by the repository, is or extends MNPIncomingEffectInfo</typeparam>
    public interface IMNPIncomingEffectInfoRepository<TMNPIncomingEffectInfo> : IRepository<TMNPIncomingEffectInfo, Int64>
        where TMNPIncomingEffectInfo : MNPIncomingEffectInfo
    {
        /// <summary>
        /// Gets all the portabilities of a given MSISDN
        /// </summary>
        /// <param name="msisdn">the msisdn to filter the portabilities</param>
        /// <returns>all the portabilities of a given MSISDN</returns>
        IEnumerable<TMNPIncomingEffectInfo> GetByMsisdn(String msisdn);

        /// <summary>
        /// Gets latest portabilities of a given MSISDN
        /// </summary>
        /// <param name="msisdn">the msisdn to filter the portabilities</param>
        /// <returns>the latest portabilities of a given MSISDN</returns>
        TMNPIncomingEffectInfo GetLatestByPortInMsisdn(String msisdn);
    }
}
