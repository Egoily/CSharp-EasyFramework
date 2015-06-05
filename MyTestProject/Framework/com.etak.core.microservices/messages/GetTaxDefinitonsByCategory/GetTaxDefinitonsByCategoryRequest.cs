using System;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetTaxDefinitonsByCategory
{
    /// <summary>
    /// Request for GetTaxDefinitonsByCategory Microservice
    /// </summary>
    public class GetTaxDefinitonsByCategoryRequest : RequestBase
    {
        /// <summary>
        /// Integer corresponding with the TaxCategory of the taxes returned
        /// </summary>
        public Int32 TaxCategory { get; set; }

    }
}
