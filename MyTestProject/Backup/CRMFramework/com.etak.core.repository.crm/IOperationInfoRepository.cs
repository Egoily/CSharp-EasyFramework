using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TOperationInfo"/> entity
    /// </summary>
    /// <typeparam name="TOperationInfo">The type of the entity managed is or extends BalanceForAccount</typeparam>
    public interface IOperationInfoRepository<TOperationInfo> : IRepository<TOperationInfo, Int64> where TOperationInfo : OperationInfo
    {
        
    }
}
