using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.repository.crm.subscription.catalog
{
    /// <summary>
    /// Interface for repository of entity ProductOfferingOption
    /// </summary>
    /// <typeparam name="TProductOfferingOption">The type of the managed entity is or extends ProductOfferingOption</typeparam>
    public interface IProductOptionRepository<TProductOfferingOption> : IRepository<TProductOfferingOption, Int32> where TProductOfferingOption : ProductOfferingOption
    {

    }
}
