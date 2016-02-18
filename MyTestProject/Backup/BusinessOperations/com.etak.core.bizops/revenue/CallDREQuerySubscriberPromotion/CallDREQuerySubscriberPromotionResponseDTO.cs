using System.Collections.Generic;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.revenue.CallDREQuerySubscriberPromotion
{

    /// <summary>
    /// CallDREQuerySubscriberPromotionResponseDTO
    /// </summary>
    public class CallDREQuerySubscriberPromotionResponseDTO : OrderResponseDTO
    {


        /// <summary>
        /// sdp_id
        /// </summary>
        public string sdp_id { get; set; }

        /// <summary>
        /// promotionId
        /// </summary>
        public string promotionId { get; set; }

        /// <summary>
        /// currentLimit
        /// </summary>
        public decimal currentLimit { get; set; }


        /// <summary>
        /// currentLimit
        /// </summary>
        public decimal frozenLimit { get; set; }
    }
}
