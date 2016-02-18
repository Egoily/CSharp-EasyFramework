using com.etak.eventsystem.model.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace com.etak.eventsystem.model
{
    public class ExtensionManager
    {
        static readonly public Type typeOfEvent = typeof(Event);
        static readonly public List<Type> knownEventTypes = new List<Type>();

        static ExtensionManager()
        {
            knownEventTypes.AddRange(typeof(ExtensionManager).Assembly.GetTypes().Where(x =>  typeOfEvent.IsAssignableFrom(x)));            
        }

        public static void Register<TEvent>() where TEvent : Event
        {
            knownEventTypes.Add(typeof(TEvent));
        }

        public static void AddTypesInAssembly(Assembly assm)
        {
            knownEventTypes.AddRange(assm.GetTypes().Where(x => typeOfEvent.IsAssignableFrom(x)));
        }

        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        {
            return knownEventTypes;
        }
    }
}
