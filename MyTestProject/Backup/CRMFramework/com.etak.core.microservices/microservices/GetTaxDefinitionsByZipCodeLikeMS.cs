using com.etak.core.microservices.messages.GetTaxDefinitionsByZipCodeLike;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm.revenueManagement;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// GetTaxDefinitionsByZipCodeLike Microservice. Will return a list of Taxes with a certain ZipCode
    /// </summary>
    public class GetTaxDefinitionsByZipCodeLikeMS : IMicroService<GetTaxDefinitionsByZipCodeLikeRequest, GetTaxDefinitionsByZipCodeLikeResponse>
    {
        /// <summary>
        /// Main process. Given a ZipCode String, will return a list of Taxes that matches the criteria.
        /// </summary>
        /// <param name="request">contains the ZipCode of the taxes to be returned</param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public GetTaxDefinitionsByZipCodeLikeResponse Process(GetTaxDefinitionsByZipCodeLikeRequest request, operation.RequestInvokationEnvironment invoker)
        {
            var repoTax = RepositoryManager.GetRepository<ITaxDefinitionRepository<TaxDefinition>>();
            var taxDefinitions = repoTax.GetDefinitionsByZipCodeLike(request.ZipCode);

            var response = new GetTaxDefinitionsByZipCodeLikeResponse()
            {
                TaxDefinitions = taxDefinitions,
                ResultType = ResultTypes.Ok,
            };

            return response;
        }
    }
}
