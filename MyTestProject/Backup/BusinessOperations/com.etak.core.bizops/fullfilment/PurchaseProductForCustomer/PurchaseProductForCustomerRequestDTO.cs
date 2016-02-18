using System;
using System.Collections.Generic;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;


namespace com.etak.core.bizops.fullfilment.PurchaseProductForCustomer
{
    /// <summary>
    /// PurchaseProductForCustomerRequestDTO
    /// </summary>
    public class PurchaseProductForCustomerRequestDTO: OrderRequestDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// Products List to purchase
        /// </summary>
        public IList<ProductCatalogDTO> products { get; set; }

        /// <summary>
        /// ForceCreditLimit Value (nullable)
        /// if null then the actual credit limit in the bundle is taken
        /// else the value for ForceCreditLimit prevails
        /// </summary>
        public Decimal? forceCreditLimit { get; set; }

        /// <summary>
        /// CustomerID, automapped by ICustomerIdBasedDTORequest
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Determines if the Purchase operation needs Subscription or not.
        /// True: The register operation can be done without subscription
        /// False: The register needs a subscription to be done
        /// </summary>
        public Boolean WithoutSubscription { get; set; }
    }
    

}
