using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.CreateSessionInfo
{
     
    /// <summary>
    /// Response for CreateSessionInfo
    /// </summary>
    public class CreateSessionInfoResponse : ResponseBase
    {
        /// <summary>
        /// SessionInfo created
        /// </summary>
        public SessionInfo SessionInfo { get; set; }

    }
}
