using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.services;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.services
{
    /// <summary>
    /// Repository based on NHibernate for TroubleTicketQuestionInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TTroubleTicketQuestionInfo">the type of entity managed, is or extends TroubleTicketQuestionInfo</typeparam>
    public class TroubleTicketQuestionInfoRepositoryNH<TTroubleTicketQuestionInfo> : NHibernateRepository<TTroubleTicketQuestionInfo, int>,
        ITroubleTicketQuestionInfoRepository<TTroubleTicketQuestionInfo> where TTroubleTicketQuestionInfo : TroubleTicketQuestionInfo
    {
        /// <summary>
        /// Get the TroubleTicketQuestionInfos by TROUBLETYPE, TTSUBTYPE, and MVNOID
        /// </summary>
        /// <param name="type">Type of Trouble Ticket</param>
        /// <param name="subType">SubType of Trouble Ticket</param>
        /// <param name="mvnoId">MVNO Id</param>
        /// <returns>List of matched TTroubleTicketQuestionInfo with given type, subType, and mvnoId</returns>
        public IEnumerable<TTroubleTicketQuestionInfo> GetByTypeAndSubTypeAndMvnoId(string type, string subType, int mvnoId)
        {
            return (GetQuery().Where(x=>x.TROUBLETYPE==type && x.TTSUBTYPE==subType && x.MVNOID==mvnoId).Future());
        }
    }
}
