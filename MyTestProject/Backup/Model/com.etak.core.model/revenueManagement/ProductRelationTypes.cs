using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Possible kind of relationships between products
    /// </summary>
    public enum ProductRelationTypes
    {
        /// <summary>
        /// The source product is incompatible with the destination product
        /// </summary>
        Incompatible = 0,

        /// <summary>
        /// The source product requires the destination product
        /// </summary>
        Required = 1,

    }
}
