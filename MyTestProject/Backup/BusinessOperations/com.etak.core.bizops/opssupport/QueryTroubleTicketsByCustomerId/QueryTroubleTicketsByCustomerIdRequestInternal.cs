using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryTroubleTicketsByCustomerId
{
    /// <summary>
    /// Request internal of QueryTroubleTicketsByCustomerId
    /// </summary>
    public class QueryTroubleTicketsByCustomerIdRequestInternal : RequestBase, ICustomerBasedRequest
    {
        /// <summary>
        /// CustomerInfo
        /// </summary>
        public model.CustomerInfo Customer { get; set; }
    }
}
