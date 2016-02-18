using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The respository interface for <typeparamref name="TEventInfo"/> entity
    /// </summary>
    /// <typeparam name="TEventInfo">The entity managed by the interface, is or extends EventInfo</typeparam>
    public interface IEventInfoRepository<TEventInfo> : IRepository<TEventInfo, Int32> where TEventInfo : EventInfo
    {
        /// <summary>
        /// Gets all the events of a given type of code and type
        /// </summary>
        /// <param name="eventCode">the eventCode to look for</param>
        /// <param name="eventType">the event type to look for</param>
        /// <returns>a list with all the events mathching envet code and event type</returns>
        IEnumerable<TEventInfo> GetByEventCodeAndEventType(int eventCode, int eventType);
    }
}
