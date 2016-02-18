using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryTroubleTicketsByCustomerId
{
    /// <summary>
    /// Response internal of QueryTroubleTicketsByCustomerId
    /// </summary>
    public class QueryTroubleTicketsByCustomerIdResponseInternal : ResponseBase
    {
        /// <summary>
        /// Trouble Ticket Infos for certain customer id
        /// </summary>
        public IEnumerable<TroubleTicketInfo> TroubleTicketInfos { get; set; }
    }
}
