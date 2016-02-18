using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.drl
{
    /// <summary>
    /// The repository interface for CustomerDataRoamingLimit
    /// </summary>
    /// <typeparam name="TCustomerDataRoamingLimit">The type of the entity managed CustomerDataRoamingLimit</typeparam>
    public interface ICustomerDataRoamingLimitRepository<TCustomerDataRoamingLimit> : IRepository<TCustomerDataRoamingLimit, Int64>
        where TCustomerDataRoamingLimit : CustomerDataRoamingLimit
    {
        /// <summary>
        /// Gets all TCustomerDataRoamingLimit of a given customer
        /// </summary>
        /// <param name="customerId">the id of the customer associated to the TCustomerDataRoamingLimit</param>
        /// <returns>all TCustomerDataRoamingLimit of a given customer</returns>
        IEnumerable<TCustomerDataRoamingLimit> GetByCustomerID(Int32 customerId);
    }
}
