using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;

namespace com.etak.core.repository.crm.operation
{
    /// <summary>
    /// Repository interface for BusinessOperationExecution
    /// </summary>
    /// <typeparam name="TBusinessOperationExecution">The type of the entity managed by the repository, is or extends BusinessOperationExecution</typeparam>
    public interface IBusinessOperationExecutionRepository<TBusinessOperationExecution> :
        IRepository<TBusinessOperationExecution, Int64>
        where TBusinessOperationExecution : BusinessOperationExecution
    {
        /// <summary>
        /// Gets all the operation executions in a given range of dates in a list of status
        /// </summary>
        /// <param name="startDate">The start date for the range where the operation started</param>
        /// <param name="endDate">The end date for the range where the operation started</param>
        /// <param name="results">the type of result of the operation</param>
        /// <returns>The list of matching operation executions</returns>
        IEnumerable<TBusinessOperationExecution> GetOperationsWithinDatesWithResultTypesIn(DateTime startDate, DateTime endDate, IEnumerable<ResultTypes> results);


        /// <summary>
        /// Gets all the operation executions in a given range of dates in a list of status and a given type
        /// of business operation
        /// </summary>
        /// <param name="bizOp">the businessOperation definition to look for</param>
        /// <param name="startDate">The start date for the range where the operation started</param>
        /// <param name="endDate">The end date for the range where the operation started</param>
        /// <param name="results">the type of result of the operation</param>
        /// <returns>The list of matching operation executions</returns>
        IEnumerable<TBusinessOperationExecution> GetOperationsOfTypeWithinDatesWithResultTypesIn(BusinessOperation bizOp, DateTime startDate, DateTime endDate, IEnumerable<ResultTypes> results);

        /// <summary>
        /// Gets the Operation of customer optionally filtering by the fields provided
        /// </summary>
        /// <param name="customer">The customer to retrieve the operations from</param>
        /// <param name="bizOp">The Operation Definition</param>
        /// <param name="startDate">Start date for range to retreive the operations</param>
        /// <param name="endDate">End date for the range to retrieve the operations</param>
        /// <param name="resultTypes">The status in which the operations needs to be</param>
        /// <returns>The list of matching operation executions</returns>
        IEnumerable<TBusinessOperationExecution> GetCustomerOperationsBetweenDates(CustomerInfo customer, BusinessOperation bizOp, DateTime startDate, DateTime endDate, ResultTypes[] resultTypes);
    }
}
