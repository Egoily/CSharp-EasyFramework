using System.Linq;
using com.etak.core.bizops.fullfilment.QueryTroubleTicketsByCustomerId;
using com.etak.core.customer.message.GetTroubleTicketInfoByCustomerId;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;

namespace com.etak.core.bizops.opssupport.QueryTroubleTicketsByCustomerId
{
    /// <summary>
    /// Operation to query TroubleTicketInfo by customerId
    /// </summary>
    public class QueryTroubleTicketsByCustomerIdBizOp : AbstractBusinessOperation<QueryTroubleTicketsByCustomerIdRequestDTO, QueryTroubleTicketsByCustomerIdResponseDTO, QueryTroubleTicketsByCustomerIdRequestInternal, QueryTroubleTicketsByCustomerIdResponseInternal>
    {
        /// <summary>
        /// Map property which is not automapped by framework
        /// </summary>
        /// <param name="dtoRequest">QueryTroubleTicketsByCustomerIdRequestDTO</param>
        /// <param name="coreInput">QueryTroubleTicketsByCustomerIdRequestInternal</param>
        protected override void MapNotAutomappedInboundProperties(QueryTroubleTicketsByCustomerIdRequestDTO dtoRequest, ref QueryTroubleTicketsByCustomerIdRequestInternal coreInput)
        {
            if (coreInput.Customer == null)
            {
                throw new BusinessLogicErrorException("Customer not found",BizOpsErrors.CustomerNotFound);
            }

            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Customer.DealerID != null ? coreInput.Customer.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coreOutput"></param>
        /// <param name="dtoOutput"></param>
        protected override void MapNotAutomappedOutboundProperties(QueryTroubleTicketsByCustomerIdResponseInternal coreOutput, ref QueryTroubleTicketsByCustomerIdResponseDTO dtoOutput)
        {
        }

        /// <summary>
        /// Process by calling MS to get TroubleTicketInfo by CustomerId
        /// </summary>
        /// <param name="request">QueryTroubleTicketsByCustomerIdRequestInternal</param>
        /// <param name="runningOperation">BusinessOperationExecution</param>
        /// <param name="invoker">RequestInvokationEnvironment</param>
        /// <returns>QueryTroubleTicketsByCustomerIdResponseInternal</returns>
        protected override QueryTroubleTicketsByCustomerIdResponseInternal ProcessBusinessLogic(QueryTroubleTicketsByCustomerIdRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            var getTroubleTicketInfoByCustomerIdRequest = MicroServiceManager
                .GetMicroService<GetTroubleTicketInfoByCustomerIdRequest, GetTroubleTicketInfoByCustomerIdResponse>();
            var getTroubleTicketInfoByCustomerIdResponse =
                getTroubleTicketInfoByCustomerIdRequest.Process(new GetTroubleTicketInfoByCustomerIdRequest()
                {
                    CustomerId = request.Customer.CustomerID.Value
                }, null);

            getTroubleTicketInfoByCustomerIdResponse.TroubleTicketInfos.Where(x => x.STATUS != (int)eAtosTTStates.CERRADO || x.STATUS != (int)eAtosTTStates.FINALIZADA);

            return new QueryTroubleTicketsByCustomerIdResponseInternal()
            {
                TroubleTicketInfos = getTroubleTicketInfoByCustomerIdResponse.TroubleTicketInfos,
                ResultType = ResultTypes.Ok,
                ErrorCode = 0,
                Message = "Query Successful"
            };
        }

        /// <summary>
        /// Operation Discriminator for QueryTroubleTicketsByCustomerId
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryTroubleTicketsByCustomerId; }
        }

        /// <summary>
        /// Operation Code for QueryTroubleTicketsByCustomerId
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryTroubleTicketsByCustomerId; }
        }
    }
}
