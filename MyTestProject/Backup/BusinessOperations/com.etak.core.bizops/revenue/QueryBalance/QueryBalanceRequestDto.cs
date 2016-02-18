using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryBalance
{
    /// <summary>
    /// Request Dto for QueryBalance API
    /// </summary>
    public class QueryBalanceRequestDto : RequestBaseDTO, IMsisdnBasedDTORequest
    {
        /// <summary>
        /// The msisdn to be used to get the customer
        /// </summary>
        public string MSISDN { get; set; }
    }
}
