using System.Linq;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.GSMSubscription.messages.GetLastSubscriptionByMsisdn;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByMsisdn
{
    /// <summary>
    /// Operation to query customer by exact msisdn, and will query last subscription if there is no active subscription
    /// </summary>
    public class QueryCustomerByMsisdnBizOp : AbstractBusinessOperation<QueryCustomerByMsisdnRequestDTO,QueryCustomerByMsisdnResponseDTO,QueryCustomerByMsisdnRequestInternal,QueryCustomerByMsisdnResponseInternal>
    {
        /// <summary>
        /// Here we will fill the Customer from coreInput.Subscription.Customer also validate if the entity has valid business logic
        /// </summary>
        /// <param name="dtoRequest"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedInboundProperties(QueryCustomerByMsisdnRequestDTO dtoRequest, ref QueryCustomerByMsisdnRequestInternal coreInput)
        {
            var subscription = coreInput.Subscription;
            if (subscription == null)
            {
                var getLastSubscriptionByMsisdnMS =
                    MicroServiceManager
                        .GetMicroService<GetLastSubscriptionByMsisdnRequest, GetLastSubscriptionByMsisdnResponse>();
                var getLastSubscriptionByMsisdnResponse = getLastSubscriptionByMsisdnMS.Process(new GetLastSubscriptionByMsisdnRequest()
                {
                    Msisdn = dtoRequest.MSISDN,
                    MVNO = coreInput.MVNO,
                    Status = new int[] { }

                }, null);
                coreInput.Subscription = getLastSubscriptionByMsisdnResponse.ResourceMBInfo.FirstOrDefault();
                if (coreInput.Subscription == null)
                {
                    throw new BusinessLogicErrorException("The MSISDN does not have any subscription", BizOpsErrors.CustomerWithoutSubscriptions);
                }
            }
            var customer = coreInput.Subscription.CustomerInfo;
            if (customer == null)
                throw new BusinessLogicErrorException("Customer not found", BizOpsErrors.CustomerNotFound);
            if (!customer.RevenueProductsInfo.Any())
                throw new BusinessLogicErrorException("Customer does not have any products assigned", BizOpsErrors.CustomerProductsNotFound);
            if (!customer.ServicesInfo.Any())
                throw new BusinessLogicErrorException("Customer does not have any services assigned", BizOpsErrors.ServicesInfoIsNull);
            if (!customer.Promotions.Any())
                throw new BusinessLogicErrorException("Customer does not have any promotions assigned", BizOpsErrors.CustomerPromotionNotFound);
            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Subscription.OperatorInfo.DealerID != null ? coreInput.Subscription.OperatorInfo.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion
        }

        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="coreOutput">the core response</param>
        /// <param name="dtoOutput">the dto response</param>
        protected override void MapNotAutomappedOutboundProperties(QueryCustomerByMsisdnResponseInternal coreOutput, ref QueryCustomerByMsisdnResponseDTO dtoOutput)
        {
        }

        /// <summary>
        /// Extremely simple implementation as is managed by the framework all entities used by this operation
        /// </summary>
        /// <param name="request">the request in core form</param>
        /// <param name="runningOperation">The trace of the operation</param>
        /// <param name="invoker">the environment of the invokation</param>
        /// <returns>The customer received in the request</returns>
        protected override QueryCustomerByMsisdnResponseInternal ProcessBusinessLogic(QueryCustomerByMsisdnRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new QueryCustomerByMsisdnResponseInternal()
            {
                Customer = request.Subscription.CustomerInfo,
                ResultType = ResultTypes.Ok,
                Message = "Query Success",
                ErrorCode = 0
            };
        }
        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryCustomerByMsisdnOperation; }
        }
        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryCustomerByMsisdnOperation; }
        }
    }
}
