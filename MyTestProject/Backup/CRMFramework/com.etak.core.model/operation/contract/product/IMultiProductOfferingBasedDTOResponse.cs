using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation.contract.product
{
    /// <summary>
    /// Interface for a Product Based DTO Response
    /// </summary>
    public interface IMultiProductOfferingBasedDTOResponse
    {
        /// <summary>
        /// A list of ProductCatalogDto
        /// </summary>
        IEnumerable<ProductCatalogDTO> ProductCatalogDtos { get; set; }
    }
}
