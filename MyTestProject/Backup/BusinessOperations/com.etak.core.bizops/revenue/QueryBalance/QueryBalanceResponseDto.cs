using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryBalance
{
    /// <summary>
    /// Response Dto for QueryBalance
    /// </summary>
    public class QueryBalanceResponseDto : ResponseBaseDTO
    {
        /// <summary>
        /// The Balance of the Customer
        /// </summary>
        public decimal Balance { get; set; }
    }
}
