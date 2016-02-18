using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// The respository interface for <typeparamref name="TCustomerProductAssignment"/> entity
    /// </summary>
    /// <typeparam name="TCustomerProductAssignment">The entity managed by the interface, is or extends CustomerProductAssignment</typeparam>
    public interface ICustomerProductAssignmentRepository<TCustomerProductAssignment> : IRepository<TCustomerProductAssignment, Int64> where TCustomerProductAssignment : CustomerProductAssignment
    {
        /// <summary>
        /// Gets all customer products assignment
        /// </summary>
        /// <param name="customerId">the Id of the customer to where the charges are assigneed</param>
        /// <returns>All the customer product assignment</returns>
        IEnumerable<TCustomerProductAssignment> GetByCustomerId(Int32 customerId);

        /// <summary>
        /// Gets all customer products assignment with start date greater than input value
        /// and endtime smaller thatn input value
        /// </summary>
        /// <param name="customerId">the id of the customer to retreive the association</param>
        /// <param name="startDate">the start date to compare with startdate of the CustomerProductAssingment</param>
        /// <param name="endDate">the end date to compare with enddate of the CustomerProductAssingment</param>
        /// <returns>the list of matching customer product assingments</returns>
        IEnumerable<TCustomerProductAssignment> GetByCustomerIdWithRange(Int32 customerId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets all customer products assignment with start date greater than input value
        /// and endtime smaller thatn input value or end date null
        /// </summary>
        /// <param name="customerId">the id of the customer to retreive the association</param>
        /// <param name="pointInTime">the start date to compare with startdate and enddate of the CustomerProductAssingment</param>
        /// <returns>the list of matching customer product assingments</returns>
        IEnumerable<TCustomerProductAssignment> GetByCustomerWhereDateRangesInDate(Int32 customerId, DateTime pointInTime);

        
    }
}
