using com.etak.eventsystem.model.dto;
using com.etak.eventsystem.model.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.es.eventsystem
{
    [DataContract(Namespace = "http://com.etak.es.eventsystem")]
    public class ChangeTo3GProductEvent : Event
    {
        [DataMember()]
        public Customer Customer { get; set; }
        [DataMember()]
        public IList<Product> NewProducts { get; set; }
    }
}
