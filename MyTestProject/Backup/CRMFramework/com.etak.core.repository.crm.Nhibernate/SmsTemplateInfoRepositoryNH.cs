using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity SmsTemplateInfo 
    /// </summary>
    /// <typeparam name="TSmsTemplateInfo">Entity managed by the repository, is or extends SmsTemplateInfo</typeparam>
    public class SmsTemplateInfoRepositoryNH<TSmsTemplateInfo> :
        NHibernateRepository<TSmsTemplateInfo, Int32>, 
        ISmsTemplateInfoRepository<TSmsTemplateInfo> where TSmsTemplateInfo : SmsTemplateInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets a TSmsTemplateInfo by it's templateCode
        /// </summary>
        /// <param name="templateCode">code of the template to retrieve </param>
        /// <returns>the list of TSmsTemplateInfo with the given template code</returns>
        public IEnumerable<TSmsTemplateInfo> GetTemplateListByTemplateCode(int templateCode)
        {
            return GetQuery().Where(ee => ee.CODE == templateCode).Cacheable().CacheRegion(CacheRegion).Future();
        }

        /// <summary>
        /// Gets all the TSmsTemplateInfo of a dealer in the given language
        /// </summary>
        /// <param name="dealerId">the id of the dealer to retrieve the templates of</param>
        /// <param name="languageId">the id of the language to filter the templates</param>
        /// <returns>List of TSmsTemplateInfo with the matching parameters</returns>
        public IEnumerable<TSmsTemplateInfo> GetTemplateList(int dealerId, int languageId)
        {
            return GetQuery().Where(ee => ee.MVNOID == dealerId && ee.LANGUAGEID == languageId).
                Cacheable().CacheRegion(CacheRegion).Future();
        }

    }
}
