using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.ApplyCustomerPromotion
{
    /// <summary>
    /// Response Internal for ApplyCustomerPromotion
    /// </summary>
    public class ApplyCustomerPromotionResponseInternal : CreateNewOrderResponse,ISubscriptionBasedResponse
    {
        /// <summary>
        /// Customer updated with all the promotions assigned
        /// </summary>
        public CustomerInfo Customer { get; set; }

        /// <summary>
        /// List with all the promotions created for the Customer
        /// </summary>
        public IList<CrmCustomersPromotionInfo> CrmCustomersPromotionInfos { get; set; }
        /// <summary>
        /// Subscription of the Customer
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
