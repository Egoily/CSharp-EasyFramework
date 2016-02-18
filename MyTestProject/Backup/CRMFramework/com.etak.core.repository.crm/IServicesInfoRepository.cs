using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The respository interface for <typeparamref name="TServicesInfo"/> entity
    /// </summary>
    /// <typeparam name="TServicesInfo">The entity managed by the interface, is or extends ServicesInfo</typeparam>
    public interface IServicesInfoRepository<TServicesInfo> : IRepository<TServicesInfo, Int32> where TServicesInfo : ServicesInfo
    {
        /// <summary>
        /// Gets all the services for a given customer
        /// </summary>
        /// <param name="customerId">The id of the customer from which the services needs to be retreived</param>
        /// <returns>the list of matching services</returns>
        IEnumerable<TServicesInfo> GetByCustomerId(int customerId);

        /// <summary>
        /// Gets the Service where the main balance is stored.
        /// </summary>
        /// <param name="customerId">the customer where the</param>
        /// <returns>A list with 0 or 1 TServicesInfo holding the main balance</returns>
        IEnumerable<TServicesInfo> GetCustomerMainService(int customerId);
    }
}
