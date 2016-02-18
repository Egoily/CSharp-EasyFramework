using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetMultiLingualDescriptionById
{
    /// <summary>
    /// GetmullingualDescriptionById Request
    /// </summary>
    public class GetMultiLingualDescriptionByIdRequest : RequestBase
    {
        /// <summary>
        /// The Id corresponding with the multi lingual description
        /// </summary>
        public Int32 MultiLingualDescriptionId { get; set; }
    }
}
