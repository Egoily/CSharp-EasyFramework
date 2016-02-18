using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TSessionInfo"/> entity
    /// </summary>
    /// <typeparam name="TSessionInfo">The type of the entity managed is or extends SessionInfo</typeparam>
    public interface ISessionInfoRepository<TSessionInfo> : IRepository<TSessionInfo, Int32> where TSessionInfo : SessionInfo
    {
        /// <summary>
        /// Gets the SessionInfo with the specific sessionId
        /// </summary>
        /// <param name="sessionId">the Id of the session</param>
        /// <returns>an enumerable with 0 or 1 SessionInfos</returns>
        IEnumerable<TSessionInfo> GetBySessionId(String sessionId);
    }
}
