using System;
using System.Collections.Generic;
using System.Globalization;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for EventInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TEventInfo">the type of entity managed, is or extends EventInfo</typeparam>
    public class EventInfoRepositoryNH<TEventInfo>
        : NHibernateRepository<TEventInfo, Int32>,
       IEventInfoRepository<TEventInfo> where TEventInfo : EventInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all the events of a given type of code and type
        /// </summary>
        /// <param name="eventCode">the eventCode to look for</param>
        /// <param name="eventType">the event type to look for</param>
        /// <returns>a list with all the events mathching envet code and event type</returns>
        public IEnumerable<TEventInfo> GetByEventCodeAndEventType(int eventCode, int eventType)
        {
            return GetQuery()
                .Where(ee => ee.EventCode == (eventCode).ToString(CultureInfo.InvariantCulture) && ee.EventType == eventType)
                .Cacheable().CacheRegion(CacheRegion)
                .Future();
        }



    }
}
