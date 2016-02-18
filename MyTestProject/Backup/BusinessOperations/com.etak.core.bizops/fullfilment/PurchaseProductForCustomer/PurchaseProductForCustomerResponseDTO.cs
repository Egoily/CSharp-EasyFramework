using System.Collections.Generic;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.PurchaseProductForCustomer
{
    /// <summary>
    /// PurchaseProductForCustomerResponseDTO
    /// </summary>
    public class PurchaseProductForCustomerResponseDTO : OrderResponseDTO,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// List of purchased Products
        /// </summary>
        public IList<CustomerProductAssignmentDTO> productPurchase { get; set; }

        /// <summary>
        /// List of Deprovisones Products
        /// </summary>
        public IList<ProductCatalogDTO> deprovisionedProductList { get; set; }
        /// <summary>
        /// Subscription of the purchaser
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
