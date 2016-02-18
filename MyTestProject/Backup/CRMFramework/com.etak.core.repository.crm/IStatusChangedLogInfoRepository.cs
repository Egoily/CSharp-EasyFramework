using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Interface for repository of entity StatusChangedLogInfo
    /// </summary>
    /// <typeparam name="TStatusChangedLogInfo">The type of the managed entity is or extends StatusChangedLogInfo</typeparam>
    public interface IStatusChangedLogInfoRepository<TStatusChangedLogInfo> : IRepository<TStatusChangedLogInfo, Int64> 
        where TStatusChangedLogInfo : StatusChangedLogInfo
    {
    }
}
