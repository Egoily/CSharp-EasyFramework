using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetTaxAuthority
{
    /// <summary>
    /// SpanishTaxAuthorityRequest
    /// </summary>
    public class GetTaxAuthorityRequest : RequestBase
    {
        /// <summary>
        /// CustomerId
        /// </summary>
        public int CustomerId { get; set; }
    }
}
