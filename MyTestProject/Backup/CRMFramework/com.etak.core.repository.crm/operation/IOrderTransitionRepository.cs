using System;
using com.etak.core.model.operation;

namespace com.etak.core.repository.crm.operation
{
    /// <summary>
    /// Repository interface for OrderTransition
    /// </summary>
    /// <typeparam name="TOrderTransition">The type of the entity managed by the repository, is or extends OrderTransition</typeparam>
    public interface IOrderTransitionRepository<TOrderTransition> : IRepository<TOrderTransition, Int64>
        where TOrderTransition : OrderTransition
    {
    }
}
