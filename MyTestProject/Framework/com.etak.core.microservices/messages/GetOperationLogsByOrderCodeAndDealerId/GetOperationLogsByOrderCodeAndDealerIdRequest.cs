using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetOperationLogsByOrderCodeAndDealerId
{
    /// <summary>
    /// Request for GetOperationLogsByOrderCodeAndDealerId
    /// </summary>
    public class GetOperationLogsByOrderCodeAndDealerIdRequest : RequestBase
    {
        /// <summary>
        /// OrderCode to be used
        /// </summary>
        public int OrderCode { get; set; }
        /// <summary>
        /// DealerId to be used
        /// </summary>
        public int DealerId { get; set; }

    }
}
