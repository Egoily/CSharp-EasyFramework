using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Implementation of ICustomerChargeScheduleRepository based on Nhibernate repository
    /// </summary>
    /// <typeparam name="TCustomerChargeSchedule">A Class that is CustomerChargeSchedule or extends it</typeparam>
    public class CustomerChargeScheduleRepositoryNH<TCustomerChargeSchedule> : NHibernateRepository<TCustomerChargeSchedule, Int64>, ICustomerChargeScheduleRepository<TCustomerChargeSchedule> where TCustomerChargeSchedule : CustomerChargeSchedule
    {
        /// <summary>
        /// Gets all the scheduled charges of a given customer
        /// </summary>
        /// <param name="customer">the customer to the charges are associated</param>
        /// <returns>A list of Scheduled charges.</returns>
        public IEnumerable<TCustomerChargeSchedule> GetChargeSchedulesByCustomer(CustomerInfo customer)
        {
            return GetQuery().Where(x => x.Customer == customer).Future();
        }
    }
}
