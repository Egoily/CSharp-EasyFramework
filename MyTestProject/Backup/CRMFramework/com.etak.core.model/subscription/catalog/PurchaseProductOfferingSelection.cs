using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.subscription.catalog
{
    /// <summary>
    /// Specification about the purchase of a product offering that represents a logical purchase
    /// </summary>
    public class PurchaseProductOfferingSelection
    {
        /// <summary>
        /// Product offering to be purchased
        /// </summary>
        public virtual ProductOffering PurchasedProductOffering { get; set; }

        /// <summary>
        /// All the optional product offerings that are added to the main product offering
        /// </summary>
        public virtual IList<PurchaseProductOfferingSelection> PurchaseOptions { get; set; }
       
    }
}
