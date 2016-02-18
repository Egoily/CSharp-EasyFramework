using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.customer.message.UnFreezeCustomerInfo;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;

namespace com.etak.core.bizops.fullfilment.UnFreezeCustomer
{
    /// <summary>
    /// UnFreeze customer
    /// </summary>
    public class UnFreezeCustomerBizOp : AbstractSinglePhaseOrderProcessor<UnFreezeCustomerRequestDTO, UnFreezeCustomerResponseDTO, UnFreezeCustomerRequestInternal, UnFreezeCustomerResponseInternal, UnFreezeCustomerOrder>
    {
        /// <summary>
        /// Operation Code for UnFreezeCustomer
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.UnFreezeCustomer; }
        }

        /// <summary>
        /// Operation Discriminator for UnFreezeCustomer
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.UnFreezeCustomer; }
        }

        /// <summary>
        /// Freeze customer, set prepertyInfo pending status to status Active
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override UnFreezeCustomerResponseInternal ProcessRequest(UnFreezeCustomerOrder order,
           UnFreezeCustomerRequestInternal request)
        {
            if (request.Subscription.CustomerInfo == null || request.Subscription.CustomerInfo.CustomerID == null)
                throw new BusinessLogicErrorException(string.Format("Cannot get Customer Information for MSISDN {0}", request.MSISDN), BizOpsErrors.CustomerNotFound);
            //UnFreezeCustomerInfoMs
            var unFreezeCustomerInfoMS = MicroServiceManager.GetMicroService<UnFreezeCustomerInfoRequest, UnFreezeCustomerInfoResponse>();
            var unFreezeCustomerRequest = new UnFreezeCustomerInfoRequest()
            {
                CustomerInfo = request.Subscription.CustomerInfo,
            };
            var unFreezeCustomerResponse = unFreezeCustomerInfoMS.Process(unFreezeCustomerRequest, null);

            return new UnFreezeCustomerResponseInternal()
            {
                ResultType = unFreezeCustomerResponse.ResultType,
                Customer = request.Subscription.CustomerInfo
            };
        }

        /// <summary>
        ///  Maps all the inboud properties of the request that are not mapped by the framework
        /// </summary>
        /// <param name="request"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedOrderInboundProperties(UnFreezeCustomerRequestDTO request,
            ref UnFreezeCustomerRequestInternal coreInput)
        {
            if (coreInput.MSISDN == null)
            {
                throw new BusinessLogicErrorException("No MSISDN defined in request", BizOpsErrors.MSISDNNotFound);
            }

            if (coreInput.Subscription == null)
            {
                throw new BusinessLogicErrorException(string.Format("Cannot find an active Resource with MSISDN {0}.", request.MSISDN), BizOpsErrors.ResourceMBNotFound);
            }

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
        /// Maps all the outboud properties of the response that are not mapped by the framework
        /// </summary>
        /// <param name="source"></param>
        /// <param name="DTOOutput"></param>
        protected override void MapNotAutomappedOrderOutboundProperties(UnFreezeCustomerResponseInternal source,
            ref UnFreezeCustomerResponseDTO DTOOutput)
        {
            DTOOutput.Customer = source.Customer.ToDto();
        }
    }
}