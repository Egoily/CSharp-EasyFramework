using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.product;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.AssignProductToCustomer
{
    /// <summary>
    /// PurchaseIndividualProductForCustomerResponseDTO for individual product purchase
    /// </summary>
    public class AssignProductOfferingToCustomerResponseDTO : OrderResponseDTO, IProductOfferingBasedDTOResponse,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// Purchased Products
        /// </summary>
        public CustomerProductAssignmentDTO productPurchased { get; set; }


        /// <summary>
        /// ProductCatalogbased
        /// </summary>
        public ProductCatalogDTO ProductCatalog { get; set; }

        /// <summary>
        /// ISubscription based
        /// </summary>
        public model.subscription.SubscriptionDTO Subscription { get; set; }
    }
}
