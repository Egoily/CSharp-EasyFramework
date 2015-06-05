using com.etak.core.microservices.messages.GetOperationLogsByReferenceCodeAndDealerId;
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
    public class GetOperationLogsByReferenceCodeAndDealerIdMS : IMicroService<GetOperationLogsByReferenceCodeAndDealerIdRequest, GetOperationLogsByReferenceCodeAndDealerIdResponse>
    {
        /// <summary>
        /// Main Process for the microservice
        /// </summary>
        /// <param name="request">Contains the Reference Code and the DealerId</param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public GetOperationLogsByReferenceCodeAndDealerIdResponse Process(GetOperationLogsByReferenceCodeAndDealerIdRequest request, operation.RequestInvokationEnvironment invoker)
        {
            var repoOpLog = RepositoryManager.GetRepository<IOperationLogRepository<OperationLog>>();
            var opLogs = repoOpLog.GetByOrderCodeAndDealerId(request.ReferenceCode, request.DealerId);
            
            var response = new GetOperationLogsByReferenceCodeAndDealerIdResponse()
            {
                OperationLogs = opLogs,
                ResultType = ResultTypes.Ok,
            };

            return response;
        }
    }
}
