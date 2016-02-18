using System;
using System.Linq;
using System.Reflection;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.dealer.messages.GetDealerInfoById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.resource.msisdn.message.ReserveMsisdnMS;
using log4net;

namespace com.etak.core.bizops.fullfilment.ReserveMsisdn
{
    /// <summary>
    /// Put the number in Reserved State
    /// </summary>
    public class ReserveMsisdnBizOp : AbstractSinglePhaseOrderProcessor<ReserveMsisdnRequestDTO, ReserveMsisdnResponseDTO, ReserveMsisdnRequestInternal, ReserveMsisdnResponseInternal, ReserveMsisdnOrder>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private const int ReservedStatus = (int)ResourceStatus.Reserved;
        private const int InitStatus = (int)ResourceStatus.Init;

        /// <summary>
        /// Get the DealerNumber and the DealerInfo using the request's resource
        /// </summary>
        /// <param name="request">Contains the msisdn to be reserved</param>
        /// <param name="coreInput">Core Request with the objects</param>
        protected override void MapNotAutomappedOrderInboundProperties(ReserveMsisdnRequestDTO request, ref ReserveMsisdnRequestInternal coreInput)
        {
            if (coreInput.NumberInPool == null)
                throw new BusinessLogicErrorException(string.Format("Msisdn {0} is not exist on pool", request.MSISDN), BizOpsErrors.MSISDNNotFound);

            var dealerNumber = coreInput.NumberInPool.NumberDealerSharing.FirstOrDefault();
                
            Log.Info("Checking that the DealerNumber found is valid.");
            if (dealerNumber == null || dealerNumber.DealerID == null)
                throw new BusinessLogicErrorException(string.Format("Cannot get DealerNumberInfo with msisdn {0}.", request.MSISDN), BizOpsErrors.DealerNumberNotFound);

            #region Get DealerInfo
            Log.InfoFormat("Calling GetDealerInfoByIdMS to get the DealerInfo with DealerId ({0}).", dealerNumber.DealerID.Value);
            IMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse> getDealerInfoMs = MicroServiceManager.GetMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var getDealerInfoReq = new GetDealerInfoByIdRequest()
            {
                DealerId = dealerNumber.DealerID.Value,
            };
            var getDealerInfoResp = getDealerInfoMs.Process(getDealerInfoReq, null);

            var dealerInfo = getDealerInfoResp.DealerInfo;
            Log.Info("Checking that the DealerInfo is valid.");
            if (dealerInfo == null)
                throw new BusinessLogicErrorException(string.Format("Cannot find any Dealer with DealerId {0}", dealerNumber.DealerID.Value), BizOpsErrors.DealerInfoNotFound);
            #endregion

            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = dealerInfo.DealerID != null ? dealerInfo.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion

            #region Fill CoreInput
            Log.Info("Filling the coreInput...");

            coreInput.DealerInfo = dealerInfo;

            #endregion
        }


        /// <summary>
        /// This bizop does not have not automapped fields
        /// </summary>
        /// <param name="source">the response of the core operation that needs to be mapped</param>
        /// <param name="coreOutput">the response of the operation in DTO format</param>
        protected override void MapNotAutomappedOrderOutboundProperties(ReserveMsisdnResponseInternal source, ref ReserveMsisdnResponseDTO coreOutput)
        {
        }

        /// <summary>
        /// Processs reserve Msisdn
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns>reserveMsisdnResponse</returns>
        public override ReserveMsisdnResponseInternal ProcessRequest(ReserveMsisdnOrder order, ReserveMsisdnRequestInternal request)
        {
            ReserveMsisdnResponseInternal reserveMsisdnResponse = new ReserveMsisdnResponseInternal()
            {
                ResultType = ResultTypes.Ok,
            };

            Log.Info("Validate that MSISDN belongs to Dealer");
            if (request.DealerInfo.FiscalUnitID != request.MVNO.FiscalUnitID)
                throw new DataValidationErrorException(string.Format("Dealer's FiscalUnitID ({0}) do not match with msisdn owner ({1}).",
                                                                     request.MVNO.FiscalUnitID, request.DealerInfo.FiscalUnitID), BizOpsErrors.OwnerNotMatch);

            ////The msisdn have to be in Init Status and not null
            if (request.NumberInPool.NumberProperty.StatusID == null || request.NumberInPool.NumberProperty.StatusID != InitStatus)
                throw new DataValidationErrorException(
                    string.Format("Number Property ({0}) is not in Init state ({1}), but was on {2} state.",
                        request.NumberInPool.NumberProperty.Resource, InitStatus, request.NumberInPool.NumberProperty.StatusID), BizOpsErrors.ResourceNotInProperStatus);

            //Setting new values
            //request.NumberInPool.NumberProperty.StatusID = ReservedStatus;
            //request.NumberInPool.NumberProperty.UpdateUserID = request.User.UserID;
            //request.NumberInPool.NumberProperty.UpdateDate = DateTime.Now;

            //Update msisdn status
            var reserveMsisdnMs = MicroServiceManager.GetMicroService<ReserveMsisdnRequest, ReserveMsisdnResponse>();
            ReserveMsisdnRequest reserveMsisdnRequest = new ReserveMsisdnRequest()
            {
                NumberPropertyInfo = request.NumberInPool.NumberProperty,
                User = request.User,
                UpdateTime = DateTime.Now,
                MVNO = request.DealerInfo,
                Channel = request.Channel,
            };
            var reserveMsisdnMsResp = reserveMsisdnMs.Process(reserveMsisdnRequest, null);

            if (reserveMsisdnMsResp.ResultType != ResultTypes.Ok)
            {
                reserveMsisdnResponse.ResultType = reserveMsisdnMsResp.ResultType;
                reserveMsisdnResponse.ErrorCode = reserveMsisdnMsResp.ErrorCode;
            }

            //TODO We need to review how will be handled the LifeCycle Log
            // DB log
            //com.etak.core.resource.utils.LifeCycleLogs.GenerateLCLogsForMsisdn(prevStatusId, ReservedStatus, request.NumberPropertyInfo.Resource, "Reserve msisdn"
            //        , request.user, request.vMNO);

            return reserveMsisdnResponse;
        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.ReserveMsisdnOperation; }
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.ReserveMsisdnOperation; }
        }
    }
}
