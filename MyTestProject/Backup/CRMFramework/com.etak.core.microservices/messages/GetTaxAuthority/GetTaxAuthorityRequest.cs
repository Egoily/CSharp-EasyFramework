using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetTaxAuthority
{
    /// <summary>
    /// SpanishTaxAuthorityRequest
    /// </summary>
    public class GetTaxAuthorityRequest : RequestBase
    {
        /// <summary>
        /// Customer to get the taxDefinitions
        /// </summary>
        public CustomerInfo Customer { get; set; }
    }
}
