using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.implementation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.manager;

namespace com.etak.core.bizops.fullfilment.QuerySimCard
{
    /// <summary>
    /// Simple operation to get SIM card information from ICCID
    /// </summary>
    public class QuerySimCardBizOp
        : AbstractBusinessOperation<QuerySimCardRequestDTO, QuerySimCardResponseDTO,
                                    QuerySimCardRequestInternal, QuerySimCardResponseInternal>
    {
        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="dtoRequest">The dto request</param>
        /// <param name="coreInput">The core input</param>
        protected override void MapNotAutomappedInboundProperties(QuerySimCardRequestDTO dtoRequest, ref QuerySimCardRequestInternal coreInput)
        {
            if (coreInput.SimCard == null)
                throw new BusinessLogicErrorException("No resource found with given ICCID", BizOpsErrors.SimCardNotFound);

            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.SimCard.Dealer.DealerID != null ? coreInput.SimCard.Dealer.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion
        }

        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="coreOutput">The core output</param>
        /// <param name="dtoOutput">The dto output</param>
        protected override void MapNotAutomappedOutboundProperties(QuerySimCardResponseInternal coreOutput, ref QuerySimCardResponseDTO dtoOutput)
        {
        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QuerySimCard; }
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QuerySimCard; }
        }

        /// <summary>
        /// Extremely simple implementation as is managed by the framework all entities used by this op
        /// </summary>
        /// <param name="request">The core input</param>
        /// <param name="runningOperation">The trace of the operation</param>
        /// <param name="invoker">The environment of the invokation</param>
        /// <returns>QuerySimCardResponseInternal</returns>
        protected override QuerySimCardResponseInternal ProcessBusinessLogic(QuerySimCardRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {

            return new QuerySimCardResponseInternal()
            {
                SimCard = request.SimCard,
                ErrorCode = 0,
                Message = "Query successful",
                ResultType = ResultTypes.Ok
            };
        }
    }
}
