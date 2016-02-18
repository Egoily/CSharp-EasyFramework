using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.CreateSmsLogInfo
{
    /// <summary>
    /// Create SmsLogInfo based on LoginInfo
    /// </summary>
    public class CreateSmsLogInfoRequest : RequestBase
    {
        /// <summary>
        /// SmsLogInfo to create
        /// </summary>
        public SmsLogInfo SmsLogInfo { get; set; }

    }
}
