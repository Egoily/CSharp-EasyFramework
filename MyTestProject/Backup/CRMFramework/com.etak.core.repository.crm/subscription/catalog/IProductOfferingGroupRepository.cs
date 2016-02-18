using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.repository.crm.subscription.catalog
{
    /// <summary>
    /// Interface for repository of entity ProductOfferingGroup
    /// </summary>
    /// <typeparam name="TProductOfferingGroup">The type of the managed entity is or extends ProductOfferingGroup</typeparam>
    public interface IProductOfferingGroupRepository<TProductOfferingGroup> : IRepository<TProductOfferingGroup, Int32> where TProductOfferingGroup : ProductOfferingGroup
    {

    }
}
