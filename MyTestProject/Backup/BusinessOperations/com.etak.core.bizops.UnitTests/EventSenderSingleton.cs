using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.etak.eventsystem.wcfSender;

namespace com.etak.core.bizops.UnitTests
{
    public static class EventSenderSingleton
    {

        static EventSenderSingleton()
        {
            var ClientFactory = new WCFClientFactory("");
            QueuedEventSender.Initialize(ClientFactory);
        }

        public static void Init()
        {
            //Dummy Init to initialize
        }
    }
}
