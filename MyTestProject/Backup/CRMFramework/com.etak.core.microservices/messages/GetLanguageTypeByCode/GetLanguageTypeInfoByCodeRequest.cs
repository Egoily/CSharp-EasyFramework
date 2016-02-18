using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetLanguageTypeByCode
{
    /// <summary>
    /// Request for GetLanguageTypeByCode Microservice
    /// </summary>
    public class GetLanguageTypeInfoByCodeRequest : RequestBase
    {
        /// <summary>
        /// The id of the language to be returned
        /// </summary>
        public int LanguadeId { get; set; }

    }
}
