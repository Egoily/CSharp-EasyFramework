using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The respository interface for <typeparamref name="TCRMMessageInfo"/> entity
    /// </summary>
    /// <typeparam name="TCRMMessageInfo">The entity managed by the interface, is or extends CRMMessageInfo</typeparam>
    public interface ICRMMessageInfoRepository<TCRMMessageInfo> : IRepository<TCRMMessageInfo, Int64> where TCRMMessageInfo : CRMMessageInfo
    {
        /// <summary>
        /// Gets all the system messages of a given Id and a languageId
        /// </summary>
        /// <param name="messageId">The id of the message to look for</param>
        /// <param name="languageId">the language of the message to look for</param>
        /// <returns>The list of messages found</returns>
        IEnumerable<CRMMessageInfo> GetByMessageIdAndLanguageId(long messageId, int languageId);

        /// <summary>
        /// Gets a message by it's unique id
        /// </summary>
        /// <param name="messageId">the unique id of the message</param>
        /// <returns>an enumerable with the message</returns>
        IEnumerable<CRMMessageInfo> GetByMessageId(long messageId);
    }
}
