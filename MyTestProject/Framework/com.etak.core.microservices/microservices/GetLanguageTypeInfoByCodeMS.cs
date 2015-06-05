using com.etak.core.microservices.messages.GetLanguageTypeByCode;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// GetLanguangeTypeByCode Microservice
    /// </summary>
    public class GetLanguageTypeInfoByCodeMS : IMicroService<GetLanguageTypeInfoByCodeRequest, GetLanguageTypeInfoByCodeResponse>
    {
        /// <summary>
        /// Main process of the microservice to get the languageTypeInfo
        /// </summary>
        /// <param name="request"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public GetLanguageTypeInfoByCodeResponse Process(GetLanguageTypeInfoByCodeRequest request, operation.RequestInvokationEnvironment invoker)
        {
            var repoLanguageType = RepositoryManager.GetRepository<ILanguageTypeInfoRepository<LanguageTypeInfo>>();
            var languageTypes = repoLanguageType.GetAllLanguageById(request.LanguadeId);

            var response = new GetLanguageTypeInfoByCodeResponse()
            {
                LanguageTypeInfos = languageTypes,
                ResultType = ResultTypes.Ok,
            };

            return response;
        }
    }
}
