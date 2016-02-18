using System;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.CancelCustomerAndSubscriptions
{
    /// <summary>
    /// Class for CancelCustomerAndSubscriptions request  in DTO model 
    /// </summary>
    public class CancelCustomerAndSubscriptionsRequestDTO : OrderRequestDTO, IMsisdnBasedDTORequest 
    {
        /// <summary>
        /// Msisdn used to get customer and subscriptions to cancel
        /// </summary>
        public string MSISDN { get; set; }

        /// <summary>
        /// NeedRecycle in case you try to execute CancelPortIn
        /// </summary>
        public Boolean NeedRecycle;
    }
}
