using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The repository interface for MNPSessionInfo
    /// </summary>
    /// <typeparam name="TMNPSession">The type of the entity managed MNPSessionInfo</typeparam>
    public interface IMNPSessionInfoRepository<TMNPSession> : IRepository<TMNPSession, int> where TMNPSession : MNPSessionInfo
    {
        /// <summary>
        /// Gets the last N sessions for a given operator code.
        /// </summary>
        /// <param name="operatorCode">the code of the operator for the sessions</param>
        /// <param name="n">the number of sessions to recover</param>
        /// <returns>the last N sessions</returns>
        IEnumerable<TMNPSession> GetLastNByOperatorCode(String operatorCode, int n);

        /// <summary>
        /// Gets the last N sessions for a given operator code of an user.
        /// </summary>
        /// <param name="operatorCode">the code of the operator for the sessions and user name</param>
        /// <param name="userName">the username of the sessions</param>
        /// <param name="n">the number of sessions to recover</param>
        /// <returns>the last N sessions</returns>
        IEnumerable<TMNPSession> GetLastNByOperatorCodeAndUsername(string operatorCode, string userName, int n);
    }
}
