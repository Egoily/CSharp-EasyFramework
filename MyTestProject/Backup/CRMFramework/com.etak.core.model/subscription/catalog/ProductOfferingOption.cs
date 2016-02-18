using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.subscription.catalog
{
    /// <summary>
    /// Express the dependency of a product to other set of products (expressed through a relation to an specific product, product that belongs to a category, 
    /// or product that belongs to a type).
    /// </summary>
    public abstract class ProductOfferingOption
    {
        public virtual int Id { get; set; }
        public virtual Int32 MinOccurs { get; set; }
        public virtual Int32 MaxOccurs { get; set; }

        public virtual ProductOfferingOptionTypes ProductOptionType { get; set; }

        public virtual ProductConflictResolutionsStrategies ConflictResolutionStrategy { get; set; }

        public virtual ProductOffering RelatedProductOffering { get; set; }

    }
}
