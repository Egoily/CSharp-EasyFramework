using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;
using NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CustomerProductAssignment 
    /// </summary>
    /// <typeparam name="TCustomerProductAssignment">Entity managed by the repository, is or extends CustomerProductAssignment</typeparam>
    public class CustomerProductAssignmentRepositoryNH<TCustomerProductAssignment> : 
        NHibernateRepository<TCustomerProductAssignment, Int64>, 
        ICustomerProductAssignmentRepository<TCustomerProductAssignment> where TCustomerProductAssignment : CustomerProductAssignment
    {
        /// <summary>
        /// Gets all customer products assignment
        /// </summary>
        /// <param name="customerId">the Id of the customer to where the charges are assigneed</param>
        /// <returns>All the customer product assignment</returns>
        public IEnumerable<TCustomerProductAssignment> GetByCustomerId(Int32 customerId)
        {
            var ret = GetQuery().Where(ee => ee.PurchasingCustomer.CustomerID == customerId).Future();
            return ret;
        }

        /// <summary>
        /// Gets all customer products assignment with start date greater than input value
        /// and endtime smaller thatn input value
        /// </summary>
        /// <param name="customerId">the id of the customer to retreive the association</param>
        /// <param name="startDate">the start date to compare with startdate of the CustomerProductAssingment</param>
        /// <param name="endDate">the end date to compare with enddate of the CustomerProductAssingment</param>
        /// <returns>the list of matching customer product assingments</returns>
        public IEnumerable<TCustomerProductAssignment> GetByCustomerIdWithRange(Int32 customerId, DateTime startDate, DateTime endDate)
        {
            IQueryOver<TCustomerProductAssignment, TCustomerProductAssignment> rootQuery = GetQuery();
            rootQuery.Where(ee => ee.PurchasingCustomer.CustomerID == customerId && ee.StartDate >= startDate && ee.EndDate <= endDate);
            return (rootQuery.Future());
        }


        /// <summary>
        /// Gets all customer products assignment with start date greater than input value
        /// and endtime smaller thatn input value or end date null
        /// </summary>
        /// <param name="customerId">the id of the customer to retreive the association</param>
        /// <param name="pointInTime">the start date to compare with startdate and enddate of the CustomerProductAssingment</param>
        /// <returns>the list of matching customer product assingments</returns>
        public IEnumerable<TCustomerProductAssignment> GetByCustomerWhereDateRangesInDate(int customerId, DateTime pointInTime)
        {
            return GetQuery().Where( x =>
                        x.PurchasingCustomer.CustomerID == customerId && x.StartDate < pointInTime &&
                        (x.EndDate == null || x.EndDate > pointInTime))
                .Future();
        }
    }
}
