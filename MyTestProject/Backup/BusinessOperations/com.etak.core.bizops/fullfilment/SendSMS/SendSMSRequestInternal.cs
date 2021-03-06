﻿using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.SendSMS
{
    /// <summary>
    /// Request Internal of SendSMSBizOp.
    /// </summary>
    public class SendSMSRequestInternal : CreateNewOrderRequest
    {
        /// <summary>
        /// SmsLogInfo to be Created.
        /// </summary>
        public SmsLogInfo SmsLogInfo { set; get; }
    }
}
