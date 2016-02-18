using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ProducerConsumerSample
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WsdlGeneratorHelper" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WsdlGeneratorHelper.svc or WsdlGeneratorHelper.svc.cs at the Solution Explorer and start debugging.
    public class WsdlGeneratorHelper : com.etak.eventsystem.model.EventSystemContract
    {
        public void ProcessEvent(com.etak.eventsystem.model.events.Event ev)
        {
            throw new NotImplementedException();
        }
    }
}
