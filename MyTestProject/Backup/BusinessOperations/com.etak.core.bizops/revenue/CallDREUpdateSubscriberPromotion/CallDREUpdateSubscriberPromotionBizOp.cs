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


namespace com.etak.core.bizops.revenue.CallDREUpdateSubscriberPromotion
{
    /// <summary>
    /// CallDREQuerySubscriberPromotionBizOp
    /// </summary>
    public class CallDREUpdateSubscriberPromotionBizOp : AbstractSinglePhaseOrderProcessor<CallDREUpdateSubscriberPromotionRequestDTO, CallDREUpdateSubscriberPromotionResponseDTO, CallDREUpdateSubscriberPromotionRequestInternal, CallDREUpdateSubscriberPromotionResponseInternal, CallDREUpdateSubscriberPromotionOrder>
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
            get { return OperationCodes.CallDREUpdateSubscriberPromotionOperation; }

        }

        /// <summary>
        /// OperationDiscriminator
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.CallDREUpdateSubscriberPromotionOperation; }
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
        public CallDREUpdateSubscriberPromotionBizOp()
        {
            HttpWebRequestHelper = new HttpWebRequestHelper();
        }


        /// <summary>
        /// MapNotAutomappedOrderInboundProperties
        /// </summary>
        /// <param name="request"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedOrderInboundProperties(CallDREUpdateSubscriberPromotionRequestDTO request, ref CallDREUpdateSubscriberPromotionRequestInternal coreInput)
        {

            coreInput.Msisdn = request.Msisdn;
            coreInput.PromotionId = request.PromotionId;
            coreInput.IncrementValue = request.IncrementValue;
            coreInput.DecrementValue = request.DecrementValue;
            coreInput.ServerURL = request.ServerURL;

        }

        /// <summary>
        /// MapNotAutomappedOrderOutboundProperties
        /// </summary>
        /// <param name="source"></param>
        /// <param name="coreOutput"></param>
        protected override void MapNotAutomappedOrderOutboundProperties(CallDREUpdateSubscriberPromotionResponseInternal source, ref CallDREUpdateSubscriberPromotionResponseDTO coreOutput)
        {

            coreOutput.errorCode = source.ErrorCode;
            coreOutput.SdpId = source.SdpId;
            coreOutput.PromotionId = source.PromotionId;


        }


        /// <summary>
        /// ProcessRequest
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override CallDREUpdateSubscriberPromotionResponseInternal ProcessRequest(CallDREUpdateSubscriberPromotionOrder order, CallDREUpdateSubscriberPromotionRequestInternal request)
        {
            Log.Info("[Begin Invoke UpdateSubscriberPromotion in DRE]");

            CallDREUpdateSubscriberPromotionResponseInternal response = new CallDREUpdateSubscriberPromotionResponseInternal();

            try
            {

                #region httpWeb

                UpdateSubscriberPromotionReq usbReq = new UpdateSubscriberPromotionReq();
                UpdateSubscriberPromotionRes usbResp = new UpdateSubscriberPromotionRes();
                DateTime dtrequest = System.DateTime.Now;

                if (string.IsNullOrEmpty(request.Msisdn))
                {
                    Log.Info("msisdn can not be null.");
                }

                if (string.IsNullOrEmpty(request.ServerURL))
                {
                    Log.Info("DREServiceAddress is not configured.");
                }

                usbReq.SdpId = "crm_dataTransfer";
                usbReq.Msisdn = request.Msisdn;
                usbReq.IncrementValue = request.IncrementValue;
                usbReq.DecrementValue = request.DecrementValue;
                usbReq.PromotionId = request.PromotionId;

                

                byte[] b = _httpWebRequestHelper.GetResponse(request.ServerURL, null, usbReq.Serialize());


                DateTime dtResponse = System.DateTime.Now;


                Encoding encoding = new UTF8Encoding(false);
                usbResp = new UpdateSubscriberPromotionRes().Deserialize(b);

                #endregion

                response.SdpId = usbResp.SdpId;
                response.PromotionId = usbResp.PromotionId;
                response.ErrorCode = usbResp.ErrorCode;

                if (usbResp.ErrorCode == 2)
                {
                    Log.Info(string.Format("Deduct customer limit failure,serverURL{0},msisdn:{1},promotionid:{2},descrement:{3}",
                        request.ServerURL, request.Msisdn, request.PromotionId, request.DecrementValue));
                }

            }
            catch (Exception ex)
            {
                Log.Info(string.Format("[Update Subscriber Balance in DRE] msisdn:{0},promotionId{1},failed:  " + ex.Message,
                    request.Msisdn, request.PromotionId.ToString()));
            }

            Log.Info(string.Format("[End Invoke UpdateSubscriberPromotion in DRE] SdpID:{0},msisdn:{1},PromotionId:{2},return code:{3},increment:{4},descrement:{5}",
                response.SdpId, request.Msisdn, response.PromotionId, response.ErrorCode, request.IncrementValue, request.DecrementValue));

            return response;
        }

        /// <summary>
        /// ReadStreamToEnd
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private byte[] ReadStreamToEnd(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] buffer = new byte[4096];

                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead <= 0)
                        break;

                    ms.Write(buffer, 0, bytesRead);
                }

                return ms.ToArray();
            }
        }

    }
}
