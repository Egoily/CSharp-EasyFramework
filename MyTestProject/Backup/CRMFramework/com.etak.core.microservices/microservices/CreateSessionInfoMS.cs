using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.microservices.messages.CreateSessionInfo;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// Microservice : adding  new record of SessionInfo 
    /// </summary>
    public class CreateSessionInfoMS : IMicroService<CreateSessionInfoRequest, CreateSessionInfoResponse>
    {
        /// <summary>
        /// adding  new record of SessionInfo (request.SessionInfo)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="invoker"></param>
        /// <returns>CreateSessionInfoResponse with the SessionInfo created (response.SessionInfo)</returns>
        public CreateSessionInfoResponse Process(CreateSessionInfoRequest request, RequestInvokationEnvironment invoker)
        {
            var repoSessionInfo = RepositoryManager.GetRepository<ISessionInfoRepository<SessionInfo>>();
            var sessionInfo = request.SessionInfo;

            //Set 10080 minutes (1 week) to idle timeout default
            sessionInfo.IdleTimoutMinutes = sessionInfo.IdleTimoutMinutes == 0
                ? 10080
                : sessionInfo.IdleTimoutMinutes;

            sessionInfo.IdleTimeoutDate = sessionInfo.IdleTimeoutDate == DateTime.MinValue
                ? DateTime.UtcNow
                : sessionInfo.IdleTimeoutDate;

            sessionInfo = repoSessionInfo.Create(request.SessionInfo);

            var response = new CreateSessionInfoResponse()
            {
                SessionInfo = sessionInfo,
                ResultType = ResultTypes.Ok,
            };

            return response;
        }
    }
}
