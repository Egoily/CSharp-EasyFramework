using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using com.etak.core.microservices.messages.CreateSessionInfo;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using log4net;

namespace com.etak.core.bizops.fullfilment.CreateSession
{

    /// <summary>
    /// Busines operation that creates a session
    /// </summary>
    public class CreateSessionBizOp :
        AbstractSinglePhaseOrderProcessor<CreateSessionRequestDTO, CreateSessionResponseDTO,
            CreateSessionRequestInternal, CreateSessionResponseInternal,
            CreateSessionOrder>
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Create sessionInfo  and return this sessionInfo with the sessionId
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override CreateSessionResponseInternal ProcessRequest(CreateSessionOrder order, CreateSessionRequestInternal request)
        {
            var createSessionInfoMS = MicroServiceManager.GetMicroService<CreateSessionInfoRequest, CreateSessionInfoResponse>();
            var sessionInfo = new SessionInfo();
            sessionInfo.LoginInfo = request.User;

            var createSessionInfoRequest = new CreateSessionInfoRequest()
            {
                SessionInfo = sessionInfo
            };

            var createSessiojnInfoResponse = createSessionInfoMS.Process(createSessionInfoRequest, null);

            return new CreateSessionResponseInternal()
            {
                ResultType = ResultTypes.Ok,
                SessionInfo = createSessiojnInfoResponse.SessionInfo
            };

        }

        /// <summary>
        /// Maps all the inboud properties of the request that are not mapped by the framework
        /// </summary>
        /// <param name="request">the request in ET DTO Form</param><param name="coreInput">the resquest partially mapped by the core and needs to be updated</param>
        protected override void MapNotAutomappedOrderInboundProperties(CreateSessionRequestDTO request, ref CreateSessionRequestInternal coreInput)
        {
    
        }

        /// <summary>
        /// Maps all the outboud properties of the response that are not mapped by the framework
        /// </summary>
        /// <param name="source">the response of the core operation that needs to be mapped</param><param name="DTOOutput">the response of the operation in DTO format</param>
        protected override void MapNotAutomappedOrderOutboundProperties(CreateSessionResponseInternal source,
            ref CreateSessionResponseDTO DTOOutput)
        {
            DTOOutput.SessionId = source.SessionInfo != null
                ? source.SessionInfo.SessionID
                : null;
        }

        /// <summary>
        /// CreateSessionBizOp Operation Discriminator
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.CreateSession; }
        }

        /// <summary>
        /// CreateSessionBizOp Operation code 
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.CreateSession; }
        }
    }
}
