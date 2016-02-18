using System;
using com.etak.core.model.operation;

namespace com.etak.core.repository.crm.operation
{
    /// <summary>
    /// Repository interface for BusinessOperationExecution
    /// </summary>
    /// <typeparam name="TBusinessOperation">The type of the entity managed by the repository, is or extends BusinessOperation</typeparam>
    public interface IBusinessOperationRepository<TBusinessOperation> 
        where TBusinessOperation : BusinessOperation
    {
        /// <summary>
        /// Gets the BusinessOperation by it's id, only for internal use
        /// </summary>
        /// <returns>The TBusinessOpertaion</returns>
        TBusinessOperation GetById(Int32 id);


        /// <summary>
        /// Gets the only possible instance at catalog level of the given TBusinessOperation,       
        /// </summary>
        /// <returns>the catalog level of the operation</returns>
        TBusinessOperation Get();

        /// <summary>
        /// Inserts an entity into the repository
        /// </summary>
        /// <param name="entity">the entity to be stored</param>
        /// <returns>the updated entity</returns>
        TBusinessOperation Create(TBusinessOperation entity);

    }
}
