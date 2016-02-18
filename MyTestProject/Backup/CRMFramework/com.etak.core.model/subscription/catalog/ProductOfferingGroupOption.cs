using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.subscription.catalog
{
    public class ProductOfferingGroupOption : ProductOfferingOption
    {
        public virtual ProductOfferingGroup Group { get; set; }

    }
}
