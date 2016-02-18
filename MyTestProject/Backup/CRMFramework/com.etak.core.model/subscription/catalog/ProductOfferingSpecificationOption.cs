using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.subscription.catalog
{
    public class ProductOfferingSpecificationOption : ProductOfferingOption
    {
        public virtual ProductOffering SpecifiedProductOffering { get; set; }
    }
}
