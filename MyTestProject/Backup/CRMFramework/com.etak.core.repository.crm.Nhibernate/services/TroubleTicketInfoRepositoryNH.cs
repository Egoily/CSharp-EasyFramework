using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.services;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.services
{
    /// <summary>
    /// Repository based on NHibernate for TroubleTicketInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TTroubleTicketInfo">the type of entity managed, is or extends TroubleTicketInfo</typeparam>
    public class TroubleTicketInfoRepositoryNH<TTroubleTicketInfo> : NHibernateRepository<TTroubleTicketInfo, int>, ITroubleTicketInfoRepository<TTroubleTicketInfo> where TTroubleTicketInfo : TroubleTicketInfo
    {
        /// <summary>
        /// Get the TroubleTicketInfos by Customer Id
        /// </summary>
        /// <param name="customerId">The Id of the customer</param>
        /// <returns>List of matching entitiesTroubleTicketInfo that fulfill the condition</returns>
        public IEnumerable<TTroubleTicketInfo> GetByCustomerId(int customerId)
        {
            return (GetQuery().Where(x=>x.CUSTOMERID == customerId).Future());
        }

        /// <summary>
        /// Get TroubleTicketInfos for a given ticket number
        /// </summary>
        /// <param name="ticketNumber">Ticket number of customer to be retrieved</param>
        /// <returns>Matched entity that fulfill the condition</returns>
        public TTroubleTicketInfo GetByTicketNumber(string ticketNumber)
        {
            return (GetQuery().Where(x => x.TICKETNUMBER == ticketNumber).Future()).FirstOrDefault();
        }
    }
}
