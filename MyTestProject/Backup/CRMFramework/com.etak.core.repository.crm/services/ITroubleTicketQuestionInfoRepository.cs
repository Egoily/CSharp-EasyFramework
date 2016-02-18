using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.services
{
    /// <summary>
    /// The respository interface for <typeparamref name="TTroubleTicketQuestionInfo"/> entity
    /// </summary>
    /// <typeparam name="TTroubleTicketQuestionInfo"></typeparam>
    public interface ITroubleTicketQuestionInfoRepository<TTroubleTicketQuestionInfo> : IRepository<TTroubleTicketQuestionInfo, int> where TTroubleTicketQuestionInfo : TroubleTicketQuestionInfo
    {
        /// <summary>
        /// Get TroubleTicketQuestionInfo by subType and mvnoId
        /// </summary>
        /// <param name="type">Type of TroubleTicket</param>
        /// <param name="subType">Sub Type of TroubleTicket</param>
        /// <param name="mvnoId">MVNO Id</param>
        /// <returns>List of matched TroubleTicketQuestionInfo with given subType and mvno id</returns>
        IEnumerable<TTroubleTicketQuestionInfo> GetByTypeAndSubTypeAndMvnoId(string type, string subType, int mvnoId);
    }
}
