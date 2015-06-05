using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetTaxDefinitionById
{
    /// <summary>
    /// GetTaxDefinitionByIdRequest
    /// </summary>
    public class GetTaxDefinitionByIdRequest: RequestBase
    {
        /// <summary>
        /// TaxDefinitionId
        /// </summary>
        public int TaxDefinitionId { get; set; }
    }
}
