using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.repository.crm.subscription.catalog
{
    /// <summary>
    /// Interface for repository of entity ProductOfferingCatalog
    /// </summary>
    /// <typeparam name="TProductOfferingCatalog">The type of the managed entity is or extends ProductOfferingCatalog</typeparam>
    public interface IProductOfferingCatalogRepository<TProductOfferingCatalog> : IRepository<TProductOfferingCatalog, Int32> where TProductOfferingCatalog : ProductOfferingCatalog
    {

    }
}
