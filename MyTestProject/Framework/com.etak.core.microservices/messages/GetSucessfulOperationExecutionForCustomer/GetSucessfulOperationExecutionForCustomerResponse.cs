using System.Collections.Generic;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetSucessfulOperationExecutionForCustomer
{
    /// <summary>
    /// The response containing the Operations that matched the criteria
    /// </summary>
    public class GetSucessfulOperationExecutionForCustomerResponse : ResponseBase
    {
        /// <summary>
        /// The list of operations that matched the criterias
        /// </summary>
        public IEnumerable<BusinessOperationExecution> Operations { get; set; }
    }
}
