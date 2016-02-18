using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;

namespace com.etak.core.repository.crm.portability
{
    /// <summary>
    /// Repository Interface for MNPOriginalNrnInfo
    /// </summary>
    public interface IMNPOriginalNrnInfoRepository<TMNPOriginalNrnInfo> : IRepository<TMNPOriginalNrnInfo, Int64>
        where TMNPOriginalNrnInfo : MNPOriginalNrnInfo
    {
        /// <summary>
        /// Gets the Ranges of a given MSISDN
        /// </summary>
        /// <param name="msisdn">the msisdn to filter the portabilities</param>
        /// <returns>all the portabilities of a given MSISDN</returns>
        IEnumerable<TMNPOriginalNrnInfo> GetByMsisdn(String msisdn);
    }
}
