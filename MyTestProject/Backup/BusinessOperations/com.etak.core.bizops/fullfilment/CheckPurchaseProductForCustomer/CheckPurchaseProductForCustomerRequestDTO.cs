using System;
using System.Collections.Generic;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.fullfilment.CheckPurchaseProductForCustomer
{
    /// <summary>
    /// Request DTO of CheckPurchaseProductForCustomerRequest BizOp
    /// </summary>
    public class CheckPurchaseProductForCustomerRequestDTO : RequestBaseDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// List of products to purcharse
        /// </summary>
        public List<ProductCatalogDTO> ProductCatalogDTOs { get; set; }

        /// <summary>
        /// Specific credit limit to set
        /// </summary>
        public Decimal? ForceCreditLimit { get; set; }

        /// <summary>
        /// Purchase product date
        /// </summary>
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Customer id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The Tax Category to be applied in the charge
        /// </summary>
        public int TaxCategory { get; set; }
    }
}
