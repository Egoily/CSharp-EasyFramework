using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.portability
{
    /// <summary>
    /// NHibernate based repository for inheritance tree of entity MNPSessionInfo
    /// </summary>
    /// <typeparam name="TMNPSessionInfo">The entity managed by the repository, is or extends MNPSessionInfo</typeparam>
    public class MNPSessionInfoRepositoryNH<TMNPSessionInfo> : NHibernateRepository<TMNPSessionInfo, Int32>, IMNPSessionInfoRepository<TMNPSessionInfo>
        where TMNPSessionInfo: MNPSessionInfo
    {
        /// <summary>
        /// Gets the last N sessions for a given operator code.
        /// </summary>
        /// <param name="operatorCode">the code of the operator for the sessions</param>
        /// <param name="n">the number of sessions to recover</param>
        /// <returns>the last N sessions</returns>
        public IEnumerable<TMNPSessionInfo> GetLastNByOperatorCode(string operatorCode, int n)
        {
            return GetQuery().Where(x => x.OperatorCode == operatorCode)
                .OrderBy(x => x.CreateTime).Desc.Take(n)
                .Future();
        }

        /// <summary>
        /// Gets the last N sessions for a given operator code of an user.
        /// </summary>
        /// <param name="operatorCode">the code of the operator for the sessions and user name</param>
        /// <param name="userName">the username of the sessions</param>
        /// <param name="n">the number of sessions to recover</param>
        /// <returns>the last N sessions</returns>
        public IEnumerable<TMNPSessionInfo> GetLastNByOperatorCodeAndUsername(string operatorCode, string userName, int n)
        {
            return GetQuery().Where(x => x.OperatorCode == operatorCode && x.UserName == userName)
                .OrderBy(x => x.CreateTime).Desc.Take(n)
                .Future();
        }
    }
}
