using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class LoadeableEntity
    {
        [DataMember()]
        public virtual Boolean IsLoaded {get;set;}

        public LoadeableEntity() 
        {
            IsLoaded = true;
        }
    }
}
