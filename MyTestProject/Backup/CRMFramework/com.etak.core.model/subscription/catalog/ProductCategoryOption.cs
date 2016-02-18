using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.subscription.catalog
{
    public class ProductCategoryOption : ProductOfferingOption
    {
        public virtual String ProductCategory { get; set; }
        public virtual String ProductSubCategory { get; set; }
    }
}
