using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// The respository interface for <typeparamref name="TCustomerAccountAssociation"/> entity
    /// </summary>
    /// <typeparam name="TCustomerAccountAssociation">The entity managed by the interface, is or extends CustomerAccountAssociation</typeparam>
    public interface ICustomerAccountAssociationRepository<TCustomerAccountAssociation> : IRepository<TCustomerAccountAssociation, Int32> where TCustomerAccountAssociation : CustomerAccountAssociation
    {
        /// <summary>
        /// Gets all the customer accounts associations of a given customer
        /// </summary>
        /// <param name="customer">The customer in the association</param>
        /// <returns>the list of customer account associations of the customer</returns>
        IEnumerable<TCustomerAccountAssociation> AllAsociationsForCustomer(CustomerInfo customer);        
    }
}
