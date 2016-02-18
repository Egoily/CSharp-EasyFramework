
using com.etak.core.customer.message.FreezeCustomerInfo;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;

namespace com.etak.core.bizops.fullfilment.FreezeCustomer
{
    /// <summary>
    /// Freeze customer
    /// </summary>
    public class FreezeCustomerBizOp : AbstractSinglePhaseOrderProcessor<FreezeCustomerRequestDTO, FreezeCustomerResponseDTO, FreezeCustomerRequestInternal, FreezeCustomerResponseInternal, FreezeCustomerOrder>
    {
        /// <summary>
        /// Operation Code for FreezeSubscription
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.FreezeCustomer; }
        }

        /// <summary>
        /// Operation Discriminator for FreezeSubscription
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.FreezeCustomer; }
        }

        /// <summary>
        /// Freeze customer, set prepertyInfo pending status to status Regulatory
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override FreezeCustomerResponseInternal ProcessRequest(FreezeCustomerOrder order,
           FreezeCustomerRequestInternal request)
        {
            if (request.Subscription.CustomerInfo == null || request.Subscription.CustomerInfo.CustomerID == null)
                throw new BusinessLogicErrorException(string.Format("Cannot get Customer Information for MSISDN {0}", request.MSISDN), BizOpsErrors.CustomerNotFound);
            //FreezeCustomerInfoMs
            var freezeCustomerInfoMS = MicroServiceManager.GetMicroService<FreezeCustomerInfoRequest, FreezeCustomerInfoResponse>();
            var freezeCustomerRequest = new FreezeCustomerInfoRequest()
            {
                CustomerInfo = request.Subscription.CustomerInfo,
            };
            var freezeCustomerResponse = freezeCustomerInfoMS.Process(freezeCustomerRequest,null);

            return new FreezeCustomerResponseInternal()
            {
                ResultType = freezeCustomerResponse.ResultType,
                Customer = request.Subscription.CustomerInfo
            };
        }

        /// <summary>
        ///  Maps all the inboud properties of the request that are not mapped by the framework
        /// </summary>
        /// <param name="request"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedOrderInboundProperties(FreezeCustomerRequestDTO request,
            ref FreezeCustomerRequestInternal coreInput)
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
        protected override void MapNotAutomappedOrderOutboundProperties(FreezeCustomerResponseInternal source,
            ref FreezeCustomerResponseDTO DTOOutput)
        {
            DTOOutput.Customer = source.Customer.ToDto();
        }
    }
}
