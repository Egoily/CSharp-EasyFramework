using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;
using NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Implementation of ICustomerChargeRepository based on Nhibernate
    /// </summary>
    /// <typeparam name="T">Any type extending CustomerCharge</typeparam>
    public class CustomerChargeRepositoryNH<T> : NHibernateRepository<T, Int64>, ICustomerChargeRepository<T> where T : CustomerCharge
    {
        /// <summary>
        /// Gets the customer charges by customerId in a future way
        /// </summary>
        /// <param name="customerId">The customer id to load</param>
        /// <returns>a list containing the charges for the customercustomers charge</returns>
        public IEnumerable<T> GetByCustomerId(Int32 customerId)
        {
            var query = GetQuery().Where(x => x.Customer.CustomerID == customerId).Future();         
            return (query);
        }

        /// <summary>
        /// Gets the customer charges by customerId in a future way with charging dates within a range
        /// </summary>
        /// <param name="customerId">The customer id to load</param>
        /// <param name="startDate">The purchase date range start</param>
        /// <param name="endDate">The purchase date range end</param>
        /// <returns>a list containing the charges for the customercustomers charge</returns>
        public IEnumerable<T> GetByCustomerIdWithRange(Int32 customerId, DateTime startDate, DateTime endDate)
        {
            IQueryOver<T, T> rootQuery = GetQuery();
            rootQuery.Where(x => x.Customer.CustomerID == customerId && x.ChargingDate >= startDate && x.ChargingDate <= endDate);
            return (rootQuery.Future());

        }

        /// <summary>
        /// Gets the customer charges by customerId of a given invoice
        /// </summary>
        /// <param name="customerId">THe id of the customer owning the charges</param>
        /// <param name="invoice">The invoice to look for</param>
        /// <returns>a list containing the charges for the customer charge</returns>
        public IEnumerable<T> GetByCustomerIdAndInvoice(int customerId, Invoice invoice)
        {
            return (GetQuery().Where(x => x.Customer.CustomerID == customerId && x.Invoice == invoice).Future());
        }
    }
}
