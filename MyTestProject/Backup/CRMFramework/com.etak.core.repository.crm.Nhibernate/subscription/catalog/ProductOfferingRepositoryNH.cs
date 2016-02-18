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
    /// Nhibernate repository for inheritance tree of entity ProductOfferingRepository 
    /// </summary>
    /// <typeparam name="TProductOffering">Entity managed by the repository, is or extends ProductOffering</typeparam>
    public class ProductOfferingRepositoryNH<TProductOffering> :
        NHibernateRepository<TProductOffering, Int32>,
        IProductOfferingRepository<TProductOffering> where TProductOffering : ProductOffering
    {
        /// <summary>
        /// Given a groupId, return all the ProductOfferings that belongs to this group
        /// </summary>
        /// <param name="groupId">The group to be used to get the product offerings</param>
        /// <returns>A list of ProductOfferings that belongs to the group specified</returns>
        public IEnumerable<TProductOffering> GetByGroupId(Int32 groupId)
        {
            var query = GetQuery().Where(x => x.Group.Id == groupId).Future();
            return query;
        }
    }
}
