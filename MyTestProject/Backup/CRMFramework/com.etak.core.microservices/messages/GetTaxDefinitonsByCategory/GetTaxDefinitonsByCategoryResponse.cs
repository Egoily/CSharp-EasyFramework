using System.Collections.Generic;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.microservices.messages.GetTaxDefinitonsByCategory
{
    /// <summary>
    /// Response for GetTaxDefinitonsByCategory Microservice
    /// </summary>
    public class GetTaxDefinitonsByCategoryResponse : ResponseBase
    {
        /// <summary>
        /// A list of Taxes that matches the criteria
        /// </summary>
        public IEnumerable<TaxDefinition> TaxDefinitions { get; set; }

    }
}
