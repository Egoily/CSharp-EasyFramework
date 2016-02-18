using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TSmsTemplateInfo"/> entity
    /// </summary>
    /// <typeparam name="TSmsTemplateInfo">The type of the entity managed is or extends SmsTemplateInfo</typeparam>
    public interface ISmsTemplateInfoRepository<TSmsTemplateInfo> : IRepository<TSmsTemplateInfo, Int32> where TSmsTemplateInfo : SmsTemplateInfo
    {
        /// <summary>
        /// Gets a TSmsTemplateInfo by it's templateCode
        /// </summary>
        /// <param name="templateCode">code of the template to retrieve </param>
        /// <returns>the list of TSmsTemplateInfo with the given template code</returns>
        IEnumerable<TSmsTemplateInfo> GetTemplateListByTemplateCode(int templateCode);

        /// <summary>
        /// Gets all the TSmsTemplateInfo of a dealer in the given language
        /// </summary>
        /// <param name="dealerId">the id of the dealer to retrieve the templates of</param>
        /// <param name="languageId">the id of the language to filter the templates</param>
        /// <returns>List of TSmsTemplateInfo with the matching parameters</returns>
        IEnumerable<TSmsTemplateInfo> GetTemplateList(int dealerId, int languageId);
    }
}
