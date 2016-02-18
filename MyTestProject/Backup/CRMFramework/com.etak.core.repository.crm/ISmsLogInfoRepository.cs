using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TSmsLogInfo"/> entity
    /// </summary>
    /// <typeparam name="TSmsLogInfo">The type of the entity managed is or extends SmsLogInfo</typeparam>
    public interface ISmsLogInfoRepository<TSmsLogInfo> : IRepository<TSmsLogInfo, Int32> where TSmsLogInfo : SmsLogInfo
    {
        /// <summary>
        /// Gets all the sms sent to a given msisdn
        /// </summary>
        /// <param name="msisdn">the msisdn to recover the sms</param>
        /// <returns>the list of sms sent the given msisdn</returns>
        IEnumerable<TSmsLogInfo> GetSmsLogInfoByMsisdn(string msisdn);
    }
}
