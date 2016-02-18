using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation.contract.product
{
    /// <summary>
    /// Interface for a Product Based Response
    /// </summary>
    public interface IMultiProductBasedResponse
    {
        /// <summary>
        /// A list of products to be returned
        /// </summary>
        IEnumerable<Product> Products { get; set; }
    }
}
