using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryBalance
{
    /// <summary>
    /// Query Balance Response internal
    /// </summary>
    public class QueryBalanceResponseInternal : ResponseBase
    {
        /// <summary>
        /// The Balance obtained from the customer
        /// </summary>
        public ServicesInfo MasterBundle { get; set; }
    }
}
