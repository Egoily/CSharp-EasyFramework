using System;
using System.Collections.Generic;
using com.etak.core.bizops.fullfilment.PurchaseProductForCustomer;
using com.etak.core.bizops.helper;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.bizops.fullfilment.CheckPurchaseProductForCustomer
{
    /// <summary>
    /// CheckPurchaseProductForCustomerRequestInternal
    /// </summary>
    public class CheckPurchaseProductForCustomerRequestInternal : RequestBase, ICustomerBasedRequest
    {
        /// <summary>
        /// CustomerInfo
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary>
        /// Dicctionary with products and chargeOption to be purchased
        /// </summary>
        public virtual List<Tuple<ProductOffering, ProductChargeOption>> ListTuplesPurchaseProducts { get; set; }

        /// <summary>
        /// Specific credit limit to set
        /// </summary>
        public virtual Decimal? ForceCreditLimit { get; set; }

        /// <summary>
        /// Purchase product date
        /// </summary>
        public virtual DateTime PurchaseDate { get; set; }

        /// <summary>
        ///  list of purchase product limit promotions configurations
        /// </summary>
        public virtual IEnumerable<MVNOConfigActionInfo> PuchaseProductLimitPromotions { get; set; }

        /// <summary>
        ///  list of purchase product unlimited promotions configurations
        /// </summary>
        public virtual IEnumerable<MVNOConfigActionInfo> PuchaseProductUnLimitedPromotions { get; set; }

        /// <summary>
        /// The tax definition that corresponds to the customer
        /// </summary>
        public virtual TaxDefinition TaxDefinition { get; set; }

        /// <summary>
        /// Next bill run date
        /// </summary>
        public virtual DateTime NextBillRunDate { get; set; }

        /// <summary>
        /// TypeOfPurchaseProductOperation
        /// </summary>
        public virtual TypeOfPurchaseProduct TypeOfPurchaseProductOperation { get; set; }



    }
}
