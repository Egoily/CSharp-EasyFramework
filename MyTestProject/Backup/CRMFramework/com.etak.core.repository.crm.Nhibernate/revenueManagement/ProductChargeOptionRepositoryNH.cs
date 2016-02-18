using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity ProductChargeOption 
    /// </summary>
    /// <typeparam name="TProductChargeOption">Entity managed by the repository, is or extends ProductChargeOption</typeparam>
    public class ProductChargeOptionRepositoryNH<TProductChargeOption> : 
        NHibernateRepository<TProductChargeOption, Int32>, 
        IProductChargeOptionRepository<TProductChargeOption> where TProductChargeOption : ProductChargeOption
    {
        /// <summary>
        /// Gets all the ProductChargeOption of a given product offering (This method in general, Product Offering already has the result of this query and cached)
        /// <see cref="com.etak.core.model.subscription.catalog.ProductOffering.ChargingOptions"/>
        /// </summary>
        /// <param name="productOfferingId">The id of the product offering to get the TProductChargeOption</param>
        /// <returns>the list of the TProductChargeOption of the product</returns>
        public IEnumerable<TProductChargeOption> GetByProductOfferingId(int productOfferingId)
        {
            return GetQuery().Where(x => x.ProductOffering.OfferedProduct.Id == productOfferingId).Cacheable().CacheRegion(CacheRegions.CatalogCacheRegion).Future();
        }
    }
}
