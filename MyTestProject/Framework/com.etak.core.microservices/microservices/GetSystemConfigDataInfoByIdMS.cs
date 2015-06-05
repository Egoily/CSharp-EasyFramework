using com.etak.core.microservices.messages.GetSystemConfigDataInfoById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm.configuration;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// GetSystemConfigDataInfoById Microservice
    /// </summary>
    public class GetSystemConfigDataInfoByIdMS : IMicroService<GetSystemConfigDataInfoByIdRequest, GetSystemConfigDataInfoByIdResponse>
    {
        /// <summary>
        /// Process of the microservice to get the SystemConfigDataInfo
        /// </summary>
        /// <param name="request">Contains the SystemConfigDataInfo Id</param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public GetSystemConfigDataInfoByIdResponse Process(GetSystemConfigDataInfoByIdRequest request, operation.RequestInvokationEnvironment invoker)
        {
            var repoSysConfigData = RepositoryManager.GetRepository<ISystemConfigDataInfoRepository<SystemConfigDataInfo>>();
            var sysConfigData = repoSysConfigData.GetById(request.SystemConfigDataId);

            var response = new GetSystemConfigDataInfoByIdResponse()
            {
                SystemConfigData = sysConfigData,
                ResultType = ResultTypes.Ok,
            };

            return response;

        }
    }
}
