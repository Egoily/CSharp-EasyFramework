using com.etak.core.model.operation.messages;
using com.etak.core.model.provisioning;

namespace com.etak.core.microservices.messages.CreateHLRRequestErrors
{
    /// <summary>
    /// Request for CreateHLRRequestErrorsMS.
    /// </summary>
    public class CreateHLRRequestErrorsRequest : RequestBase
    {
        /// <summary>
        /// HLRRequestErrors
        /// </summary>
        public HLRRequestErrors HLRRequestErrorsObj { get; set; }

    }
}