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
    public interface IProductBasedDTOResponse
    {
        /// <summary>
        /// The ProductCatalogDTO to be returned
        /// </summary>
        ProductCatalogDTO ProductCatalog { get; set; }
    }
}
