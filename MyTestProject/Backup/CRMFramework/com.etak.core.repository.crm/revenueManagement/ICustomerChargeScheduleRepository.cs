using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Repository Interface for CustomerChargeSchedule
    /// </summary>
    /// <typeparam name="TCustomerChargeSchedule">A Class that is CustomerChargeSchedule or extends it</typeparam>
    public interface ICustomerChargeScheduleRepository<TCustomerChargeSchedule> : IRepository<TCustomerChargeSchedule, Int64> where TCustomerChargeSchedule : CustomerChargeSchedule
    {
        /// <summary>
        /// Gets all the scheduled charges of a given customer
        /// </summary>
        /// <param name="customer">the customer to the charges are associated</param>
        /// <returns>A list of Scheduled charges.</returns>
        IEnumerable<TCustomerChargeSchedule> GetChargeSchedulesByCustomer(CustomerInfo customer);
    }
}
