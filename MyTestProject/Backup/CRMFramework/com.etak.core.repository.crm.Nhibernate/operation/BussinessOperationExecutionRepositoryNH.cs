using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.repository.crm.operation;
using com.etak.core.repository.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace com.etak.core.repository.crm.Nhibernate.operation
{
    /// <summary>
    /// Repository based on NHibernate for  BusinessOperationExecution entity inheritance tree
    /// </summary>
    /// <typeparam name="TBusinessOperationExecution">The type of the managed entity, is or inherits BusinessOperationExecution</typeparam>
    public class BusinessOperationExecutionRepositoryNH<TBusinessOperationExecution> : 
            NHibernateRepository<TBusinessOperationExecution, Int64>
        , IBusinessOperationExecutionRepository<TBusinessOperationExecution> where TBusinessOperationExecution : BusinessOperationExecution
    {
        /// <summary>
        /// Gets all the operation executions in a given range of dates in a list of status
        /// </summary>
        /// <param name="startDate">The start date for the range where the operation started</param>
        /// <param name="endDate">The end date for the range where the operation started</param>
        /// <param name="results">the type of result of the operation</param>
        /// <returns>The list of matching operation executions</returns>
        public IEnumerable<TBusinessOperationExecution> GetOperationsWithinDatesWithResultTypesIn(DateTime startDate, DateTime endDate, IEnumerable<ResultTypes> results)
        {
            return GetQuery()
                    .Where(x => x.StartTime > startDate && x.StartTime < endDate  && x.ResultType.IsIn(results.ToArray()))
                    .Future();

        }


        /// <summary>
        /// Gets all the operation executions in a given range of dates in a list of status and a given type
        /// of business operation
        /// </summary>
        /// <param name="bizOp">the businessOperation definition to query</param>
        /// <param name="startDate">The start date for the range where the operation started</param>
        /// <param name="endDate">The end date for the range where the operation started</param>
        /// <param name="results">the type of result of the operation</param>
        /// <returns>The list of matching operation executions</returns>
        public IEnumerable<TBusinessOperationExecution> GetOperationsOfTypeWithinDatesWithResultTypesIn(BusinessOperation bizOp, DateTime startDate, DateTime endDate, IEnumerable<ResultTypes> results)
        {
            return GetQuery()
                   .Where(x => x.StartTime > startDate && x.StartTime < endDate && x.OperationDefintition == bizOp && x.ResultType.IsIn(results.ToArray()))
                   .Future();
        }


        /// <summary>
        /// Gets the Operation of customer optionally filtering by the fields provided
        /// </summary>
        /// <param name="customer">The customer to retrieve the operations from</param>
        /// <param name="bizOp">The Discriminator of the bussiness Operation to retrieve</param>
        /// <param name="startDate">Start date for range to retreive the operations</param>
        /// <param name="endDate">End date for the range to retrieve the operations</param>
        /// <param name="resultTypes">The status in which the operations needs to be</param>
        /// <returns>The list of matching operation executions</returns>
        public IEnumerable<TBusinessOperationExecution> GetCustomerOperationsBetweenDates(CustomerInfo customer, BusinessOperation bizOp, DateTime startDate, DateTime endDate, ResultTypes[] resultTypes)
        {
            IQueryOver<TBusinessOperationExecution, TBusinessOperationExecution> q = GetQuery().
                Where(x => (x.Customer == customer || x.CustomerDestination == customer) && 
                        x.StartTime > startDate && x.StartTime < endDate &&
                        x.OperationDefintition == bizOp &&
                        x.ResultType.IsIn(resultTypes.ToArray()));

            return q.Future();
        }
    }
}
