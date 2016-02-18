using System;
using System.Collections.Generic;
using System.Threading;
using com.etak.core.queue.Client;
using com.etak.core.queue.Common;
using com.etak.eventsystem.model;
using com.etak.eventsystem.model.events;
using log4net;

namespace com.etak.eventsystem.wcfSender
{
    class DequeueEventProcessorAdapter : AbstractXMLBackUpDequeuer<Event>, IDequeuer<Event>
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IEventContractImplementorFactory SenderFactory;
        private EventSystemContract Sender;

        public DequeueEventProcessorAdapter(IEventContractImplementorFactory SenderFactory)
        {
            this.SenderFactory = SenderFactory;
        }

        public override void StartUp()
        {
            base.StartUp();   
        }

        public override IList<QueueElementInformation<Event>> Process(IList<QueueElementInformation<Event>> CurrentBlock)
        {
            if (CurrentBlock == null || CurrentBlock.Count == 0)
            {
                throw new NotImplementedException();
            }

            IList<QueueElementInformation<Event>> responseList = new List<QueueElementInformation<Event>>();
            foreach (QueueElementInformation<Event> element in CurrentBlock)
            {                
                try
                {
                    if (Sender == null)
                    {
                        Sender = SenderFactory.GetImplementation();
                    }
                    this.Sender.ProcessEvent(element.Element);
                }
                catch (Exception ex)
                {
                    log.Error("Unable to send event, sending back as failed", ex);
                    try
                    {
                        SenderFactory.Destroy(Sender);
                    }
                    catch (Exception)
                    {
                        
                    }
                    Sender = null;           
                    responseList.Add(element);
                    Thread.Sleep(500);
                }
            }
            return (responseList);
        }
    }
}
