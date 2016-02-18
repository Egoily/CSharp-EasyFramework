using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetSystemConfigDataInfoById
{
    /// <summary>
    /// Response for GetSystemConfigDataInfoById
    /// </summary>
    public class GetSystemConfigDataInfoByIdResponse : ResponseBase
    {
        /// <summary>
        /// SystemConfigData obtained
        /// </summary>
        public SystemConfigDataInfo SystemConfigData { get; set; }

    }
}
