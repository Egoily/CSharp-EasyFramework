using com.etak.core.microservices.messages.CreateSmsLogInfo;
using com.etak.core.microservices.messages.GetSystemConfigDataInfoById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm;
using com.etak.core.repository.crm.configuration;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// CreateSmsLogInfoMS Microservice
    /// </summary>
    public class CreateSmsLogInfoMS : IMicroService<CreateSmsLogInfoRequest, CreateSmsLogInfoResponse>
    {
        /// <summary>
        /// adding  new record of SmsLogInfo (request.SmsLogInfo)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="invoker"></param>
        /// <returns>CreateSmsLogInfoResponse with the SmsLogInfo created (response.SmsLogInfo)</returns>
        public CreateSmsLogInfoResponse Process(CreateSmsLogInfoRequest request, RequestInvokationEnvironment invoker)
        {
            var repoSmsLogInfo = RepositoryManager.GetRepository<ISmsLogInfoRepository<SmsLogInfo>>();
            var SmsLogInfo = request.SmsLogInfo;

            SmsLogInfo = repoSmsLogInfo.Create(request.SmsLogInfo);

            var response = new CreateSmsLogInfoResponse()
            {
                SmsLogInfo = SmsLogInfo,
                ResultType = ResultTypes.Ok,
            };

            return response;
        }
    }
}
