using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity SmsLogQueue 
    /// </summary>
    /// <typeparam name="TSmsLogInfo">Entity managed by the repository, is or extends SmsLogQueue</typeparam>
    public class SmsLogInfoRepositoryNH<TSmsLogInfo> : NHibernateRepository<TSmsLogInfo, Int32>, ISmsLogInfoRepository<TSmsLogInfo> where TSmsLogInfo : SmsLogInfo
    {
        /// <summary>
        /// Gets all the sms sent to a given msisdn
        /// </summary>
        /// <param name="msisdn">the msisdn to recover the sms</param>
        /// <returns>the list of sms sent the given msisdn</returns>
        public IEnumerable<TSmsLogInfo> GetSmsLogInfoByMsisdn(string msisdn)
        {
            return GetQuery().Where(x => x.MSISDN == msisdn).Future();
        }
    }
}
