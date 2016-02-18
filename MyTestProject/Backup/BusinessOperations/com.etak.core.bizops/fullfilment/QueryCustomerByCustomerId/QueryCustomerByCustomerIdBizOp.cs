using System.Linq;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByCustomerId
{
    /// <summary>
    /// Simple operation to get customer information from the Id
    /// </summary>
    public class QueryCustomerByCustomerIdBizOp : AbstractBusinessOperation<QueryCustomerByCustomerIdRequestDTO, QueryCustomerByCustomerIdResponseDTO, QueryCustomerByCustomerIdRequestInternal, QueryCustomerByCustomerIdResponseInternal>
    {
        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="dtoRequest">the dto request</param>
        /// <param name="coreInput">the core request</param>
        protected override void MapNotAutomappedInboundProperties(QueryCustomerByCustomerIdRequestDTO dtoRequest, ref QueryCustomerByCustomerIdRequestInternal coreInput)
        {
            if (coreInput.Customer == null)
                throw new BusinessLogicErrorException("Customer not found", BizOpsErrors.CustomerNotFound);
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
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="coreOutput">the core response</param>
        /// <param name="dtoOutput">the dto response</param>
        protected override void MapNotAutomappedOutboundProperties(QueryCustomerByCustomerIdResponseInternal coreOutput, ref QueryCustomerByCustomerIdResponseDTO dtoOutput)
        {
        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode { get { return OperationCodes.QueryCustomerByCustomerIdOperation; } }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator { get { return OperationDiscriminators.QueryCustomerByCustomerIdOperation; } }

        /// <summary>
        /// Extremely simple implementation as is managed by the framework all entities used by this op
        /// </summary>
        /// <param name="request">the request in core form</param>
        /// <param name="runningOperation">The trace of the operation</param>
        /// <param name="invoker">the environemnt of the invokation</param>
        /// <returns>The customer received in the request</returns>
        protected override QueryCustomerByCustomerIdResponseInternal ProcessBusinessLogic(QueryCustomerByCustomerIdRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new QueryCustomerByCustomerIdResponseInternal()
            {
                Customer = request.Customer,
                Subscription = request.Customer.ResourceMBInfo.FirstOrDefault(x=>x.StatusID == (int)ResourceStatus.Active),
                ErrorCode = 0,
                Message = "Query successful",
                ResultType = ResultTypes.Ok
            };
        }

    }
}
