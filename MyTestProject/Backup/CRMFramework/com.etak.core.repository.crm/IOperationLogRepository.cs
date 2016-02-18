using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for OperationLog
    /// </summary>
    /// <typeparam name="TOperationLog">The entity managed by the repository, is or extends OperationLog</typeparam>
    public interface IOperationLogRepository<TOperationLog> : IRepository<TOperationLog, Int64> where TOperationLog : OperationLog
    {
        /// <summary>
        /// Gets all the operation logs of a given vmnoId and a reference code
        /// </summary>
        /// <param name="referenceCode">the external id (reference)</param>
        /// <param name="vmnoId">the id of the mvno/fiscal unit </param>
        /// <returns>The list of matching TOperationLog</returns>
        IEnumerable<TOperationLog> GetByOrderCodeAndDealerId(string referenceCode, int vmnoId);

        /// <summary>
        /// Gets all the operation logs of a given vmnoId and a order code
        /// </summary>
        /// <param name="orderCode">order code</param>
        /// <param name="vmnoId">the id of the mvno/fiscal unit </param>
        /// <returns>The list of matching TOperationLog</returns>
        IEnumerable<TOperationLog> GetByOrderCodeColumnAndDealerId(int orderCode, int vmnoId);
    }
}
