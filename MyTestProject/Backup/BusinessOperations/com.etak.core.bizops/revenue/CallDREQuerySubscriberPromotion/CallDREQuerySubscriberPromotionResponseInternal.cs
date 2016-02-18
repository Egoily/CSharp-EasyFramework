using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.CallDREQuerySubscriberPromotion
{
    /// <summary>
    /// CallDREQuerySubscriberPromotionResponseInternal
    /// </summary>
    public class CallDREQuerySubscriberPromotionResponseInternal : CreateNewOrderResponse
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
        /// errorCode
        /// </summary>
        public int errorCode { get; set; }

        /// <summary>
        /// currentLimit
        /// </summary>
        public decimal frozenLimit { get; set; }


    }
}
