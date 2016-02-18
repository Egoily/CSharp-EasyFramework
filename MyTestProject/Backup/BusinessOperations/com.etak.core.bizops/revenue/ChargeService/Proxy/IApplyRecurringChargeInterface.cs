using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Services.Protocols;

namespace com.etak.core.bizops.revenue.ChargeService.Proxy
{
    /// <summary>
    /// Interface to CRM RecurringCharge
    /// </summary>
    public interface IApplyRecurringChargeInterface
    {
        /// <remarks/>
        event firstPeriodChargingApplyCompletedEventHandler firstPeriodChargingApplyCompleted;

        /// <summary>
        /// SoapVersion
        /// </summary>
        SoapProtocolVersion SoapVersion { get; set; }

        /// <summary>
        /// AllowAutoRedirect
        /// </summary>
        bool AllowAutoRedirect { get; set; }

        /// <summary>
        /// CookieContainer
        /// </summary>
        CookieContainer CookieContainer { get; set; }
        /// <summary>
        /// ClientCertificates
        /// </summary>
        X509CertificateCollection ClientCertificates { get; }
        /// <summary>
        /// EnableDecompression
        /// </summary>
        bool EnableDecompression { get; set; }

        /// <summary>
        /// UserAgent
        /// </summary>
        string UserAgent { get; set; }

        /// <summary>
        /// Proxy
        /// </summary>
        IWebProxy Proxy { get; set; }

        /// <summary>
        /// UnsafeAuthenticatedConnectionSharing
        /// </summary>
        bool UnsafeAuthenticatedConnectionSharing { get; set; }

        /// <summary>
        /// Credentials
        /// </summary>
        ICredentials Credentials { get; set; }


        /// <summary>
        /// UseDefaultCredentials
        /// </summary>
        bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// ConnectionGroupName
        /// </summary>
        string ConnectionGroupName { get; set; }

        /// <summary>
        /// PreAuthenticate
        /// </summary>
        bool PreAuthenticate { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// RequestEncoding
        /// </summary>
        Encoding RequestEncoding { get; set; }

        /// <summary>
        /// Timeout
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// Site
        /// </summary>
        ISite Site { get; set; }

        /// <summary>
        /// Container
        /// </summary>
        IContainer Container { get; }

        /// <remarks/>
        [SoapDocumentMethod("", RequestNamespace = "http://apis.etak.com/", ResponseNamespace = "http://apis.etak.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        chargingApplyResponse firstPeriodChargingApply([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)] chargingRequestParameters chargingRequestParameters);

        /// <remarks/>
        System.IAsyncResult BeginfirstPeriodChargingApply(chargingRequestParameters chargingRequestParameters, System.AsyncCallback callback, object asyncState);

        /// <remarks/>
        chargingApplyResponse EndfirstPeriodChargingApply(System.IAsyncResult asyncResult);

        /// <remarks/>
        void firstPeriodChargingApplyAsync(chargingRequestParameters chargingRequestParameters);

        /// <remarks/>
        void firstPeriodChargingApplyAsync(chargingRequestParameters chargingRequestParameters, object userState);

        /// <remarks/>
        void CancelAsync(object userState);

        /// <summary>
        /// Discover
        /// </summary>
        void Discover();

        /// <summary>
        /// Abort
        /// </summary>
        void Abort();

        /// <summary>
        /// Dispose
        /// </summary>
        void Dispose();

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        string ToString();

        /// <summary>
        /// Disposed
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// GetLifetimeService
        /// </summary>
        /// <returns></returns>
        object GetLifetimeService();

        /// <summary>
        /// InitializeLifetimeService
        /// </summary>
        /// <returns></returns>
        object InitializeLifetimeService();

        /// <summary>
        /// CreateObjRef
        /// </summary>
        /// <param name="requestedType"></param>
        /// <returns></returns>
        ObjRef CreateObjRef(Type requestedType);
    }
}