using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.services
{
    /// <summary>
    /// The respository interface for <typeparamref name="TTroubleTicketInfo"/> entity
    /// </summary>
    /// <typeparam name="TTroubleTicketInfo">The entity managed by the interface, is or extends SIMCardInfo</typeparam>
    public interface ITroubleTicketInfoRepository<TTroubleTicketInfo> : IRepository<TTroubleTicketInfo,int> where TTroubleTicketInfo : TroubleTicketInfo
    {
        /// <summary>
        /// Get TroubleTicketInfos for a given customerId
        /// </summary>
        /// <param name="customerId">Id of customer to be retrieved</param>
        /// <returns>List of matching entities that fulfill the condition</returns>
        IEnumerable<TTroubleTicketInfo> GetByCustomerId(int customerId);

        /// <summary>
        /// Get TroubleTicketInfos for a given ticket number
        /// </summary>
        /// <param name="ticketNumber">Ticket number of customer to be retrieved</param>
        /// <returns>Matched entity that fulfill the condition</returns>
        TTroubleTicketInfo GetByTicketNumber(string ticketNumber);
    }
}
