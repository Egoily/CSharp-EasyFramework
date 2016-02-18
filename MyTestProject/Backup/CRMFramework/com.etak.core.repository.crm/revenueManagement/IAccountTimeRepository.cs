using System;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Interface for repository of entity AccountTime
    /// </summary>
    /// <typeparam name="TAccountTime">The type of the managed entity is or extends AccountTime</typeparam>
    public interface IAccountTimeRepository<TAccountTime> : IRepository<TAccountTime, Int64> where TAccountTime : AccountTime
    {
    }
}
