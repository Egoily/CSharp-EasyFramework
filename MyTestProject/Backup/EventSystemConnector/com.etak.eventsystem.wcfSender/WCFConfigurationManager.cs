using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace com.etak.eventsystem.wcfSender
{
    public class WCFConfigurationManager
    {
        public static WSHttpBinding BuildBinding()
        {
            var readerQuotas = new System.Xml.XmlDictionaryReaderQuotas()
            {
                MaxDepth = 32,
                MaxStringContentLength = 8192,
                MaxArrayLength = 16384,
                MaxBytesPerRead = 4096,
                MaxNameTableCharCount = 16384
            };

            var reliableSession = new OptionalReliableSession()
            {
                Ordered = true,
                InactivityTimeout = new System.TimeSpan(0, 10, 0),
                Enabled = false
            };

            var security = new WSHttpSecurity
            {
                Mode = SecurityMode.None,
                Transport = new HttpTransportSecurity
                {
                    ClientCredentialType = HttpClientCredentialType.None,
                    ProxyCredentialType = HttpProxyCredentialType.None,
                    Realm = ""
                },
                Message = new NonDualMessageSecurityOverHttp
                {
                    ClientCredentialType = MessageCredentialType.None,
                    NegotiateServiceCredential = true,
                    EstablishSecurityContext = true
                }
            };

            WSHttpBinding binding = new WSHttpBinding()
            {
                Name = "WSHttpBinding_EventSystemContract",
                ReceiveTimeout = new System.TimeSpan(0, 10, 0),
                BypassProxyOnLocal = false,
                TransactionFlow = false,
                HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                MaxBufferPoolSize = 524288,
                MaxReceivedMessageSize = 65536,
                MessageEncoding = WSMessageEncoding.Text,
                TextEncoding = System.Text.Encoding.UTF8,
                UseDefaultWebProxy = true,
                AllowCookies = false,
                ReaderQuotas = readerQuotas,
                ReliableSession = reliableSession,
                Security = security
            };

            return binding;
        }

        public static EndpointAddress BuildEndpoint(string url)
        {
            var endpointAddress = new EndpointAddress(url);
            return endpointAddress;
        }
    }
}
