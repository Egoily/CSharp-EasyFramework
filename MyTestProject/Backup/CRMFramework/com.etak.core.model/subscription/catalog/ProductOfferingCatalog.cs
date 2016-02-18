using System;
using System.Collections.Generic;

namespace com.etak.core.model.subscription.catalog
{

    /// <summary>
    /// Defines a commercial offer for a MNO through a given channel
    /// </summary>
    public class ProductOfferingCatalog
    {
        /// <summary>
        /// Unique ID of the product catalog
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// All products available in the catalog
        /// </summary>
        public virtual IList<ProductOffering> ProductOfferingsInCatalog { get; set; }

        /// <summary>
        /// The operator to which this catalog is available for
        /// </summary>
        public virtual DealerInfo MNO { get; set; }

        /// <summary>
        /// The channel to which is this catalog is available for.
        /// </summary>
        public virtual String Channel { get; set; }
    }
}
