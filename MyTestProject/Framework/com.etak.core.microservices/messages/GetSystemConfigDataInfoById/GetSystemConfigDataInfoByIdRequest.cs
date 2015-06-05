using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetSystemConfigDataInfoById
{
    /// <summary>
    /// GetSystemConfigDataInfoById Request
    /// </summary>
    public class GetSystemConfigDataInfoByIdRequest : RequestBase
    {
        /// <summary>
        /// The id of the SystemConfigData
        /// </summary>
        public string SystemConfigDataId { get; set; }

    }
}
