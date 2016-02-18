using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.ApplyCustomerPromotion
{
    /// <summary>
    /// DTO Response for Apply Customer Promotion
    /// </summary>
    public class ApplyCustomerPromotionResponseDTO : OrderResponseDTO,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// Subscription of the Customer
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
