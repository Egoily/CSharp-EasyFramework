using com.etak.core.microservices.messages.GetTaxDefinitionById;
using com.etak.core.microservices.messages.GetTaxDefinitonsByCategory;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm.revenueManagement;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// GetTaxDefinitionByIdMS
    /// </summary>
    public class GetTaxDefinitionByIdMS : IMicroService<GetTaxDefinitionByIdRequest, GetTaxDefinitionByIdResponse>
    {
        /// <summary>
        /// Process for GetTaxDefinitionByIdMS, return texdefinition by his id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public GetTaxDefinitionByIdResponse Process(GetTaxDefinitionByIdRequest request, operation.RequestInvokationEnvironment invoker)
        {
            var repoTax = RepositoryManager.GetRepository<ITaxDefinitionRepository<TaxDefinition>>();
            var taxDefinitions = repoTax.GetById(request.TaxDefinitionId);

            var response = new GetTaxDefinitionByIdResponse()
            {
                TaxDefinition = taxDefinitions,
                ResultType = ResultTypes.Ok,
            };

            return response;
        }
    }
}
