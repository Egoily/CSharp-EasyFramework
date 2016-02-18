using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetLanguageTypeByCode
{
    /// <summary>
    /// Get LanguageTypeByCode Response
    /// </summary>
    public class GetLanguageTypeInfoByCodeResponse : ResponseBase
    {
        /// <summary>
        /// LanguageTypeInfo returned by the Microservice
        /// </summary>
        public IEnumerable<LanguageTypeInfo> LanguageTypeInfos { get; set; }

    }
}
