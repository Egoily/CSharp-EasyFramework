using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CustomerAccountAssociation 
    /// </summary>
    /// <typeparam name="TCustomerAccountAssociation">Entity managed by the repository, is or extends CustomerAccountAssociation</typeparam>
    public class CustomerAccountAssociationRepositoryNH<TCustomerAccountAssociation> : 
        NHibernateRepository<TCustomerAccountAssociation, Int32>, 
        ICustomerAccountAssociationRepository<TCustomerAccountAssociation> where TCustomerAccountAssociation : CustomerAccountAssociation
    {
        /// <summary>
        /// Gets all the customer accounts associations of a given customer
        /// </summary>
        /// <param name="customer">The customer in the association</param>
        /// <returns>the list of customer account associations of the customer</returns>
        public IEnumerable<TCustomerAccountAssociation> AllAsociationsForCustomer(CustomerInfo customer)
        {
            var ret = GetQuery().Where(x => x.Customer == customer).Future();
            return ret;
        }
    }
}
