using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetOrderById
{
    /// <summary>
    /// Request for GetOrderByIdMs
    /// </summary>
    public class GetOrderByIdRequest : RequestBase
    {
        /// <summary>
        /// The OrderId to be get
        /// </summary>
        public long OrderId { get; set; }
    }
}
