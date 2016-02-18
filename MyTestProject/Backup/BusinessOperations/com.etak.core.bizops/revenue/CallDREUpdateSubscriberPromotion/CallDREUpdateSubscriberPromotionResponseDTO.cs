using System.Collections.Generic;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.revenue.CallDREUpdateSubscriberPromotion
{

    /// <summary>
    /// CallDREQuerySubscriberPromotionResponseDTO
    /// </summary>
    public class CallDREUpdateSubscriberPromotionResponseDTO : OrderResponseDTO
    {

        /// <summary>
        /// SdpId
        /// </summary>
        public string SdpId { get; set; }

        /// <summary>
        /// PromotionId
        /// </summary>
        public string PromotionId { get; set; }
    }
}
