using System.Reflection;
using com.etak.eventsystem.model;
using com.etak.eventsystem.model.events;
using com.etak.eventsystem.wcfSender;
using log4net;

namespace ProducerConsumerSample
{
    public class ConsumerSampleApplication : EventSystemContract
    {
        static private  readonly  ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public void ProcessEvent(Event ev)
        {
            Log.InfoFormat("Hey! I received an event! of type: {0}", ev.GetType());
           
            ////let's send a new event
            QueuedEventSender.GetInstance().ProcessEvent(new TestEvent( ));
        }
    }
}