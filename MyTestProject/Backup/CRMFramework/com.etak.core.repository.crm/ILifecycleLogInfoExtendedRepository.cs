using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for LifecycleLogInfoExtended
    /// </summary>
    /// <typeparam name="TLifecycleLogInfoExtended">The type of the entity managed by the repository, is or extends LifecycleLogInfoExtended</typeparam>
    public interface ILifecycleLogInfoExtendedRepository<TLifecycleLogInfoExtended> 
        : IRepository<TLifecycleLogInfoExtended, Int64> 
        where TLifecycleLogInfoExtended : LifecycleLogInfoExtended
    {

    }
}
