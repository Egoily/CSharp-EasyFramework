using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.customer
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TCustomerInfo"/> entity
    /// </summary>
    /// <typeparam name="TCustomerInfo">The type of the entity managed is or extends CustomerInfo</typeparam>
    public interface ICustomerInfoRepository<TCustomerInfo> : IRepository<TCustomerInfo, Int32> where TCustomerInfo : CustomerInfo
    {
        /// <summary>
        /// Gets the csutomer with the associations pre loaded from the db
        /// </summary>
        /// <param name="customerId">the Id of the customer to test</param>
        /// <returns>an enumerable with 0 or 1 customers</returns>
        IEnumerable<TCustomerInfo> LoadCustomerAsociations(Int32 customerId);

        /// <summary>
        /// Gets all customers with a given parent
        /// </summary>
        /// <param name="customerId">the paret of the customer</param>
        /// <returns>the list of customers</returns>
        IEnumerable<TCustomerInfo> GetByParentId(Int32 customerId);


        /// <summary>
        /// Load a customer and it's promotions by a given customerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IEnumerable<TCustomerInfo> LoadCustomerAndPromotionsByCustomerId(Int32 customerId);

        /// <summary>
        /// Gets the only customer info with the provided customer id that ara not in a given state
        /// </summary>
        /// <param name="customerId">the Id of the customer to look up</param>
        /// <param name="statusId">the status Id of the customer to exclude</param>
        /// <returns>the 1 or 0 customers in an enumerable</returns>
        IEnumerable<TCustomerInfo> LoadCustomerAllInfoByCustomerIdExcludeStatusID(int customerId, int statusId);

        /// <summary>
        /// Gets all customers with a given external id, and filtering by dealer id
        /// </summary>
        /// <param name="mvnoId">the dealer owner of the configuration</param>
        /// <param name="externalId">the external id of the customer</param>
        /// <returns>all customers with the given external id</returns>
        IEnumerable<TCustomerInfo> GetByDealerIdAndExternalId(Int32 mvnoId, string externalId);

        /// <summary>
        /// Gets all customers with a given external id
        /// </summary>
        /// <param name="externalId">the external id of the customer</param>
        /// <returns>all customers with the given external id</returns>
        IEnumerable<TCustomerInfo> GetByExternalId(string externalId);

    }
}
