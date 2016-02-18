using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.product;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.CancelCustomerProduct
{
    /// <summary>
    /// Class for CancelCustomerProduct response  in DTO model
    /// </summary>
    public class CancelCustomerProductResponseDTO : OrderResponseDTO,ISubscriptionBasedDTOResponse,ICustomerBasedDTOResponse,IProductOfferingBasedDTOResponse
    {
        /// <summary>
        /// Subscription owning the product
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
        /// <summary>
        /// Customer owning the product
        /// </summary>
        public CustomerDTO Customer { get; set; }
        /// <summary>
        /// Product cancelled
        /// </summary>
        public ProductCatalogDTO ProductCatalog { get; set; }
    }
}
