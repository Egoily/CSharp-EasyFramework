using System.Collections.Generic;
using com.etak.core.model.operation.messages;
using com.etak.core.model.usage;

namespace com.etak.core.bizops.revenue.QueryUsageDetails
{
    /// <summary>
    /// Response DTO of QueryUsageDetailsBizOp
    /// </summary>
    public class QueryUsageDetailsResponseDTO : ResponseBaseDTO
    {
        /// <summary>
        /// Usage Deatils DTO of the customer
        /// </summary>
        public IList<UsageDetailDTO> UsageDetails { get; set; }
    }
}
