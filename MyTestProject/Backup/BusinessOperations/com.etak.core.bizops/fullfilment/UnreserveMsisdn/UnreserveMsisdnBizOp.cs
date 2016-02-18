using System.Linq;
using System.Reflection;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.dealer.messages.GetDealerInfoById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.resource.msisdn.message.GetDealerNumberByResource;
using com.etak.core.resource.msisdn.message.UnReserveMsisdnMS;
using log4net;
using com.etak.core.operation.manager;

namespace com.etak.core.bizops.fullfilment.UnreserveMsisdn
{
    /// <summary>
    /// Operation to create a new order that unreserve MSISDN by setting the status of the MSISDN to Init Status 
    /// </summary>
    public class UnreserveMsisdnBizOp : AbstractSinglePhaseOrderProcessor<UnreserveMsisdnRequestDTO, UnreserveMsisdnResponseDTO, UnreserveMsisdnRequestInternal, UnreserveMsisdnResponseInternal, UnreserveMsisdnOrder>
    {
        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.UnreserveMsisdnOperation; }
        }
        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.UnreserveMsisdnOperation; }
        }
        /// <summary>
        /// Logger of every action that has been made
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// ReservedStatus of MSISDN, it is used to check if the status of the msisdn is in Reserved State
        /// </summary>
        private const int ReservedStatus = (int)ResourceStatus.Reserved;

        /// <summary>
        /// We need to get the DealerInfo from the request by GetDealerNumber then GetDealerInfo, because it is not automatically managed by the framework
        /// </summary>
        /// <param name="request">DTO Request</param>
        /// <param name="coreInput">Request Internal</param>
        protected override void MapNotAutomappedOrderInboundProperties(UnreserveMsisdnRequestDTO request, ref UnreserveMsisdnRequestInternal coreInput)
        {
            if (Log.IsDebugEnabled)
                Log.InfoFormat("Calling GetDealerNumberByResourceMS to get the DealerNumberInfo of the msisdn ({0}) specified.", request.MSISDN);

            #region Get DealerNumber By Resource
            var getDealerNumberByResourceMS = MicroServiceManager.GetMicroService<GetDealerNumberByResourceRequest, GetDealerNumberByResourceResponse>();
            var getDealerNumberByResourceReq = new GetDealerNumberByResourceRequest()
            {
                Msisdn = request.MSISDN
            };
            var getDealerNumberByResourceRes = getDealerNumberByResourceMS.Process(getDealerNumberByResourceReq, null);
            Log.Info("Checking that the DealerNumber found is valid.");
            if (getDealerNumberByResourceRes.DealerNumberInfo.IsEmpty())
            {
                throw new DataValidationErrorException(string.Format("Cannot get DealerNumberInfo with msisdn {0}.", request.MSISDN), BizOpsErrors.DealerNumberNotFound);
            }

            var dealerNumber = getDealerNumberByResourceRes.DealerNumberInfo.FirstOrDefault();
            
            #endregion

            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = dealerNumber != null ? dealerNumber.DealerID.Value : 0};
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion

            #region Get DealerInfo By Id
            var getDealerInfoByIdMS = MicroServiceManager.GetMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var getDealerInfoByIdReq = new GetDealerInfoByIdRequest()
            {
                DealerId = dealerNumber.DealerID.Value
            };
            var getDealerInfoByIdRes = getDealerInfoByIdMS.Process(getDealerInfoByIdReq, null);

            var dealerInfo = getDealerInfoByIdRes.DealerInfo;
            Log.Info("Checking that the DealerInfo is valid.");
            if (dealerInfo == null)
                throw new BusinessLogicErrorException(string.Format("Cannot find any Dealer with DealerId {0}", dealerNumber.DealerID.Value), BizOpsErrors.DealerInfoNotFound);
            #endregion
            #region Fill RequestInternal
            coreInput.DealerInfo = dealerInfo;
            #endregion
        }

        /// <summary>
        /// Validate the MSISDN which status need to be change, and change the status by ReserveMsisdnMS
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override UnreserveMsisdnResponseInternal ProcessRequest(UnreserveMsisdnOrder order, UnreserveMsisdnRequestInternal request)
        {
            #region Validate all the request internal
            //Validate that MSISDN belongs to Dealer
            if (request.DealerInfo.FiscalUnitID != request.MVNO.FiscalUnitID)
            {
                throw new DataValidationErrorException(string.Format("Dealer's FiscalUnitID ({0}) do not match with msisdn owner ({1}).",
                                                                      request.MVNO.FiscalUnitID, request.DealerInfo.FiscalUnitID), BizOpsErrors.OwnerNotMatch);
            }
            //Validate that MSISDN has to be in Reserved Status or not null
            if (request.NumberInPool.NumberProperty.StatusID != ReservedStatus ||
                request.NumberInPool.NumberProperty.StatusID == null)
            {
                throw new DataValidationErrorException(
                    string.Format("Number Property ({0}) is not in Reserved state ({1}), but was on {2} state.",
                        request.NumberInPool.NumberProperty.Resource, ReservedStatus, request.NumberInPool.NumberProperty.StatusID), BizOpsErrors.ResourceNotInProperStatus);
            }
            #endregion

            #region Set the NumberProperty.Status to Unreserved by using MS

            var unreservedMSISDNReq = new UnReserveMsisdnRequest()
            {
                NumberPropertyInfo = request.NumberInPool.NumberProperty,
                User = request.User,
                MVNO = request.MVNO,
                Channel = request.Channel,
            };
            var unreservedMSISDNMS =
                MicroServiceManager.GetMicroService<UnReserveMsisdnRequest, UnReserveMsisdnResponse>();
            var unreservedMSISDNRes = unreservedMSISDNMS.Process(unreservedMSISDNReq, null);
            #endregion

            #region Return The Response
            UnreserveMsisdnResponseInternal unreserveMsisdnResponse = new UnreserveMsisdnResponseInternal()
            {
                ResultType = ResultTypes.Ok,
            };
            if (!unreservedMSISDNRes.ResultType.Equals(ResultTypes.Ok))
            {
                unreserveMsisdnResponse.ResultType = unreservedMSISDNRes.ResultType;
                unreserveMsisdnResponse.ErrorCode = unreservedMSISDNRes.ErrorCode;
            }
            return unreserveMsisdnResponse;
            #endregion
        }

        /// <summary>
        /// Nothing to be mapped from internal response to dto response
        /// </summary>
        /// <param name="source"></param>
        /// <param name="coreOutput"></param>
        protected override void MapNotAutomappedOrderOutboundProperties(UnreserveMsisdnResponseInternal source, ref UnreserveMsisdnResponseDTO coreOutput)
        {

        }
    }
}
