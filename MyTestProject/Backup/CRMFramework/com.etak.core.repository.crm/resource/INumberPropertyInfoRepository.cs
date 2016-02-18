using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.resource
{
    /// <summary>
    /// Repository interface for NumberPropertyInfo
    /// </summary>
    /// <typeparam name="TNumberPropertyInfo">The entity managed by the repository, is or extends NumberPropertyInfo</typeparam>
    public interface INumberPropertyInfoRepository<TNumberPropertyInfo> 
        : IRepository<TNumberPropertyInfo, String>
            where TNumberPropertyInfo : NumberPropertyInfo
    {
        /// <summary>
        /// Gets the Number in the pool by the MSISDN 
        /// </summary>
        /// <param name="msisdn">the msisdn too look up</param>
        /// <returns></returns>
        IEnumerable<TNumberPropertyInfo> getByMSISDN(String msisdn);
    }
}
