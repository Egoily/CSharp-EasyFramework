using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryBalance
{
    /// <summary>
    /// Request Internal for QueryBalance API
    /// </summary>
    public class QueryBalanceRequestInternal : RequestBase, ISubscriptionLastActiveBasedRequest
    {
        /// <summary>
        /// Msisdn of the customer
        /// </summary>
        public string MSISDN { get; set; }
        
        /// <summary>
        /// The subscription associated to the msisdn
        /// </summary>
        public model.ResourceMBInfo Subscription { get; set; }
    }
}
