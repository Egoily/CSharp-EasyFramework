using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetOperationLogsByOrderCodeAndDealerId
{
    /// <summary>
    /// Response for GetOperationLogsByOrderCodeAndDealerId
    /// </summary>
    public class GetOperationLogsByOrderCodeAndDealerIdResponse : ResponseBase
    {
        /// <summary>
        /// List of all the OperationLogs that matchs with the criteria
        /// </summary>
        public IEnumerable<OperationLog> OperationLogs { get; set; }

    }
}
