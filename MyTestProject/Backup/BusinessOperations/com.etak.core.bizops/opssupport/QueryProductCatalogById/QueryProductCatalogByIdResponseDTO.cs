using System.Collections.Generic;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.opssupport.QueryProductCatalogById
{
    /// <summary>
    /// Response DTO of QueryProductCatalogById
    /// </summary>
    public class QueryProductCatalogByIdResponseDTO : ResponseBaseDTO
    {
        /// <summary>
        /// ProductCatalogDTO
        /// </summary>
        public ProductCatalogDTO CustomerProductCatalogDto { get; set; }

        /// <summary>
        /// IList of ProductPurchaseChargingOptionDTO
        /// </summary>
        public IList<ProductPurchaseChargingOptionDTO> ProductPurchaseChargingOptionDto { get; set; }
    }
}
