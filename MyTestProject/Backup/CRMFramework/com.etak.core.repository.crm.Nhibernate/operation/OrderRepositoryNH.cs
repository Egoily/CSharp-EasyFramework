using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.repository.crm.operation;
using com.etak.core.repository.NHibernate;
using System.Linq.Expressions;

namespace com.etak.core.repository.crm.Nhibernate.operation
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity Order 
    /// </summary>
    /// <typeparam name="TOrder">Entity managed by the repository, is or extends Order</typeparam>
    public class OrderRepositoryNH<TOrder> : NHibernateRepository<TOrder, Int64>
       , IOrderRepository<TOrder>
       where TOrder : Order
    {
        /// <summary>
        /// Gets the list of orders of the given dealer with external reference
        /// </summary>
        /// <param name="dealerInfo">the dealer (mvno) that did the order</param>
        /// <param name="externalId">the external reference to look up</param>
        /// <returns>the list of matching items</returns>
        public IEnumerable<TOrder> GetByExternalIdAndDealer(DealerInfo dealerInfo, string externalId)
        {
            return GetQuery().Where(x => x.ExternalId == externalId && x.Dealer == dealerInfo).Future();
        }

        /// <summary>
        /// Gets the list of orders of the given dealer and filters
        /// </summary>
        /// <param name="dealerInfo">the dealer (mvno) that did the order</param>
        /// <param name="filters">filters to apply</param>
        /// <returns>the list of matching items</returns>
        /// <param name="pageNumber">page Number start with 1</param>
        /// <param name="pageSize">page Size</param>
        public IEnumerable<TOrder> GetByDealerAndFilters(DealerInfo dealerInfo, List<Expression<Func<TOrder, bool>>> filters, int pageNumber, int pageSize)
        {
            var query = GetQuery().Where(x => x.Dealer == dealerInfo);
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }
            return query.Skip((pageNumber-1)*pageSize).Take(pageSize).Future();
        }
    }
}
