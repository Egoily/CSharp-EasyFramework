using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetOrderById
{
    /// <summary>
    /// Response for GetOrderByIdMs
    /// </summary>
    public class GetOrderByIdResponse : ResponseBase
    {
        /// <summary>
        /// Order obtained
        /// </summary>
        public Order Order { get; set; }
    }
}
