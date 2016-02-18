using com.etak.core.microservices.messages.GetTaxDefinitonsByCategory;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm.revenueManagement;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// GetTaxDefinitonsByCategory Microservice. Will return a list of taxes with a certain Category Id
    /// </summary>
    public class GetTaxDefinitonsByCategoryMS : IMicroService<GetTaxDefinitonsByCategoryRequest, GetTaxDefinitonsByCategoryResponse>
    {
        /// <summary>
        /// Main process that given a Tax Category value, returns a list of taxes that match the criteria
        /// </summary>
        /// <param name="request">Contains the caterogy Id of the taxes to be returned</param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public GetTaxDefinitonsByCategoryResponse Process(GetTaxDefinitonsByCategoryRequest request, operation.RequestInvokationEnvironment invoker)
        {
            var repoTax = RepositoryManager.GetRepository<ITaxDefinitionRepository<TaxDefinition>>();
            var taxDefinitions = repoTax.GetDefinitionsForCategory(request.TaxCategory);

            var response = new GetTaxDefinitonsByCategoryResponse()
            {
                TaxDefinitions = taxDefinitions,
                ResultType = ResultTypes.Ok,
            };

            return response;
        }
    }
}
