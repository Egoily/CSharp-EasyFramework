using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetSettingInfosByDealerId
{
    /// <summary>
    /// Response of GetSettingInfosByDealerIdMS
    /// </summary>
    public class GetSettingInfosByDealerIdResponse : ResponseBase
    {
        /// <summary>
        /// SettingInfos with certain dealerId
        /// </summary>
        public IEnumerable<SettingInfo> SettingInfos { get; set; }

    }
}
