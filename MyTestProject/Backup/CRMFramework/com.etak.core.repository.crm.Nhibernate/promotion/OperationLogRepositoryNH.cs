using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for OperationLog entity inheritance tree
    /// </summary>
    /// <typeparam name="TOperationLog">the type of entity managed, is or extends OperationLog</typeparam>
    public class OperationLogRepositoryNH<TOperationLog>
        : NHibernateRepository<TOperationLog, Int64>,
       IOperationLogRepository<TOperationLog> where TOperationLog : OperationLog
    {
        /// <summary>
        /// Gets all the operation logs of a given vmnoId and a reference code
        /// </summary>
        /// <param name="referenceCode">the external id (reference)</param>
        /// <param name="vmnoId">the id of the mvno/fiscal unit </param>
        /// <returns>The list of matching TOperationLog</returns>
        public IEnumerable<TOperationLog> GetByOrderCodeAndDealerId(string referenceCode, int vmnoId)
        {
            return (GetQuery().
                    Where(x => x.ExternalCode == referenceCode && x.DealerID == vmnoId)
                    .Future()   
                   );
        }

        /// <summary>
        /// Gets all the operation logs of a given vmnoId and a order code
        /// </summary>
        /// <param name="orderCode">order code</param>
        /// <param name="vmnoId">the id of the mvno/fiscal unit </param>
        /// <returns>The list of matching TOperationLog</returns>
        public IEnumerable<TOperationLog> GetByOrderCodeColumnAndDealerId(int orderCode, int vmnoId)
        {
            return (GetQuery().
                    Where(x => x.OrderCode == orderCode && x.DealerID == vmnoId)
                    .Future());
        }
    }
}
