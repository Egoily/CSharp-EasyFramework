using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TProductChargeOption"/> entity
    /// </summary>
    /// <typeparam name="TProductChargeOption">The type of the entity managed is or extends ProductChargeOption</typeparam>
    public interface IProductChargeOptionRepository<TProductChargeOption> : IRepository<TProductChargeOption, Int32> where TProductChargeOption : ProductChargeOption
    {
        /// <summary>
        /// Gets all the ProductChargeOption of a given product offering (This method in general, Product Offering already has the result of this query and cached)
        /// <see cref="com.etak.core.model.subscription.catalog.ProductOffering.ChargingOptions"/>
        /// </summary>
        /// <param name="productOfferingId">The id of the product to get the TProductChargeOption</param>
        /// <returns>the list of the TProductChargeOption of the product offering</returns>
        IEnumerable<TProductChargeOption> GetByProductOfferingId(Int32 productOfferingId);
    }
}
