using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.vfes.implementation.bizOps.ChangeOfHolderBizOp
{
    /// <summary>
    /// the new holder for ChangeOfHolder 
    /// </summary>
    public class HolderToCreate
    {
        #region Info for New Holder
        /// <summary>
        /// The new holder for this MSISDN
        /// </summary>
        public virtual CustomerInfo CustomerInfo { get; set; }
        /// <summary>
        /// Flag to know if the Customer contains 4G Products
        /// </summary>
        public virtual bool IsContain4GProduct { get; set; }
        /// <summary>
        /// A list of the Purchased Products. Each product will contain the PurchaseChargeOption
        /// that will be applied
        /// </summary>
        public virtual List<Tuple<ProductOffering, ProductChargeOption>> PurchasedProducts { get; set; }

        /// <summary>
        /// Provision Information for the Customer
        /// </summary>
        public virtual CrmDefaultProvisionInfo ProvisionInfoDefinition { get; set; }

        /// <summary>
        /// If defined, will determine which BillCycle applies to the customer. If not, will be used
        /// the billcycle defined in the configuration
        /// </summary>
        public virtual BillCycle BillCycleForCustomer { get; set; }
        #endregion
    }
    /// <summary>
    /// the old holder for ChangeOfHolder 
    /// </summary>
    public class HolderToCancel
    {
        #region Info for Old holder
        /// <summary>
        /// The old holder for this MSISDN
        /// </summary>
        public virtual CustomerInfo CustomerInfo { get; set; }
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
        /// Subscription to be deleted or cancelled
        /// </summary>
        public virtual ResourceMBInfo Subscription { get; set; }

        /// <summary>
        /// Next bill run date
        /// </summary>
        public virtual DateTime NextBillRunDate { get; set; }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class ChangeOfHolderRequestInternal : CreateNewOrderRequest
    {

        /// <summary>
        /// Sim Card to be used in the registration process
        /// </summary>
        public virtual SIMCardInfo SimCard { get; set; }

        /// <summary>
        /// Number Property of the number
        /// </summary>
        //public virtual NumberPropertyInfo NumberPropertyDefinition { get; set; }
        public virtual NumberInfo NumberInPool { get; set; }

        /// <summary>
        /// the new holder will be created using the msisdn
        /// </summary>
        public HolderToCreate newHolder { get; set; }
        /// <summary>
        /// the old holder who is using the msisdn will be cancel 
        /// </summary>
        public HolderToCancel oldHolder { get; set; }

        /// <summary>
        /// the total amout that the old hold hasn't exhausted
        /// </summary>
        public decimal LeftoverDataAmount { get; set; }

        /// <summary>
        /// recurring promotiongroup members 
        /// </summary>
        public List<RmPromotionPlanInfo> recurringPromotions { get; set; }
    }
}
