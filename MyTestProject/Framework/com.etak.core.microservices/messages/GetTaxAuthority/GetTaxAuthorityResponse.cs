using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.microservices.messages.GetTaxAuthority
{
    /// <summary>
    /// SpanishTaxAuthorityResponse
    /// </summary>
    public class GetTaxAuthorityResponse : ResponseBase
    {
        /// <summary>
        /// TaxDefinition
        /// </summary>
        public TaxDefinition TaxDefinition { get; set; }
    }
}
