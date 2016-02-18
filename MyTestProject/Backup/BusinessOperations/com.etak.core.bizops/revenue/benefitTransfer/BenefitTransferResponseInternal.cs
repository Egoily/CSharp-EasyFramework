using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.BenefitTransfer
{
    /// <summary>
    /// Input response for BenefitTransfer output parameters in CORE model 
    /// </summary>
    public class BenefitTransferResponseInternal : CreateNewOrderResponse
    {
        /// <summary>
        /// SourceProductCustomerAssociation
        /// </summary>
        public List<CustomerProductAssignment> SourceProductCustomerAssociation { get; set; }

        /// <summary>
        /// DestinationProductCustomerAssociation
        /// </summary>
        public List<CustomerProductAssignment> DestinationProductCustomerAssociation { get; set; }

        /// <summary>
        /// TransferredPormotions
        /// </summary>
        public Dictionary<CrmCustomersPromotionInfo, decimal> TransferredPormotions { get; set; }

        /// <summary>
        /// PurchasedPromotionInfo
        /// </summary>
        public CrmCustomersPromotionInfo PurchasedPromotionInfo { get; set; }


    }
}
