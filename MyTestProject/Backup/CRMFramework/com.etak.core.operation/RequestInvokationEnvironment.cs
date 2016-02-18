using System;
using System.Reflection;
using com.etak.core.model;

namespace com.etak.core.operation
{
    /// <summary>
    /// Class that holds network information for incoming requests and the MethodInfo for the request received. 
    /// </summary>
    public class RequestInvokationEnvironment
    {
        /// <summary>
        /// The front end method that received the 
        /// </summary>
        public MethodBase Invoker { get; set; }

        /// <summary>
        /// Source Up that sent the request
        /// </summary>
        public String SourceIp { get; set; }
        
        /// <summary>
        /// Proxy/Load Balancer Ip address
        /// </summary>
        public String ProxyIp { get; set; }

        /// <summary>
        /// The Ip that received the request
        /// </summary>
        public String DestinationIp { get; set; }

        /// <summary>
        /// Url to which the request was sent.
        /// </summary>
        public String ServingUrl { get; set; }

        /// <summary>
        /// Id of the session of the request
        /// </summary>
        public String SessionId { get; set; }

        /// <summary>
        /// user of the request
        /// </summary>
        public LoginInfo User { get; set; }

        /// <summary>
        /// Dealer of the request
        /// </summary>
        public DealerInfo Dealer { get; set; }
    }
}
