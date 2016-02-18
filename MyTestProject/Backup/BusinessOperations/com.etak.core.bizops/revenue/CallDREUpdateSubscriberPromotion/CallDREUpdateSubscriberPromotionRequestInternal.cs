using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.CallDREUpdateSubscriberPromotion
{

    /// <summary>
    /// CallDREQuerySubscriberPromotionRequestInternal
    /// </summary>
    public class CallDREUpdateSubscriberPromotionRequestInternal : CreateNewOrderRequest
    {
        /// <summary>
        /// Msisdn
        /// </summary>
        public string Msisdn { get; set; }

        /// <summary>
        /// PromotionId
        /// </summary>
        public long PromotionId { get; set; }

        /// <summary>
        /// IncrementValue
        /// </summary>
        public float IncrementValue { get; set; }

        /// <summary>
        /// DecrementValue
        /// </summary>
        public float DecrementValue { get; set; }

        /// <summary>
        /// serverURL
        /// </summary>
        public string ServerURL { get; set; }

    }
}
