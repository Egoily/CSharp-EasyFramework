using System.Linq;
using System.Reflection;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.GSMSubscription.messages.GetResourceMBInfosByICCID;
using com.etak.core.GSMSubscription.messages.UpdateSubscription;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.resource.simCard.message.ActiveSimCard;
using com.etak.core.resource.simCard.message.ExpirateSimCard;
using com.etak.core.resource.simCard.message.GetSimCardByICCId;
using com.etak.core.resource.simCard.message.HLRSwapResources;
using FrontEndServiceContract.messages.provision;
using log4net;
using Network3GPPModel.model.subscriber;

namespace com.etak.core.bizops.fullfilment.SwapSimCard
{
    /// <summary>
    /// SwapSimCardBizOp to swap old sim card with new sim card
    /// </summary>
    public class SwapSimCardBizOp : AbstractSinglePhaseOrderProcessor<SwapSimCardRequestDTO, SwapSimCardResponseDTO, SwapSimCardRequestInternal, SwapSimCardResponseInternal, SwapSimCardOrder>
    {
        /// <summary>
        /// LogManager to write log info
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Map not automap Inbound properties SwapSimCardRequestDTO to SwapSimCardRequestInternal
        /// </summary>
        /// <param name="requestDTO">SwapSimCardRequestDTO</param>
        /// <param name="coreInput">SwapSimCardRequestInternal</param>
        protected override void MapNotAutomappedOrderInboundProperties(SwapSimCardRequestDTO requestDTO, ref SwapSimCardRequestInternal coreInput)
        {
            //call getResourceMBInfosByICCIDMS microservice
            Log.InfoFormat("Calling GetResourceMBInfosByICCIDMS to get ResourceInfo of ICCID ({0}) specified,", coreInput.SimCard.ICCID);
            IMicroService<GetResourceMBInfosByICCIDRequest, GetResourceMBInfosByICCIDResponse> getResourceMBInfosByICCIDMS =
                MicroServiceManager.GetMicroService<GetResourceMBInfosByICCIDRequest, GetResourceMBInfosByICCIDResponse>
                    ();
            var getResourceMBInfoMSReq = new GetResourceMBInfosByICCIDRequest()
            {
                ICCID = requestDTO.ICCID,
                Msisdn = requestDTO.Msisdn
            };
            var ResourceTemp = getResourceMBInfosByICCIDMS.Process(getResourceMBInfoMSReq, null);
            coreInput.ResourceMBInfos = ResourceTemp.ResourceMbInfos.Where(x => !(x.StatusID == (int)ResourceStatus.Expired ||
                                                                                          x.StatusID == (int)ResourceStatus.Frozen ||
                                                                                          x.StatusID == (int)ResourceStatus.Init ||
                                                                                          x.StatusID == (int)ResourceStatus.Converted ||
                                                                                          x.StatusID == (int)ResourceStatus.Locked ||
                                                                                          x.StatusID == (int)ResourceStatus.Deleted)).FirstOrDefault();

            if (coreInput.ResourceMBInfos != null)
            {
                #region checkAuthorization
                var microServiceCheckAuthorization =
                    MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
                var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.ResourceMBInfos.OperatorInfo.DealerID != null ? coreInput.ResourceMBInfos.OperatorInfo.DealerID.Value : 0 };
                var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
                if (!checkAuthorizationResponse.IsAuthorized)
                    throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
                #endregion
            }
            //call GetSimCardByICCIdMS microservice
            Log.InfoFormat("Calling GetSimCardByICCIdMS to get SimCardInfo of ICCID ({0}) specified,", requestDTO.NewIccId);
            IMicroService<GetSimCardByICCIdRequest, GetSimCardByICCIdResponse> getSimCardByICCIdMS =
                MicroServiceManager.GetMicroService<GetSimCardByICCIdRequest, GetSimCardByICCIdResponse>();
            var getSimCardInfoReq = new GetSimCardByICCIdRequest()
            {
                IccId = requestDTO.NewIccId
            };
            var SimCardTemp = getSimCardByICCIdMS.Process(getSimCardInfoReq, null);
            coreInput.DestinationSim = SimCardTemp.SimCardInfo;
            coreInput.Msisdn = requestDTO.Msisdn;            

        }
        /// <summary>
        /// Map not automap outbound properties SwapSimCardResponseInternal to SwapSimCardResponseDTO
        /// </summary>
        /// <param name="source">SwapSimCardResponseInternal</param>
        /// <param name="coreOutput">SwapSimCardResponseDTO</param>
        protected override void MapNotAutomappedOrderOutboundProperties(SwapSimCardResponseInternal source, ref SwapSimCardResponseDTO coreOutput)
        {
            if (source.SimCardInfo != null)
            {
                coreOutput.SimCard = source.SimCardInfo.ToDto();
            }
            else
            {
                coreOutput.SimCard = null;
            }
        }

        /// <summary>
        /// Business logic of SwapSimCardOperation
        /// </summary>
        /// <param name="order">SwapSimCardOrder</param>
        /// <param name="requestInternal">SwapSimCardRequestInternal</param>
        /// <returns>SwapSimCardResponseInternal as Internal Input</returns>
        public override SwapSimCardResponseInternal ProcessRequest(SwapSimCardOrder order, SwapSimCardRequestInternal requestInternal)
        {
            //Checking data validation
            var SimCardInfoReqrequest = new SwapSimCardRequestDTO();
            if (requestInternal.ResourceMBInfos == null)
                throw new DataValidationErrorException(string.Format("SimCard ({0}) not assigned to a number.", requestInternal.SimCard.ICCID), BizOpsErrors.ResourceNotFound);
            if (requestInternal.ResourceMBInfos.Resource != requestInternal.Msisdn)
                throw new BusinessLogicErrorException(string.Format("SimCard ({0}) not assigned to msisdn ({1}).", requestInternal.SimCard.ICCID, SimCardInfoReqrequest.Msisdn), BizOpsErrors.ResourceDiferent);

            
            var oldsim = requestInternal.SimCard;
            var newsim = requestInternal.DestinationSim;
            if (oldsim == null)
                throw new DataValidationErrorException(string.Format("Cannot get SimCard with IccId ({0}).", SimCardInfoReqrequest.OldIccId), BizOpsErrors.SourceSimNull);
            requestInternal.DestinationSim = newsim;
            if (requestInternal.DestinationSim == null)
                throw new DataValidationErrorException(string.Format("Cannot get SimCard with IccId ({0}).", SimCardInfoReqrequest.NewIccId), BizOpsErrors.DestinationSimNull);
            //Check old SIM status
            SIMCardStatus simStatus = (SIMCardStatus)oldsim.Status;
            if (simStatus == SIMCardStatus.Expired ||
                simStatus == SIMCardStatus.Locked ||
                simStatus == SIMCardStatus.Init ||
                simStatus == SIMCardStatus.Frozen)
            {
                throw new BusinessLogicErrorException(string.Format("Incorrect SIM card status for {0}.", SimCardInfoReqrequest.OldIccId), BizOpsErrors.ErrorBase);
            }
            //Check new SIM Status
            SIMCardStatus simcardStatus = (SIMCardStatus)newsim.Status;
            if (simcardStatus != SIMCardStatus.Init && simcardStatus != SIMCardStatus.NotInAuc)
            {
                throw new BusinessLogicErrorException(string.Format("Incorrect SIM card status for new icc '{0}'", SimCardInfoReqrequest.NewIccId), BizOpsErrors.ErrorBase);
            }

            //Check old resource status
            ResourceStatus status = (ResourceStatus)requestInternal.ResourceMBInfos.StatusID;

            if (status == ResourceStatus.Expired ||
                status == ResourceStatus.Frozen ||
                status == ResourceStatus.Init ||
                status == ResourceStatus.Converted ||
                status == ResourceStatus.Locked ||
                status == ResourceStatus.Deleted)
            {
                throw new BusinessLogicErrorException(string.Format("Incorrect resource status for '{0}'", requestInternal.ResourceMBInfos.Resource), BizOpsErrors.ErrorBase);
            }

            //Update Resource Info
            requestInternal.ResourceMBInfos.ICC = requestInternal.DestinationSim.ICCID;
            requestInternal.ResourceMBInfos.IMSI = requestInternal.DestinationSim.IMSI1;
            string PUK = string.IsNullOrEmpty(requestInternal.DestinationSim.PUK2) ? requestInternal.DestinationSim.PUK1 : requestInternal.DestinationSim.PUK1 + "/" + requestInternal.DestinationSim.PUK2;
            requestInternal.ResourceMBInfos.PUK = PUK;

            //Expire old SIM
            IMicroService<ExpirateSimCardRequest, ExpirateSimCardResponse> ExpirateSimCard =
                MicroServiceManager.GetMicroService<ExpirateSimCardRequest, ExpirateSimCardResponse>();
            var ExpirateSimCardReq = new ExpirateSimCardRequest
            {
                SimCardInfo = requestInternal.SimCard,
                User = requestInternal.User,
                MVNO = requestInternal.MVNO
            };
            ExpirateSimCard.Process(ExpirateSimCardReq, null);

            //Activate new SIM
            IMicroService<ActiveSimCardRequest, ActiveSimCardResponse> ActiveSimCard =
                MicroServiceManager.GetMicroService<ActiveSimCardRequest, ActiveSimCardResponse>();
            var ActiveSimCardReq = new ActiveSimCardRequest()
            {
                SimCardInfo = requestInternal.DestinationSim,
                User = requestInternal.User,
                MVNO = requestInternal.MVNO
            };

            ActiveSimCard.Process(ActiveSimCardReq, null);

            //Update ResourceMB info
            requestInternal.ResourceMBInfos.IMSI = requestInternal.DestinationSim.IMSI1;
            requestInternal.ResourceMBInfos.ICC = requestInternal.DestinationSim.ICCID;
            requestInternal.ResourceMBInfos.PUK = string.IsNullOrEmpty(requestInternal.DestinationSim.PUK2) ? requestInternal.DestinationSim.PUK1 : requestInternal.DestinationSim.PUK1 + "/" + requestInternal.DestinationSim.PUK2;
            IMicroService<UpdateSubscriptionRequest, UpdateSubscriptionResponse> UpdateSubscription =
                MicroServiceManager.GetMicroService<UpdateSubscriptionRequest, UpdateSubscriptionResponse>();
            var UpdateSubscriptionReq = new UpdateSubscriptionRequest()
            {
                SubcriberInfo = requestInternal.ResourceMBInfos,
                SIMCardSubcriber = requestInternal.SimCard
            };
            UpdateSubscription.Process(UpdateSubscriptionReq, null);
            var swapRequest = new SwapResourcesRequest();
            SubscriberIdentification identification =  new SubscriberIdentification();
            identification.KeyType = SubscriberIdentificationKeys.IMSI;
            identification.KeyValue = requestInternal.SimCard.IMSI1;
            swapRequest.Identification = identification;

            //TODO Call HLR

            #region To Do(original code & wait until issue solved) will use in future

            var hLRSwapResourcesMS =
                MicroServiceManager.GetMicroService<HLRSwapResourcesRequest, HLRSwapResourcesResponse>();

            var hLRSwapResourcesMSRequest = new HLRSwapResourcesRequest()
            {
                Msisdn = requestInternal.Msisdn,
                DestinationSim = requestInternal.DestinationSim,
                SourceSim = requestInternal.SimCard
            };
            hLRSwapResourcesMS.Process(hLRSwapResourcesMSRequest, null);

           #endregion
            var response = new SwapSimCardResponseInternal()
            {
                SimCardInfo = requestInternal.DestinationSim,
                Subscription = requestInternal.ResourceMBInfos,
                Customer = requestInternal.ResourceMBInfos.CustomerInfo,
                ResultType = ResultTypes.Ok,
                Message = "SIM Card Swapped and HLR updated"
            };

            return response;

        }

        /// <summary>
        /// Operation code of swap sim card
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.SwapSimCardOperation; }
        }
        /// <summary>
        /// Operation discriminator of swap sim card
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.SwapSimCardOperation; }
        }
    }
}
