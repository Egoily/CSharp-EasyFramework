using com.etak.eventsystem.model;

namespace com.etak.eventsystem.wcfSender
{
    public class WCFClientFactory : IEventContractImplementorFactory
    {
        private string _endpointUrl;
        public WCFClientFactory(string endpointUrl)
        {
            this._endpointUrl = endpointUrl;
        }

        public EventSystemContract GetImplementation()
        {
            var binding = WCFConfigurationManager.BuildBinding();
            var endpoint = WCFConfigurationManager.BuildEndpoint(_endpointUrl);
            return new WCFSender(binding, endpoint);
        }

        public void Destroy(EventSystemContract implementation)
        {
            WCFSender sender = implementation as WCFSender;
            sender.Close();            
        }        
    }
}
