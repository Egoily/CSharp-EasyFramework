using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.SendSMS
{
    /// <summary>
    /// Response Internal of SendSMSBizOp.
    /// </summary>
    public class SendSMSResponseInternal : CreateNewOrderResponse
    {
        /// <summary>
        /// SmsLogInfo had be Created.
        /// </summary>
        public SmsLogInfo SmsLogInfo { set; get; }
    }
}
