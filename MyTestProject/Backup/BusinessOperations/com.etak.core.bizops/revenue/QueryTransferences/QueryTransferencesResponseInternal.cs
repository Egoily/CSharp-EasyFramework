using System.Collections.Generic;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryTransferences
{
    /// <summary>
    /// The internal response for QueryTransferences
    /// </summary>
    public class QueryTransferencesResponseInternal : ResponseBase
    {
        /// <summary>
        /// All the Ok operations for the customer, within a time range and a type
        /// </summary>
        public IEnumerable<BusinessOperationExecution> Operations { get; set; }
    }
}
