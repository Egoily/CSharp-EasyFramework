using System.Collections.Generic;
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
        /// List of TaxDefinition for the given customer
        /// </summary>
        public List<TaxDefinition> TaxDefinitions { get; set; }
    }
}
