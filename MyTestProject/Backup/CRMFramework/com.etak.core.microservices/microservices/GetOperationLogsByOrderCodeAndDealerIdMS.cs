using com.etak.core.microservices.messages.GetOperationLogsByOrderCodeAndDealerId;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// GetOperationLogByExternalCodeAndDealerId Microservice
    /// </summary>
    public class GetOperationLogsByOrderCodeAndDealerIdMS : IMicroService<GetOperationLogsByOrderCodeAndDealerIdRequest, GetOperationLogsByOrderCodeAndDealerIdResponse>
    {
        /// <summary>
        /// Main Process for the microservice
        /// </summary>
        /// <param name="request">Contains the Reference Code and the DealerId</param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public GetOperationLogsByOrderCodeAndDealerIdResponse Process(GetOperationLogsByOrderCodeAndDealerIdRequest request, operation.RequestInvokationEnvironment invoker)
        {
            var repoOpLog = RepositoryManager.GetRepository<IOperationLogRepository<OperationLog>>();
            var opLogs = repoOpLog.GetByOrderCodeColumnAndDealerId(request.OrderCode, request.DealerId);

            var response = new GetOperationLogsByOrderCodeAndDealerIdResponse()
            {
                OperationLogs = opLogs,
                ResultType = ResultTypes.Ok,
            };

            return response;
        }
    }
}
