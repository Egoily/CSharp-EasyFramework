using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.model.operation.contract.product
{
    /// <summary>
    /// Interface to implement a Product Offering Based Response
    /// </summary>
    public interface IProductOfferingBasedResponse
    {
        /// <summary>
        /// The product offering to be returned
        /// </summary>
        ProductOffering ProductOffering { get; set; }
    }
}
