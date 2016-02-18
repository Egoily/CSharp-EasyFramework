using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.service.messages.CustomerHasCredit;
using com.etak.core.service.microservices;
using log4net;

namespace com.etak.core.bizops.revenue.QueryBalance
{
    /// <summary>
    /// QueryBalance Business Operation to get the credit limit for the customer with a given msisdn
    /// </summary>
    public class QueryBalanceBizOp : AbstractBusinessOperation<QueryBalanceRequestDto, QueryBalanceResponseDto, QueryBalanceRequestInternal, QueryBalanceResponseInternal>
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Unique Id of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryBalanceOperation; }
        }

        /// <summary>
        /// Logical code, this is a cnst field but Nhibernate requires the setter.
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryBalanceOperation; }
        }

        /// <summary>
        /// This method operates in ET model. Converts the DTORequest to the Request types
        /// </summary>
        /// <param name="dtoRequest">The request in ET DTO form</param><param name="coreInput">The request in Internal form, prefilled with the common parameters</param>
        /// <returns>
        /// the operation in the et Internal form
        /// </returns>
        protected override void MapNotAutomappedInboundProperties(QueryBalanceRequestDto dtoRequest, ref QueryBalanceRequestInternal coreInput)
        {
            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Subscription.CustomerInfo.DealerID != null ? coreInput.Subscription.CustomerInfo.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion
        }

        /// <summary>
        /// Coverts the response/result of the process (in internal form) to the ET DTO form.
        /// </summary>
        /// <param name="coreOutput">the result of process implementation</param><param name="dtoOutput">the preallocated response, will all auto fields mapped</param>
        /// <returns>
        /// the response in the ET DTO form
        /// </returns>
        protected override void MapNotAutomappedOutboundProperties(QueryBalanceResponseInternal coreOutput, ref QueryBalanceResponseDto dtoOutput)
        {
            if (coreOutput.MasterBundle != null)
            {
                logger.InfoFormat("Get Credit Limit for Service {0}", coreOutput.MasterBundle.ServiceID);
                dtoOutput.Balance = coreOutput.MasterBundle.CreditLimit.Value;
            }
            else
            {
                logger.Info("Cannot get Credit Limit.");
            }
            
        }

        /// <summary>
        /// Method implemented by the inheriting class that actually performs the core operation
        /// </summary>
        /// <param name="request">Input parameter for the request</param><param name="runningOperation">The trace for the ongoing operation</param><param name="invoker">The information about the invokation of the operation</param>
        /// <returns>
        /// The response of processing the request
        /// </returns>
        protected override QueryBalanceResponseInternal ProcessBusinessLogic(QueryBalanceRequestInternal request, model.operation.BusinessOperationExecution runningOperation, operation.RequestInvokationEnvironment invoker)
        {
            var customerInfo = request.Subscription.CustomerInfo;

            logger.Info("Prepare request to call CustomerHasCreditMS in order to get the master bundle.");
            var customerCreditMs = MicroServiceManager.GetMicroService<CustomerHasCreditRequest, CustomerHasCreditResponse>();
            var customerCreditReq = new CustomerHasCreditRequest()
            {
                Amount = 0,
                CustomerInfo = customerInfo,
                DateOfCharge = DateTime.Now,
                MVNO = request.MVNO,
                User = request.User,
            };
            logger.InfoFormat("Call microservice CustomerHasCredit for Customer {0}", customerInfo.CustomerID.Value);
            var customerCreditResp = customerCreditMs.Process(customerCreditReq, null);
            if (customerCreditResp.ResultType != ResultTypes.Ok || customerCreditResp.MasterBundle == null)
                throw new BusinessLogicErrorException(string.Format("Cannot get a Master BundleInfo for Customer {0}", customerInfo.CustomerID.Value), BizOpsErrors.MasterBundleNotFound);

            logger.Info("ResultType is equal to ResultTypes.Ok and the MasterBundle exists. Returning response.");
            return new QueryBalanceResponseInternal()
            {
                ResultType = ResultTypes.Ok,
                MasterBundle = customerCreditResp.MasterBundle
            };
        }
        
    }
}
