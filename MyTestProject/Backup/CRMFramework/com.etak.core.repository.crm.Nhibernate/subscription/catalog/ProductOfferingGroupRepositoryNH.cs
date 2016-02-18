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
    /// Nhibernate repository for inheritance tree of entity ProductOfferingGroup 
    /// </summary>
    /// <typeparam name="TProductOfferingGroup">Entity managed by the repository, is or extends ProductOfferingGroup</typeparam>
    public class ProductOfferingGroupRepositoryNH<TProductOfferingGroup> :
        NHibernateRepository<TProductOfferingGroup, Int32>,
        IProductOfferingGroupRepository<TProductOfferingGroup> where TProductOfferingGroup : ProductOfferingGroup
    {
        
    }
}
