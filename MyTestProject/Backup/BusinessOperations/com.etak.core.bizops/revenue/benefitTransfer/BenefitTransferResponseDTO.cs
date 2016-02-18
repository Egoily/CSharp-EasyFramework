using System.Collections.Generic;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.revenue.BenefitTransfer
{
    /// <summary>
    /// Class for BenefitTransfer response  in DTO model 
    /// </summary>
    public class BenefitTransferResponseDTO : OrderResponseDTO
    {
        /// <summary>
        /// SourceProductCustomerAssociation
        /// </summary>
        public List<CustomerProductAssignmentDTO> SourceProductCustomerAssociation { get; set; }

        /// <summary>
        /// DestinationProductCustomerAssociation
        /// </summary>
        public List<CustomerProductAssignmentDTO> DestinationProductCustomerAssociation { get; set; }

        /// <summary>
        /// TransferredPormotions
        /// </summary>
        public Dictionary<CrmCustomersPromotionInfoDTO, decimal> TransferredPormotions { get; set; }

        /// <summary>
        /// PurchasedPromotionInfo
        /// </summary>
        public CrmCustomersPromotionInfoDTO PurchasedPromotionInfo { get; set; }


    }
}
