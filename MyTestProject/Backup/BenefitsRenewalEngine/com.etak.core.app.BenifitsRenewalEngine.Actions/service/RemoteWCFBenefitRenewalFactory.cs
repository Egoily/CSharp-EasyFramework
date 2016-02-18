using System;
using System.Collections.Concurrent;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using com.etak.core.app.BenefitsRenewalEngine.contract;


namespace com.etak.core.app.BenifitsRenewalEngine.Actions.service
{
    public class RemoteWCFBenefitRenewalFactory : IBenefitRenewalFactory
    {
        private const String remoteServiceURLName = "RemoteServiceURL";
        static private ConcurrentQueue<ChannelFactory<IBenefitsRenewalService>> BenefitFactories = new ConcurrentQueue<ChannelFactory<IBenefitsRenewalService>>();
        

       
        public static void CreateFactoryOfURL(String url)
        {
            Uri uri = new Uri(url);
            EndpointAddress endpointAddress = new EndpointAddress(uri);
            ServicePoint servicePoint = System.Net.ServicePointManager.FindServicePoint(uri);
            servicePoint.ConnectionLimit = Int32.MaxValue;

            ChannelFactory<IBenefitsRenewalService> factory = new ChannelFactory<IBenefitsRenewalService>(GetBinding(), endpointAddress);
            BenefitFactories.Enqueue(factory);
        }

        public IBenefitsRenewalService GetInstance()
        {
            ChannelFactory<IBenefitsRenewalService> factory;
            if (!BenefitFactories.TryDequeue(out factory))
                throw new Exception("There are no factories to reaad, add by calling CreateFactoryOfURL");
            
            //Requeit so it behaves as a circular buffer
            BenefitFactories.Enqueue(factory);
            return factory.CreateChannel();
        }

       
        private static Binding GetBinding()
        {
            var readerQuotas = new System.Xml.XmlDictionaryReaderQuotas
            {
                MaxDepth = 32,
                MaxStringContentLength = 8192,
                MaxArrayLength = 16384,
                MaxBytesPerRead = 4096,
                MaxNameTableCharCount = 16384
            };

            var reliableSession = new OptionalReliableSession
            {
                Ordered = true,
                InactivityTimeout = new TimeSpan(0, 10, 0),
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

            WSHttpBinding binding = new WSHttpBinding
            {
                Name = "WSHttpBinding_HPS",
                ReceiveTimeout = new TimeSpan(0, 10, 0),
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

      
    }
}
