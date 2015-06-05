using System.Collections.Generic;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.microservices.messages.GetTaxDefinitionsByZipCodeLike
{
    /// <summary>
    /// Response for GetTaxDefinitionsByZipCodeLike Microservice
    /// </summary>
    public class GetTaxDefinitionsByZipCodeLikeResponse : ResponseBase
    {
        /// <summary>
        /// A list of TaxDefinitions tat match the criteria
        /// </summary>
        public IEnumerable<TaxDefinition> TaxDefinitions { get; set; }
    }
}
