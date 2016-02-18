using System;
using com.etak.core.microservices.messages.GetTaxAuthority;
using com.etak.core.operation.contract;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// SpanishTaxAuthorityMS
    /// </summary>
    public class GetTaxAuthorityMS: IMicroService<GetTaxAuthorityRequest,GetTaxAuthorityResponse>
    {
        /// <summary>
        /// Process for SpanishTaxAuthorityMS
        /// </summary>
        /// <param name="request"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public GetTaxAuthorityResponse Process(GetTaxAuthorityRequest request, operation.RequestInvokationEnvironment invoker)
        {
            throw new NotImplementedException();
        }
    }
}
