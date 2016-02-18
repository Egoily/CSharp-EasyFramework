using com.etak.core.model.operation.messages;
using com.etak.core.model.provisioning;

namespace com.etak.core.microservices.messages.CreateHLRRequestErrors
{
    /// <summary>
    /// response for CreateHLRRequestErrorsMS
    /// </summary>
    public class CreateHLRRequestErrorsResponse : ResponseBase
    {
        /// <summary>
        /// return result of CreateHLRRequestErrorsMS as HLRRequestErrors
        /// </summary>
        public virtual HLRRequestErrors HLRRequestErrorsObj { get; set; }

    }
}