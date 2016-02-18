using System;
using System.Collections.Generic;
using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.fullfilment.RegisterCustomer
{
    /// <summary>
    /// Register customer DTO Request
    /// </summary>
    public class RegisterCustomerRequestDTO : OrderRequestDTO, IICCIDBasedDTORequest, IMsisdnBasedDTORequest
    {
        /// <summary>
        /// iccId of the Simcard
        /// </summary>
        public string ICCID { get; set; }

        /// <summary>
        /// MSISDN used for the Customer
        /// </summary>
        public string MSISDN { get; set; }

        /// <summary>
        /// The HLRProfile used to create the Customer
        /// </summary>
        public string HLRProfile { get; set; }

        /// <summary>
        /// Customer Data DTO in reference to the Customer
        /// </summary>
        public CustomerDTO CustomerData { get; set; }

        /// <summary>
        /// The Credit Limit to be set to the Customer (if corresponds)
        /// </summary>
        public Decimal? CreditLimit { get; set; }

        /// <summary>
        /// A list of Products purchased by the customer
        /// </summary>
        public IList<ProductCatalogDTO> PurchasedProducts { get; set; }

        /// <summary>
        /// The BillCycle Id to be set to the customer's account.
        /// If it's not set, will be used the default BillCycle per MVNO
        /// </summary>
        public Int32? BillCycleId { get; set; }

        /// <summary>
        /// Determines if the register operation needs Subscription or not.
        /// True: The register operation can be done without subscription
        /// False: The register needs a subscription to be done
        /// </summary>
        public Boolean WithoutSubscription { get; set; }
    }
}
