using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.portability
{
    /// <summary>
    /// Respository for <typeparamref name="TMNPPortabilityMultiPortInInfo"/> entity
    /// </summary>
    /// <typeparam name="TMNPPortabilityMultiPortInInfo">The type of the managed entity, is or extends MNPPortabilityMultiPortInInfo</typeparam>
    public interface IMNPPortabilityMultiPortInInfoRepository<TMNPPortabilityMultiPortInInfo> : IRepository<TMNPPortabilityMultiPortInInfo, Int64>
        where TMNPPortabilityMultiPortInInfo : MNPPortabilityMultiPortInInfo
    {
        /// <summary>
        /// Gets the MNPPortabilityMultiPortInInfo of a given MSISDN
        /// </summary>
        /// <param name="msisdn">the list of msisdn to filter the MNPPortabilityMultiPortInInfo</param>
        /// <returns>the matching MNPPortabilityMultiPortInInfo of the msisdn</returns>
        IEnumerable<TMNPPortabilityMultiPortInInfo> GetByMsisdn(String msisdn);
    }
}
