using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetOperationLogsByReferenceCodeAndDealerId
{
    /// <summary>
    /// Request to call the GetOperationLogsByExternalCodeAndDealerId
    /// </summary>
    public class GetOperationLogsByReferenceCodeAndDealerIdRequest : RequestBase
    {
        /// <summary>
        /// ReferenceCode to be used
        /// </summary>
        public string ReferenceCode { get; set; }

        /// <summary>
        /// vMvno Code to be used
        /// </summary>
        public int DealerId { get; set; }

    }
}
