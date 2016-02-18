using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.numberInfo;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.fullfilment.CancelCustomerAndSubscriptions
{
    /// <summary>
    /// Input request for CancelCustomerAndSubscriptions input in CORE model
    /// </summary>
    public class CancelCustomerAndSubscriptionsRequestInternal : CreateNewOrderRequest, ISubscriptionLastActiveBasedRequest, INumberInfoBasedRequest
    {
        /// <summary>
        /// SimCardInfo will be expriated or deleted 
        /// </summary>
        public virtual SIMCardInfo SimCardInfo { get; set; }

        /// <summary>
        /// Customer Account Associations will be deleted
        /// </summary>
        public virtual CustomerAccountAssociation CustomerAccountAssociation { get; set; }

        /// <summary>
        /// CustomerDataRamingLimits will be deleted
        /// </summary>
        public virtual IEnumerable<CustomerDataRoamingLimit> CustomerDataRoamingLimits { get; set; }

        /// <summary>
        /// CustomerDataRamingLimitsCustomerDataRoamingLimitNotifications will be deleted
        /// </summary>
        public virtual IEnumerable<CustomerDataRoamingLimitNotification> CustomerDataRoamingLimitNotifications { get; set; }

        /// <summary>
        /// RoamingBlackListInfos will be deleted
        /// </summary>
        public virtual IEnumerable<RoamingBlackListInfo> RoamingBlackListInfos { get; set; }

        /// <summary>
        /// NeedRecycle in case you try to execute CancelPortIn
        /// </summary>
        public virtual bool NeedRecycle { get; set; }

        /// <summary>
        /// The customer owning the subcription to cancel
        /// </summary>
        public virtual CustomerInfo CustomerInfo { get; set; }

        /// <summary>
        /// Subscription to be deleted or cancelled
        /// </summary>
        public virtual ResourceMBInfo Subscription { get; set; }

        /// <summary>
        /// Msisdn used to Recycle Ip
        /// </summary>
        public virtual string MSISDN { get; set; }

        /// <summary>
        /// The Number to be CoolDown or deleted
        /// </summary>
        public virtual NumberInfo NumberInPool { get; set; }

        /// <summary>
        /// Next bill run date
        /// </summary>
        public virtual DateTime NextBillRunDate { get; set; }
    }
}
