using com.etak.core.jms.listener;
using com.etak.core.jms.nmstracing;
using com.etak.eventsystem.extension.test;
using com.etak.eventsystem.jmsReceiver;
using com.etak.eventsystem.model;
using com.etak.eventsystem.wcfSender;
using log4net;
using System;
using System.Configuration;
using System.Reflection;

namespace ProducerConsumerSample
{
    public class Global : System.Web.HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static JMSMessageListener<JMSEventMessageAdapter<ConsumerSampleApplication>> _eventSystemReceiver;

        protected void Application_End(object sender, EventArgs e)
        {
            //When the application ends, we need to shut down the listener.
            if (_eventSystemReceiver != null)
            {
                try
                {
                    _eventSystemReceiver.Stop();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error stopping the receiver", ex);
                    throw;
                }
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                //load Log4Net config from web.config
                log4net.Config.XmlConfigurator.Configure();
                ExtensionManager.AddTypesInAssembly(typeof(SuperEventTest).Assembly);

                #region Receiver/Consumer initialization
                //Enable the tracing of NMS with the log4net adapter
                TracerEnabler.EnableTracerAdapter();

                //Read the queue configuration from the app|web.config to provide to the Consumer
                JMSConnectionConfiguration config = (JMSConnectionConfiguration)ConfigurationManager.GetSection("JMSConnectionConfiguration");

                //Create a JMS listener with the processor specified in the Generic argument!
                _eventSystemReceiver = new JMSMessageListener<JMSEventMessageAdapter<ConsumerSampleApplication>>(config);

                //Start the receiver, it will invoke the process method of the class provided as processor: ConsumerSampleApplication
                _eventSystemReceiver.Start();
                #endregion

                #region Sender/Producer initialization
                //Initializates the factory that will be used by the queue to deliver the events.
                WCFClientFactory cli = new WCFClientFactory("http://192.168.25.18:8280/services/EventDispatcher");
                QueuedEventSender.Initialize(cli);
                #endregion
            }
            catch (Exception ex)
            {
                Logger.Error("Error initializing the application", ex);
                throw;
            }
        }
        

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

       
    }
}