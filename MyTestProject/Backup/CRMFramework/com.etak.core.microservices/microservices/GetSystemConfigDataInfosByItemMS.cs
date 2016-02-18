using com.etak.core.microservices.messages.GetSystemConfigDataInfosByItem;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm.configuration;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// GetSystemConfigDataInfosByItem Microservice
    /// </summary>
    public class GetSystemConfigDataInfosByItemMS : IMicroService<GetSystemConfigDataInfosByItemRequest, GetSystemConfigDataInfosByItemResponse>
    {
        /// <summary>
        /// Process of the microservice to get the SystemConfigDataInfo
        /// </summary>
        /// <param name="request">Contains the SystemConfigDataInfo Item</param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public GetSystemConfigDataInfosByItemResponse Process(GetSystemConfigDataInfosByItemRequest request, operation.RequestInvokationEnvironment invoker)
        {
            var repoSysConfigData = RepositoryManager.GetRepository<ISystemConfigDataInfoRepository<SystemConfigDataInfo>>();
            var sysConfigData = repoSysConfigData.GetSystemConfigDateInfoByItem(request.Item);
            
            var response = new GetSystemConfigDataInfosByItemResponse()
            {
                SystemConfigData = sysConfigData,
                ResultType = ResultTypes.Ok,
            };

            return response;

        }
    }
}
