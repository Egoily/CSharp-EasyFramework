using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.CreateSession
{
    /// <summary>
    /// Input response for CancelCustomerProductCreateSession output parameters in CORE model 
    /// </summary>
    public class CreateSessionResponseInternal : CreateNewOrderResponse
    {
        /// <summary>
        /// SessionInfo generated with the specific sessionId
        /// </summary>
        public SessionInfo SessionInfo { get; set; }
    }
}
