using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// Interface to return a ChargeCatalogDTO Based Response
    /// </summary>
    public interface IChargeCatalogBasedDTOResponse
    {
        /// <summary>
        /// Charge Catalog DTO object to be returned
        /// </summary>
        ChargeCatalogDTO ChargeCatalogDto { get; set; }
    }
}
