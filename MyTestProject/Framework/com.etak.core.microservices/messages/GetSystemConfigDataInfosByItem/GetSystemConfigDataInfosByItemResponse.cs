using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetSystemConfigDataInfosByItem
{
    /// <summary>
    /// GetSystemConfigDataInfosByItemResponse with a list of SystemConfigData objects
    /// </summary>
    public class GetSystemConfigDataInfosByItemResponse : ResponseBase
    {
        /// <summary>
        /// A list with all the SystemConfigDataInfo returned
        /// </summary>
        public IEnumerable<SystemConfigDataInfo> SystemConfigData { get; set; }

    }
}
