using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.CallDREQuerySubscriberPromotion
{

    /// <summary>
    /// CallDREQuerySubscriberPromotionRequestInternal
    /// </summary>
    public class CallDREQuerySubscriberPromotionRequestInternal : CreateNewOrderRequest
    {
        /// <summary>
        /// msisdn
        /// </summary>
        public string msisdn { get; set; }

        /// <summary>
        /// promotionId
        /// </summary>
        public string promotionId { get; set; }


        /// <summary>
        /// millionSecond
        /// </summary>
        public int millionSecond { get; set; }

        /// <summary>
        /// serverURL
        /// </summary>
        public string serverURL { get; set; }

    }
}
