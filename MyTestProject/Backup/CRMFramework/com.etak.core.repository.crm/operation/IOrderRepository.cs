using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation;
using System.Linq.Expressions;

namespace com.etak.core.repository.crm.operation
{
    /// <summary>
    /// Repository for Order entity
    /// </summary>
    /// <typeparam name="TOrder">The type that is or extends <see cref="Order"/></typeparam>
    public interface IOrderRepository<TOrder> : IRepository<TOrder, Int64> where TOrder : Order
    {
        /// <summary>
        /// Gets the list of order of a given dealer with externalId
        /// </summary>
        /// <param name="dealerInfo">the dealer of the Order</param>
        /// <param name="externalId">the external id of the order</param>
        /// <returns>The list of matchin orders</returns>
        IEnumerable<TOrder> GetByExternalIdAndDealer(DealerInfo dealerInfo, string externalId);
        /// <summary>
        /// Gets the list of order of a given dealer and filters
        /// </summary>
        /// <param name="dealerInfo">the dealer of the Order</param>
        /// <param name="filters">filters to apply</param>
        /// <param name="pageNumber">page Number  start with 1</param>
        /// <param name="pageSize">page Size</param>
        /// <returns>The list of matchin orders</returns>
        IEnumerable<TOrder> GetByDealerAndFilters(DealerInfo dealerInfo, List<Expression<Func<TOrder, bool>>> filters, int pageNumber, int pageSize);
    }

   
}
