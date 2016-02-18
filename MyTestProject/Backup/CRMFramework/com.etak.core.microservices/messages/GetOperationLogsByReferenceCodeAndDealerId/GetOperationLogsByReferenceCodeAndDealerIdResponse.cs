using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetOperationLogsByReferenceCodeAndDealerId
{
    /// <summary>
    /// Response for GetOperationLogsByExternalCodeAndDealerId
    /// </summary>
    public class GetOperationLogsByReferenceCodeAndDealerIdResponse : ResponseBase
    {
        /// <summary>
        /// A list with all the Operation Logs that match with the criteria
        /// </summary>
        public IEnumerable<OperationLog> OperationLogs { get; set; }

    }
}
