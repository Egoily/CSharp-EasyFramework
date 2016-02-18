using com.etak.core.microservices.messages.CreateSmsLogInfo;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.etak.core.bizops.fullfilment.SendSMS
{
    /// <summary>
    /// Simple operation to Send SMS.
    /// </summary>
    public class SendSMSBizOp : AbstractSinglePhaseOrderProcessor<SendSMSRequestDTO, SendSMSResponseDTO, SendSMSRequestInternal, SendSMSResponseInternal, SendSMSOrder>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Operation Code for SendSMS
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.SendSMSOperation; }
        }

        /// <summary>
        /// Operation Discriminator for SendSMS
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.SendSMS; }
        }

        /// <summary>
        /// Mapping Internal request to DTO output
        /// </summary>
        /// <param name="request">SendSMSRequestDTO</param>
        /// <param name="coreInput">SendSMSRequestInternal</param>
        protected override void MapNotAutomappedOrderInboundProperties(SendSMSRequestDTO request, ref SendSMSRequestInternal coreInput)
        {
            if (!string.IsNullOrEmpty(request.MSISDNs))
            {
                DateTime createDate = DateTime.Now;
                coreInput.SmsLogInfo = coreInput.SmsLogInfo ?? new model.SmsLogInfo();
                coreInput.SmsLogInfo.CategoryId = request.CategoryId;
                coreInput.SmsLogInfo.Sender = request.Sender;
                coreInput.SmsLogInfo.SearchByIsSent = request.SearchByIsSent;
                coreInput.SmsLogInfo.IsSent = request.IsSent;
                coreInput.SmsLogInfo.SearchByVirtualDeleted = request.SearchByVirtualDeleted;
                coreInput.SmsLogInfo.VirtualDeleted = request.VirtualDeleted;
                coreInput.SmsLogInfo.MSISDN = request.MSISDNs;
                coreInput.SmsLogInfo.Priority = request.Priority;
                coreInput.SmsLogInfo.DealerId = coreInput.MVNO.DealerID;
                coreInput.SmsLogInfo.SentDate = request.ScheduledDeliveryTime.HasValue ? request.ScheduledDeliveryTime : createDate;
                coreInput.SmsLogInfo.CreateDate = createDate;
                coreInput.SmsLogInfo.SmsMessage = request.Body;
                coreInput.SmsLogInfo.UserId = coreInput.User.UserID;
                coreInput.SmsLogInfo.ScheduleDeliveryTime = request.ScheduledDeliveryTime;
                coreInput.SmsLogInfo.Validity = request.Validity;
                coreInput.SmsLogInfo.TrySentTimes = request.TrySentTimes;
                coreInput.SmsLogInfo.MaxSentTimes = request.MaxSentTimes;
                coreInput.SmsLogInfo.Name1 = request.Name1;
                coreInput.SmsLogInfo.Name2 = request.Name2;
                coreInput.SmsLogInfo.Name3 = request.Name3;
                coreInput.SmsLogInfo.Name4 = request.Name4;
                coreInput.SmsLogInfo.Name5 = request.Name5;
                coreInput.SmsLogInfo.Name6 = request.Name6;
                coreInput.SmsLogInfo.Value1 = request.Value1;
                coreInput.SmsLogInfo.Value2 = request.Value2;
                coreInput.SmsLogInfo.Value3 = request.Value3;
                coreInput.SmsLogInfo.Value4 = request.Value4;
                coreInput.SmsLogInfo.Value5 = request.Value5;
                coreInput.SmsLogInfo.Value6 = request.Value6;
                coreInput.SmsLogInfo.NotificationType = request.NotificationType;
            }
        }

        /// <summary>
        /// Mapping Internal response to DTO output
        /// </summary>
        /// <param name="source">SendSMSResponseInternal</param>
        /// <param name="DTOOutput">SendSMSResponseDTO</param>
        protected override void MapNotAutomappedOrderOutboundProperties(SendSMSResponseInternal source, ref SendSMSResponseDTO DTOOutput)
        {
            DTOOutput.resultType = source.ResultType;
            DTOOutput.errorCode = source.ErrorCode;
            if (!string.IsNullOrEmpty(source.Message))
            {
                DTOOutput.messages = new string[] { source.Message };
            }
        }

        /// <summary>
        /// Process SendSMS function.
        /// </summary>
        /// <param name="order">SendSMS order</param>
        /// <param name="request">Internal request</param>
        /// <returns>Internal response</returns>
        public override SendSMSResponseInternal ProcessRequest(SendSMSOrder order, SendSMSRequestInternal request)
        {
            if (request.SmsLogInfo == null || string.IsNullOrEmpty(request.SmsLogInfo.MSISDN))
                throw new BusinessLogicErrorException("No MSISDN defined in request", BizOpsErrors.MSISDNNotFound);

            SendSMSResponseInternal response = new SendSMSResponseInternal()
            {
                ResultType = ResultTypes.Queued,
            };

            var createSMSLogInfoMS = MicroServiceManager.GetMicroService<CreateSmsLogInfoRequest, CreateSmsLogInfoResponse>();
            var createSmsLogInfoRequest = new CreateSmsLogInfoRequest()
            {
                SmsLogInfo = request.SmsLogInfo,
            };
            var createSMSLogInfoResponse = createSMSLogInfoMS.Process(createSmsLogInfoRequest, null);

            response = new SendSMSResponseInternal()
            {
                ResultType = createSMSLogInfoResponse.ResultType,
                ErrorCode = createSMSLogInfoResponse.ErrorCode,
                Message = createSMSLogInfoResponse.Message
            };

            return response;
        }
    }
}
