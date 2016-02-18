using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryUsageDetails
{
    /// <summary>
    /// Response Internal of QueryUsageDetailResponseBizOp
    /// </summary>
    public class QueryUsageDetailsResponseInternal : ResponseBase
    {
        /// <summary>
        /// List of UsageDetailRecord
        /// </summary>
        public virtual IEnumerable<UsageDetailRecord> UsageDetails { get; set; }
    }
}
