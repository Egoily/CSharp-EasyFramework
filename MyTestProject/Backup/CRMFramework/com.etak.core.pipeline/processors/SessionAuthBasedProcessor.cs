using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.aaa;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.repository;
using com.etak.core.repository.crm;
using log4net;

namespace com.etak.core.pipeline.processors
{
    
    /// <summary>
    /// Class that takes care of the coordination of validating inputs,check session athority,
    /// Converting from customer model to et model, process the request and ensure DB access
    /// </summary>
    /// <typeparam name="TExternalInput">Type of the request in customer model</typeparam>
    /// <typeparam name="TExternalOutput">Type of the response in customer model</typeparam>
    /// <typeparam name="TInternalInput">Type of the request in ET model</typeparam>
    /// <typeparam name="TInternalOutput">Type of the response in ET model</typeparam>
    public class SessionAuthBasedProcessor<TExternalInput, TExternalOutput, TInternalInput, TInternalOutput>
        : IFrontEndMessageProcessor<TExternalInput, TExternalOutput>
        where TExternalInput : class, new()
        where TExternalOutput : class, new()
        where TInternalInput : RequestBaseDTO, new()
        where TInternalOutput : ResponseBaseDTO, new()
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The method that must be invoked internally to perform the operation.
        /// Uses the Internal model to perform the operation
        /// </summary>
        /// <param name="request">the operation to perform</param>
        /// <param name="methodBase">the method information of the invokation</param>
        /// <returns>the result of processing the request</returns>
        public TExternalOutput ProcessRequest(TExternalInput request, MethodBase methodBase)
        {
            TExternalOutput response;
            try
            {
                //Instead of rename the thread, we are going to use Log4Net Context Properties
                KeyFieldThreadRenamer.RenameLoggerContext(request, methodBase);

                _log.Info("Processing request:" + methodBase.Name);

                //Get network information
                RequestInvokationEnvironment env  = GenerateInvokationEnvironment(methodBase);

                //Connection to repository handling
                using (RepositoryManager.GetNewConnection())
                {
                    //Input Data validation
                    IValidator<TExternalInput> inputValidator = PipeLineManager.GetValidator<TExternalInput>();
                    
                    //Covert the types 
                    ITypeConverter<TExternalInput, TInternalInput> inboundConverter = PipeLineManager.GetTypeConverter<TExternalInput, TInternalInput>();
                    ITypeConverter<TInternalOutput, TExternalOutput> outboundConverter = PipeLineManager.GetTypeConverter<TInternalOutput, TExternalOutput>();


                    TInternalInput etakRequest = inboundConverter.Convert(request);

                    //Core valiadtion Validate
                    if (_log.IsDebugEnabled)
                        _log.Debug("Performing core request validation");
                    ValidateInput(etakRequest);


                    //get LoginInfo based on userId
                    if (_log.IsDebugEnabled)
                        _log.Debug("Getting user information");

                    LoginInfo user = GetLoginInfoBySessionId(etakRequest.sessionId);
                    
                    env.User = user;
                    
                    var userDealerInfo = user.UserDealerInfo.FirstOrDefault();
                    if (userDealerInfo != null)
                        env.Dealer = GetDealerInfoByDealerIdRepo(userDealerInfo.DealerID);
                    


                    //Process the request
                    _log.Debug("Looking up operation processor");
                    IDTOBusinessOperation<TInternalInput, TInternalOutput> processor = PipeLineManager.GetBusinessOperation<TInternalInput, TInternalOutput>();

                    _log.Debug("Processing input message");
                    TExternalOutput eTresponse = processor.ProcessFromCustomerModel(inputValidator, inboundConverter, outboundConverter, request, env);

                    return (eTresponse);
                }
            }
            catch (ElephantTalkBaseException ex)
            {
                _log.Info("Handled error", ex);
                IDefaultResponseGenerator<TExternalOutput> defResponseGenerator = PipeLineManager.GetDefaultMessageGenerator<TExternalOutput>();
                response = defResponseGenerator.GenerateDefaultResponse(ex);
            }
            catch (Exception ex)
            {   
                _log.Error("Unhandled error during operation, sending default response", ex);
                IDefaultResponseGenerator<TExternalOutput> defResponseGenerator = PipeLineManager.GetDefaultMessageGenerator<TExternalOutput>();
                response = defResponseGenerator.GenerateDefaultResponse();
            }
            return (response);
        }

        private RequestInvokationEnvironment GenerateInvokationEnvironment (MethodBase invoker)
        {
            MessageProperties messageProps = OperationContext.Current.IncomingMessageProperties;
            RemoteEndpointMessageProperty remoteEndProp = (RemoteEndpointMessageProperty)messageProps[RemoteEndpointMessageProperty.Name];
            HttpRequestMessageProperty httpProp = (HttpRequestMessageProperty)messageProps[HttpRequestMessageProperty.Name];

            String forwardHeader = httpProp.Headers["X-Forwarded-For"];

            RequestInvokationEnvironment env = new RequestInvokationEnvironment();
            env.Invoker = invoker;
            env.SourceIp = forwardHeader ?? remoteEndProp.Address;
            env.ProxyIp = forwardHeader == null ? remoteEndProp.Address : null;
            env.ServingUrl = httpProp.QueryString;
            _log.DebugFormat("RequestInvokationEnvironment Invoker:{0} SourceIp:{1} ProxyIp:{2} ServingUrl:{3}", env.Invoker, env.SourceIp, env.ProxyIp, env.ServingUrl);
            return (env);
        }

        private void ValidateInput(TInternalInput request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (String.IsNullOrWhiteSpace(request.sessionId))
                throw new DataValidationErrorException("sessionId provided was empty", OperationErrorCodes.SessionIdNotProvided);

        }

        private static LoginInfo GetLoginInfoBySessionId(string sessionId)
        {
            ISessionInfoRepository<SessionInfo> sessionInfoRepo = RepositoryManager.GetRepository<ISessionInfoRepository<SessionInfo>>();
            IEnumerable<SessionInfo> sessionInfos = sessionInfoRepo.GetBySessionId(sessionId);
            
            SessionInfo sessionInfo = sessionInfos.FirstOrDefault();
            
            if (sessionInfo == null)
                throw new DataValidationErrorException("Given sessionId does not exists", OperationErrorCodes.SessionIdNotProvided);

            if (sessionInfo.IdleTimeoutDate.AddMinutes(Convert.ToDouble(sessionInfo.IdleTimoutMinutes)) < DateTime.UtcNow)
                throw new DataValidationErrorException("Sessionid expired", OperationErrorCodes.SessionIdExpired);

            return sessionInfo.LoginInfo;
        }

        private static DealerInfo GetDealerInfoByDealerIdRepo(int dealerId)
        {
            IDealerInfoRepository<DealerInfo> dealerInfoRepo = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();
            IEnumerable<DealerInfo> dealerInfos = dealerInfoRepo.GetByDealerIdAndCache(dealerId);

            DealerInfo dealerInfo = dealerInfos.FirstOrDefault();

            if (dealerInfo == null)
                throw new DataValidationErrorException("Given dealerId does not exists", OperationErrorCodes.DealerIdNotExsist);

            return dealerInfo;
        }
    }    
}
