
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetTaxDefinitionsByZipCodeLike
{
    /// <summary>
    /// Request for GetTaxDefinitionsByZipCodeLike Request
    /// </summary>
    public class GetTaxDefinitionsByZipCodeLikeRequest : RequestBase
    {
        /// <summary>
        /// ZipCode to be used to get the ZipCodes
        /// </summary>
        public string ZipCode { get; set; }
    }
}
