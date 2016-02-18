using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetMultiLingualDescriptionById
{
    /// <summary>
    /// GetMultiLingualDescription By Id Response
    /// </summary>
    public class GetMultiLingualDescriptionByIdResponse : ResponseBase
    {
        /// <summary>
        /// MultiLingualDescription find
        /// </summary>
        public MultiLingualDescription MultiLingualDescription { get; set; }
    }
}
