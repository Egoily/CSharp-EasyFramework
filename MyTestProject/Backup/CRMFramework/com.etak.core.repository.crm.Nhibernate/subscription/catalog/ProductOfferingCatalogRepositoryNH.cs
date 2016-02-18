using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.subscription.catalog;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.subscription.catalog
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity ProductOfferingCatalog 
    /// </summary>
    /// <typeparam name="TProductOfferingCatalog">Entity managed by the repository, is or extends ProductOfferingCatalog</typeparam>
    public class ProductOfferingCatalogRepositoryNH<TProductOfferingCatalog> :
        NHibernateRepository<TProductOfferingCatalog, Int32>,
        IProductOfferingCatalogRepository<TProductOfferingCatalog> where TProductOfferingCatalog : ProductOfferingCatalog
    {
        
    }
}
