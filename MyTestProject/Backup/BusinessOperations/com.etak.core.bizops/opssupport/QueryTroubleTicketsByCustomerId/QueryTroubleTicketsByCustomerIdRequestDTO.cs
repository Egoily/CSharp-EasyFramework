using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryTroubleTicketsByCustomerId
{
    /// <summary>
    /// Request in DTO form of QueryTroubleTicketsByCustomerId
    /// </summary>
    public class QueryTroubleTicketsByCustomerIdRequestDTO : RequestBaseDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// Customer Id needs to be queried
        /// </summary>
        public int CustomerId { get; set; }
    }
}
