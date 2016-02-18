using System.Linq;
using System.Reflection;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.service.messages.UpdateServicesInfoWithCustomCreditLimit;
using log4net;
using com.etak.core.operation.manager;

namespace com.etak.core.bizops.revenue.ModifyCustomerCreditLimit
{
    /// <summary>
    /// BizOp for modifying customer credit limit
    /// </summary>
    public class ModifyCustomerCreditLimitBizOp : AbstractSinglePhaseOrderProcessor<ModifyCustomerCreditLimitRequestDTO, ModifyCustomerCreditLimitResponseDTO, ModifyCustomerCreditLimitRequestInternal, ModifyCustomerCreditLimitResponseInternal, ModifyCustomerCreditLimitOrder>
    {
        /// <summary>
        /// Logger of every action that has been made
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Check if the the customer exist and Fill the core input with the request dto 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedOrderInboundProperties(ModifyCustomerCreditLimitRequestDTO request, ref ModifyCustomerCreditLimitRequestInternal coreInput)
        {
            //CustomerInfo has already been retrieved by the framework, check if the customer exist
            if (coreInput.Customer== null)
                throw new DataValidationErrorException(string.Format("It doesn't exist information for the CustomerID:{0}", request.CustomerId),BizOpsErrors.CustomerNotFound);

            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Customer.DealerID != null ? coreInput.Customer.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion

            #region Fill the Core Input
            Log.Info("Fill the Core Input");
            coreInput.NewCreditLimit = request.NewCreditLimit;
            #endregion
        }

        /// <summary>
        /// There is nothing to be mapped from core output to dto output
        /// </summary>
        /// <param name="source">the core output</param>
        /// <param name="coreOutput">the dto output</param>
        protected override void MapNotAutomappedOrderOutboundProperties(ModifyCustomerCreditLimitResponseInternal source, ref ModifyCustomerCreditLimitResponseDTO coreOutput)
        {
        }

        /// <summary>
        /// Modify Customer Credit Limit by updating the CustomerInfo.ServicesInfo.CreditLimit
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override ModifyCustomerCreditLimitResponseInternal ProcessRequest(ModifyCustomerCreditLimitOrder order, ModifyCustomerCreditLimitRequestInternal request)
        {
            //Check if the new credit limit input is less than zero
            if (request.NewCreditLimit < 0)
                throw new DataValidationErrorException("New credit must be positive value", BizOpsErrors.CreditLimtiInRequestLessThanZero);

            Log.InfoFormat("Get Services Info where CreditLimitBaseBundleId is the same with BundleDefinition.BundleId");
            var servicesInfoWithBaseCreditLimit = request.Customer.ServicesInfo.Where(x => x.CREDITLIMITBASEBUNDLEID == x.BundleDefinition.BundleID);

            if (servicesInfoWithBaseCreditLimit.IsNotEmpty())
            {
                //Get the ServicesInfo of the customer to be deleted
                var firstServicesInfoWithBaseCreditLimit = servicesInfoWithBaseCreditLimit.FirstOrDefault();

                if (firstServicesInfoWithBaseCreditLimit == null)
                    throw new DataValidationErrorException("ServicesInfo is null", BizOpsErrors.ServicesInfoIsNull);

                firstServicesInfoWithBaseCreditLimit.CreditLimit = request.NewCreditLimit;
                var updateServicesInfoWithCustomCreditLimitReq = new UpdateServicesInfoWithCustomCreditLimitRequest()
                {
                    ServicesInfo = firstServicesInfoWithBaseCreditLimit,
                    NewCreditLimit = request.NewCreditLimit
                };
                //Update ServicesInfo with CustomCreditLimit
                var updateServicesInfoWithCustomCreditLimitMS =
                    MicroServiceManager
                        .GetMicroService
                        <UpdateServicesInfoWithCustomCreditLimitRequest, UpdateServicesInfoWithCustomCreditLimitResponse
                            >();
                var updateServicesInfoWithCustomCreditLimitRes =
                    updateServicesInfoWithCustomCreditLimitMS.Process(updateServicesInfoWithCustomCreditLimitReq, null);
                return new ModifyCustomerCreditLimitResponseInternal() { ResultType = updateServicesInfoWithCustomCreditLimitRes.ResultType, Subscription = request.Customer.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active) };
            }
            throw new BusinessLogicErrorException("Service with Base Credit Limit is not existent", BizOpsErrors.CustomerDoesNotHaveABaseCreditLimit);
        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.ModifyCustomerCreditLimitOperation; }
        }
        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.ModifyCustomerCreditLimitOperation; }
        }
    }
}
