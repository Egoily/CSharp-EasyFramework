using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetSettingInfosByDealerId
{
    /// <summary>
    /// Request of GetSettingInfosByDealerIdMS
    /// </summary>
    public class GetSettingInfosByDealerIdRequest : RequestBase
    {
        /// <summary>
        /// DealerId which settings need to be acquired
        /// </summary>
        public int DealerId { get; set; }
    }
}
