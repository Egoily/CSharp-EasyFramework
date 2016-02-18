using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.CreateSessionInfo
{
    /// <summary>
    /// Create SessionInfo based on LoginInfo
    /// </summary>
    public class CreateSessionInfoRequest : RequestBase
    {
        /// <summary>
        /// SessionInfo to create
        /// </summary>
        public SessionInfo SessionInfo { get; set; }

    }
}
