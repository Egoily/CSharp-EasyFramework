using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.CreateSmsLogInfo
{
     
    /// <summary>
    /// Response for CreateSmsLogInfo
    /// </summary>
    public class CreateSmsLogInfoResponse : ResponseBase
    {
        /// <summary>
        /// SmsLogInfo created
        /// </summary>
        public SmsLogInfo SmsLogInfo { get; set; }

    }
}
