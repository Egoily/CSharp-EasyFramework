using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CRMMessageInfo 
    /// </summary>
    /// <typeparam name="TCRMMessageInfo">Entity managed by the repository, is or extends CRMMessageInfo</typeparam>
    public class CRMMessageInfoRepositoryNH<TCRMMessageInfo> : 
        NHibernateRepository<TCRMMessageInfo, Int64>, 
        ICRMMessageInfoRepository<TCRMMessageInfo> where TCRMMessageInfo : CRMMessageInfo
    {

        private const String CacheRegion = CacheRegions.SystemSettingsCacheRegion;


        /// <summary>
        /// Gets all the system messages of a given Id and a languageId
        /// </summary>
        /// <param name="messageId">The id of the message to look for</param>
        /// <param name="languageId">the language of the message to look for</param>
        /// <returns>The list of messages found</returns>
        public IEnumerable<CRMMessageInfo> GetByMessageIdAndLanguageId(long messageId, int languageId)
        {
            return (GetQuery().
                         Where(x => x.MessageID == messageId).And(x => x.LanguageID == languageId).
                         Cacheable().CacheRegion(CacheRegion).
                         Future());
        }

        /// <summary>
        /// Gets a message by it's unique id
        /// </summary>
        /// <param name="messageId">the unique id of the message</param>
        /// <returns>an enumerable with the message</returns>
        public IEnumerable<CRMMessageInfo> GetByMessageId(long messageId)
        {
            return (GetQuery().
                       Where(x => x.MessageID == messageId).
                       Cacheable().CacheRegion(CacheRegion).
                       Future());
        }
      
    }
}
