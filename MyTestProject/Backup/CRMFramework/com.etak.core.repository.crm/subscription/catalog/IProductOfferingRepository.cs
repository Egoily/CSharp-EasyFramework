using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.repository.crm.subscription.catalog
{
    /// <summary>
    /// Interface for repository of entity ProductOffering
    /// </summary>
    /// <typeparam name="TProductOffering">The type of the managed entity is or extends ProductOffering</typeparam>
    public interface IProductOfferingRepository<TProductOffering> : IRepository<TProductOffering, Int32> where TProductOffering : ProductOffering
    {
        /// <summary>
        /// Given a groupId, return all the ProductOfferings that belongs to this group
        /// </summary>
        /// <param name="groupId">The group to be used to get the product offerings</param>
        /// <returns>A list of ProductOfferings that belongs to the group specified</returns>
        IEnumerable<TProductOffering> GetByGroupId(Int32 groupId);
    }
}
