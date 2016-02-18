using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.model.operation.contract.product
{
    /// <summary>
    /// Interface for a Product Offering Based Response
    /// </summary>
    public interface IMultiProductOfferingBasedResponse
    {
        /// <summary>
        /// A list of products offerings to be returned
        /// </summary>
        IEnumerable<ProductOffering> ProductOfferings { get; set; }
    }
}
