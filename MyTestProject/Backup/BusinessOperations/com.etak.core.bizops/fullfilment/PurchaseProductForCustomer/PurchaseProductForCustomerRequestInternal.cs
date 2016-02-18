using System;
using System.Collections.Generic;
using com.etak.core.bizops.helper;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.bizops.fullfilment.PurchaseProductForCustomer
{
    /// <summary>
    /// PurchaseProductForCustomerRequestInternal
    /// </summary>
    public class PurchaseProductForCustomerRequestInternal : CreateNewOrderRequest, ICustomerBasedRequest
    {
        /// <summary>
        /// Invoice to use when called from Register/PortIn (new customer )
        /// </summary>
        public virtual Invoice Invoice { get; set; }

        /// <summary>
        /// AccountDefinition
        /// </summary>
        public virtual Account AccountDefinition { get; set; }
       

        /// <summary>
        /// ForceCreditLimit
        /// </summary>
        public virtual Decimal? ForceCreditLimit { get; set; }

        /// <summary>
        /// TypeOfPurchaseProductOperation
        /// </summary>
        public virtual TypeOfPurchaseProduct TypeOfPurchaseProductOperation { get; set; }

        /// <summary>
        /// DatetimePurchase
        /// </summary>
        public virtual DateTime? DatetimePurchase { get; set; }


        /// <summary>
        /// CustomerInfo, autoMapped by ICustomerBasedRequest
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }


        /// <summary>
        /// Flattened dictioanry with all the products for purchase with chargeoption (includes child products)
        /// </summary>
        public List<Tuple<ProductOffering, ProductChargeOption>> listTuplePoducts { get; set; }

        /// <summary>
        /// Determines if the register operation needs Subscription or not.
        /// True: The register operation can be done without subscription
        /// False: The register needs a subscription to be done
        /// </summary>
        public Boolean WithoutSubscription { get; set; }
    }
}
