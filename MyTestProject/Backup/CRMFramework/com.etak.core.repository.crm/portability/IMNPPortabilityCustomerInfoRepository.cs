using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.portability
{
    /// <summary>
    /// Respository for <typeparamref name="TMNPPortabilityCustomerInfo"/> entity
    /// </summary>
    /// <typeparam name="TMNPPortabilityCustomerInfo">The type of the managed entity, is or extends MNPPortabilityCustomerInfo</typeparam>
    public interface IMNPPortabilityCustomerInfoRepository<TMNPPortabilityCustomerInfo> : IRepository<TMNPPortabilityCustomerInfo, Int32>
        where TMNPPortabilityCustomerInfo : MNPPortabilityCustomerInfo
    {
        /// <summary>
        /// Gets all the MNPPortabilityCustomerInfo of a given customer
        /// </summary>
        /// <param name="customerId">The id of the customer to which the MNPPortabilityCustomerInfo is associated</param>
        /// <returns>The list of MNPPortabilityCustomerInfo of the customer</returns>
        IEnumerable<TMNPPortabilityCustomerInfo> GetByCustomerId(Int32 customerId);
    }
}
