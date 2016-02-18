using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.CallDREUpdateSubscriberPromotion
{
    /// <summary>
    /// CallDREQuerySubscriberPromotionResponseInternal
    /// </summary>
    public class CallDREUpdateSubscriberPromotionResponseInternal : CreateNewOrderResponse
    {
        /// <summary>
        /// SdpId
        /// </summary>
        public string  SdpId { get; set; }

        /// <summary>
        /// PromotionId
        /// </summary>
        public string PromotionId { get; set; }

    }
}
