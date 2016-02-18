using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;

namespace com.etak.core.repository.crm.portability
{
    /// <summary>
    /// Repository Interface for MNPNpdbEsvfInfo
    /// </summary>
    public interface IMNPNpdbEsvfInfoRepository<TMNPNpdbEsvfInfo> : IRepository<TMNPNpdbEsvfInfo, Int64>
        where TMNPNpdbEsvfInfo : MNPNpdbEsvfInfo
    {
        /// <summary>
        /// Gets all the portabilities of a given MSISDN
        /// </summary>
        /// <param name="msisdn">the msisdn to filter the portabilities</param>
        /// <returns>all the portabilities of a given MSISDN</returns>
        IEnumerable<TMNPNpdbEsvfInfo> GetByMsisdn(String msisdn);
    }
}
