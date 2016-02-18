using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Enum with the different possible statuses of a product
    /// </summary>
    public enum ProductStatuses
    {
        /// <summary>
        /// Product in under test and should not be shown in 
        /// any public API, but it's purchase should be allowed
        /// </summary>
        Test,

        /// <summary>
        /// Product is active and can be purchased
        /// </summary>
        Current,

        /// <summary>
        /// Product is disabled, and can't be purchased, and all the associations
        /// with this product must be distables.
        /// </summary>
        Disabled,

        /// <summary>
        /// The existing purchases of the product are valid, but no new associations 
        /// can be allowd.
        /// </summary>
        EndOfLife,

    }
}
