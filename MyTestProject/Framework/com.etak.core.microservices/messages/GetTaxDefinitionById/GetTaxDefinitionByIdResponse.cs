using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.microservices.messages.GetTaxDefinitionById
{
    /// <summary>
    /// GetTaxDefinitionByIdResponse
    /// </summary>
    public class GetTaxDefinitionByIdResponse: ResponseBase
    {
        /// <summary>
        /// TaxDefinition
        /// </summary>
        public TaxDefinition TaxDefinition { get; set; }
    }
}
