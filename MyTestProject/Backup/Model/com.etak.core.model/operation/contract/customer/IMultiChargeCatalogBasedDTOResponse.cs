using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// Interface to return a Multi ChargeCatalogDTO Based Response
    /// </summary>
    public interface IMultiChargeCatalogBasedDTOResponse
    {
        /// <summary>
        /// A list of ChargeCatalogDTOs to be returned
        /// </summary>
        IEnumerable<ChargeCatalogDTO> ChargeCatalogDtos { get; set; }
    }
}
