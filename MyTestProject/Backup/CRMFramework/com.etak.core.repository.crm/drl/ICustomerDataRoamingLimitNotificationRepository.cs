using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.drl
{
    /// <summary>
    /// Respository for <typeparamref name="TCustomerDataRoamingLimitNotification"/> entity
    /// </summary>
    /// <typeparam name="TCustomerDataRoamingLimitNotification">The type of the managed entity, is or extends CustomerDataRoamingLimitNotification</typeparam>
    public interface ICustomerDataRoamingLimitNotificationRepository<TCustomerDataRoamingLimitNotification> :
        IRepository<TCustomerDataRoamingLimitNotification, Int64>
        where TCustomerDataRoamingLimitNotification : CustomerDataRoamingLimitNotification
    {
        /// <summary>
        /// Gets all TCustomerDataRoamingLimitNotification of a given customer
        /// </summary>
        /// <param name="customerId">the id of the customer associated to the TCustomerDataRoamingLimitNotification</param>
        /// <returns>all TCustomerDataRoamingLimitNotification of a given customer</returns>
        IEnumerable<TCustomerDataRoamingLimitNotification> GetByCustomerID(Int32 customerId);
    }
}
