using System.Collections.Generic;
using com.etak.core.model.dto;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.vfes.implementation.bizOps.ChangeOfHolderBizOp
{
    /// <summary>
    /// ChangeOfHolderRequestDTO
    /// </summary>
    public class ChangeOfHolderRequestDTO:OrderRequestDTO
    {
         /// <summary>
        /// MSISDN
         /// </summary>
        public string MSISDN { get; set; }
        /// <summary>
        /// DocumentType
        /// </summary>
        public DocumentTypes DocumentType { get; set; }
        /// <summary>
        /// DocumentNumber
        /// </summary>
        public string DocumentNumber { get; set; }
        /// <summary>
        /// CustomerData
        /// </summary>
        public CustomerDTO CustomerData { get; set; }
        /// <summary>
        /// PurchasedProducts
        /// </summary>
        public IList<ProductCatalogDTO> PurchasedProducts { get; set; }
    }
}
