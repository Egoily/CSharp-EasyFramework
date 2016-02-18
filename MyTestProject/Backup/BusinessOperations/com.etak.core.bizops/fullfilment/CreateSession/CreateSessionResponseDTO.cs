using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.CreateSession
{
    /// <summary>
    /// Class for CancelCustomerProduct response  in DTO model
    /// </summary>
    public class CreateSessionResponseDTO : OrderResponseDTO
    {
        /// <summary>
        /// SessionId generated 
        /// </summary>
        public string SessionId { get; set; }

    }
}
