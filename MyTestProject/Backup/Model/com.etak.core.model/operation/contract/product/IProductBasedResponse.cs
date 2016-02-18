using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation.contract.product
{
    /// <summary>
    /// Interface to implement a Product Based Response
    /// </summary>
    public interface IProductBasedResponse
    {
        /// <summary>
        /// The product to be returned
        /// </summary>
        Product Product { get; set; }
    }
}
