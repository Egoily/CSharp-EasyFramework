using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.PurchaseProductForCustomer;
using com.etak.core.bizops.helper;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.customer.message.GetCustomersActivePromotionInfo;
using com.etak.core.microservices.messages.GetSucessfulOperationExecutionForCustomer;
using com.etak.core.microservices.messages.GetTaxAuthority;
using com.etak.core.microservices.microservices;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetCurrentBillRunForBillCycle;
using com.etak.core.product.message.GetProductByProductId;
using com.etak.core.product.message.GetProductChargeOptionByProductChargeOptionId;
using com.etak.core.product.message.GetProductChargeOptionsByProductId;
using com.etak.core.repository;
using com.etak.core.repository.crm.promotion;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.customer.message.SubtractBalance;
using com.etak.core.operation.dtoConverters;
using com.etak.core.promotion.microservices;
using com.etak.core.promotion.messages.UpdateCustomersPromotion;
using com.etak.core.GSMSubscription.messages.CheckProductListDependencyRelationsForCustomer;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerId;
using com.etak.core.customer.message.AddCrmCustomersBalanceTransationHistory;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using System.Net;
using log4net;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text;


namespace com.etak.core.bizops.revenue.CallDREQuerySubscriberPromotion
{
    /// <summary>
    /// CallDREQuerySubscriberPromotionBizOp
    /// </summary>
    public class CallDREQuerySubscriberPromotionBizOp : AbstractSinglePhaseOrderProcessor<CallDREQuerySubscriberPromotionRequestDTO, CallDREQuerySubscriberPromotionResponseDTO, CallDREQuerySubscriberPromotionRequestInternal, CallDREQuerySubscriberPromotionResponseInternal, CallDREQuerySubscriberPromotionOrder>
    {


        /// <summary>
        /// Helper for call to httpWebRequest
        /// </summary>
        private IHttpWebRequestHelper _httpWebRequestHelper = null;


        /// <summary>
        /// log
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// OperationCode
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.CallDREQuerySubscriberPromotionOperation; }
        }

        /// <summary>
        /// OperationDiscriminator
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.CallDREQuerySubscriberPromotionOperation; }
        }

        /// <summary>
        /// Helper for call to httpWebRequest
        /// </summary>
        public IHttpWebRequestHelper HttpWebRequestHelper
        {
            get { return _httpWebRequestHelper; }
            set { _httpWebRequestHelper = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CallDREQuerySubscriberPromotionBizOp()
        {
            HttpWebRequestHelper = new HttpWebRequestHelper();
        }


        /// <summary>
        /// MapNotAutomappedOrderInboundProperties
        /// </summary>
        /// <param name="request"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedOrderInboundProperties(CallDREQuerySubscriberPromotionRequestDTO request, ref CallDREQuerySubscriberPromotionRequestInternal coreInput)
        {
            coreInput.msisdn = request.msisdn;
            coreInput.promotionId = request.promotionId;
            coreInput.millionSecond = request.millionSecond;
            coreInput.serverURL = request.serverURL;
        }

        /// <summary>
        /// MapNotAutomappedOrderOutboundProperties
        /// </summary>
        /// <param name="source"></param>
        /// <param name="coreOutput"></param>
        protected override void MapNotAutomappedOrderOutboundProperties(CallDREQuerySubscriberPromotionResponseInternal source, ref CallDREQuerySubscriberPromotionResponseDTO coreOutput)
        {
            coreOutput.sdp_id = source.sdp_id;
            coreOutput.currentLimit = source.currentLimit;
            coreOutput.frozenLimit = source.frozenLimit;
            coreOutput.promotionId = source.promotionId;
            coreOutput.errorCode = source.errorCode;
        }


        /// <summary>
        /// ProcessRequest
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override CallDREQuerySubscriberPromotionResponseInternal ProcessRequest(CallDREQuerySubscriberPromotionOrder order, CallDREQuerySubscriberPromotionRequestInternal request)
        {
            Log.Info("[Begin Invoke QuerySubscriberPromotion in DRE]");
            CallDREQuerySubscriberPromotionResponseInternal res = new CallDREQuerySubscriberPromotionResponseInternal();
            try
            {
                if (string.IsNullOrEmpty(request.msisdn))
                {
                    Log.Info("msisdn can not be null.");
                }

                if (string.IsNullOrEmpty(request.promotionId))
                {
                    Log.Info("promotionId can not be null.");
                }

                if (request.promotionId == "0")
                {
                    Log.Info("promotionId can not be 0.");
                }

                if (string.IsNullOrEmpty(request.serverURL))
                {
                    Log.Info("DREServiceAddress is not configured.");
                }

            

                #region httpWeb

                QuerySubscriberPromotionReq DRERequest = new QuerySubscriberPromotionReq();
                DRERequest.scp_id = "crm_dataTransfer";
                DRERequest.msisdn = request.msisdn;
                DRERequest.promotionId = request.promotionId;


                byte[] b = _httpWebRequestHelper.GetResponse(request.serverURL, request.millionSecond, DRERequest.Serialize());


                QuerySubscriberPromotionRes DREResponse = new QuerySubscriberPromotionRes();
                DREResponse = new QuerySubscriberPromotionRes().Deserialize(b);

                #endregion


                if (!DREResponse.errorCode.Equals(0))
                {
                    Log.Info("External Server Down exception.");
                }

                res.sdp_id = DREResponse.sdp_id;
                res.promotionId = DREResponse.promotionId;
                res.currentLimit = DREResponse.currentLimit;
                res.errorCode = DREResponse.errorCode;
                res.frozenLimit = DREResponse.frozenLimit;

            }
            catch (Exception ex)
            {
                Log.Info(string.Format("[Retrieve Subscriber Promotion in DRE] msisdn:{0},promotionId:{1},serverURL:{2} failed:  " + ex.Message,
                    request.msisdn, request.promotionId, request.serverURL));

                res.errorCode = -100;
            }

            Log.Info(string.Format("[End Invoke QuerySubscriberPromotion in DRE] SdpID:{0} msisdn:{1},PromotionId:{2},return code:{3},currentLimit:{4},frozenLimit:{5}", 
                res.sdp_id, request.msisdn, res.promotionId, res.errorCode, res.currentLimit, res.frozenLimit));

            return res;
        }


    }
}
