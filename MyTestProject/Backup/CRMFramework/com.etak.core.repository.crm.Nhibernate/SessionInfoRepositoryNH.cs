using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
     
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity SessionInfo 
    /// </summary>
    /// <typeparam name="TSessionInfo">Entity managed by the repository, is or extends SessionInfo</typeparam>
    public class SessionInfoRepositoryNH<TSessionInfo>
        : NHibernateRepository<TSessionInfo, Int32>, //Extends and gets basic CRUD operations
            ISessionInfoRepository<TSessionInfo> where TSessionInfo : SessionInfo //Implementes 
    {
        /// <summary>
        /// Gets all the SessionInfo with specific sessionId
        /// </summary>
        /// <param name="sessionId">the Id of the session</param>
        /// <returns>an enumerable with 0 or 1 SessionInfos</returns>de
        public IEnumerable<TSessionInfo> GetBySessionId(String sessionId)
        {
            return (GetQuery().Where(x => x.SessionID == sessionId).
                Future());
        }
    }
}
