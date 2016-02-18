using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.numberInfo;
using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.bizops.fullfilment.RegisterCustomer
{
    /// <summary>
    /// Request Internal with all the Core Objects needed to register a customer
    /// </summary>
    public class RegisterCustomerRequestInternal : CreateNewOrderRequest, ISimCardBasedRequest, INumberInfoBasedRequest
    {
        /// <summary>
        /// If set, will determine the credit limit setted to the Customer
        /// </summary>
        public virtual Decimal? CreditLimit { get; set; }

        /// <summary>
        /// The CustomerInfo that contains all the information related with the new customer
        /// </summary>
        public virtual CustomerInfo CustomerInfoDefinition { get; set; }

        /// <summary>
        /// Flag to know if the Customer contains 4G Products
        /// </summary>
        public virtual bool IsContain4GProduct { get; set; }
        
        /// <summary>
        /// Number Property of the number
        /// </summary>
        //public virtual NumberPropertyInfo NumberPropertyDefinition { get; set; }
        public virtual NumberInfo NumberInPool { get; set; }

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
        /// Sim Card to be used in the registration process
        /// </summary>
        public virtual SIMCardInfo SimCard { get; set; }

        /// <summary>
        /// If defined, will determine which BillCycle applies to the customer. If not, will be used
        /// the billcycle defined in the configuration
        /// </summary>
        public virtual BillCycle BillCycleForCustomer { get; set; }

        /// <summary>
        /// Determines if the register operation needs Subscription or not.
        /// True: The register operation can be done without subscription
        /// False: The register needs a subscription to be done
        /// </summary>
        public virtual Boolean WithoutSubscription { get; set; }
    }
}
