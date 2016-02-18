using com.etak.eventsystem.model;
using com.etak.eventsystem.model.events;
using com.etak.eventsystem.model.dto;


namespace com.etak.eventsystem.wcfSender
{
    [System.ServiceModel.ServiceContractAttribute(Namespace = Definitions.EV_SYSTEM_NAMESPACE, ConfigurationName = "EventSystemConnector.senders.EventSystemChannelContract")]
    public interface EventSystemContractChannel : EventSystemContract, System.ServiceModel.IClientChannel
    {

    }

    /// <summary>
    /// A WCF sender implementing th interface of the event system
    /// </summary>
    public class WCFSender : System.ServiceModel.ClientBase<EventSystemContract>, EventSystemContract
    {
        public WCFSender()
        {
        }

        public WCFSender(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public WCFSender(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public WCFSender(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public WCFSender(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public void ProcessEvent(Event ev)
        {
            base.Channel.ProcessEvent(ev);
        }
    }
}

