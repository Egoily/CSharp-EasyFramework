using System;
using System.IO;
using System.Reflection;
using System.Web.Services.Protocols;
using log4net;

namespace com.etak.core.bizops.SOAPExtensions
{

    /// Define a SOAP Extension that traces the SOAP request and SOAP
    /// response for the Web service method the SOAP extension is
    /// applied to.

    public class TraceExtension : SoapExtension
    {
        Stream oldStream;
        Stream newStream;
        string filename;

        enum LOGTYPES { LOCALDISK=0,LOG4NET};


        static LOGTYPES logtype = LOGTYPES.LOG4NET;
        static private readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// Save the Stream representing the SOAP request or SOAP response into
        /// a local memory buffer.
        public override Stream ChainStream(Stream stream)
        {
            oldStream = stream;
            newStream = new MemoryStream();
            return newStream;
        }

        /// When the SOAP extension is accessed for the first time, the XML Web
        /// service method it is applied to is accessed to store the file
        /// name passed in, using the corresponding SoapExtensionAttribute.   
        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return ((TraceExtensionAttribute)attribute).Filename;
        }

        /// The SOAP extension was configured to run using a configuration file
        /// instead of an attribute applied to a specific Web service
        /// method.
        public override object GetInitializer(Type WebServiceType)
        {
            // Return a file name to log the trace information to, based on the
            // type.
            return "C:\\" + WebServiceType.FullName + ".log";
        }

        /// Receive the file name stored by GetInitializer and store it in a
        /// member variable for this specific instance.
        public override void Initialize(object initializer)
        {
            filename = (string)initializer;
        }

        ///  If the SoapMessageStage is such that the SoapRequest or
        ///  SoapResponse is still in the SOAP format to be sent or received,
        ///  save it out to a file.
        public override void ProcessMessage(SoapMessage message)
        {
            switch (message.Stage)
            {
                case SoapMessageStage.BeforeSerialize:
                    break;
                case SoapMessageStage.AfterSerialize:
                    WriteOutput(message);
                    break;
                case SoapMessageStage.BeforeDeserialize:
                    WriteInput(message);
                    break;
                case SoapMessageStage.AfterDeserialize:
                    break;
                default:
                    throw new Exception("invalid stage");
            }
        }

        /// <summary>
        /// Log Output
        /// </summary>
        /// <param name="message"></param>
        public void WriteOutput(SoapMessage message)
        {
            string soapString = "";

            switch(logtype)
            {
                case LOGTYPES.LOCALDISK:
                    newStream.Position = 0;
                    FileStream fs = new FileStream(filename, FileMode.Append,
                                                   FileAccess.Write);
                    StreamWriter w = new StreamWriter(fs);

                    soapString = "SoapRequest";
                    w.WriteLine("-----" + soapString + " at " + DateTime.Now);
                    w.Flush();
                    Copy(newStream, fs);
                    w.Close();
                    newStream.Position = 0;
                    Copy(newStream, oldStream);

                    break;
                case LOGTYPES.LOG4NET:

                    newStream.Position = 0;
                    var sr = new StreamReader(newStream);
                    var myStr = sr.ReadToEnd();
                    soapString = "SoapRequest";

                    Log.Info("-----" + soapString + " at " + DateTime.Now + myStr);

                    newStream.Position = 0;
                    Copy(newStream, oldStream);
                    
                    break;
            }

        }

        /// <summary>
        /// Log Input
        /// </summary>
        /// <param name="message"></param>
        public void WriteInput(SoapMessage message)
        {
            string soapString = "";

            switch (logtype)
            {
                case LOGTYPES.LOCALDISK:
                    Copy(oldStream, newStream);
                    FileStream fs = new FileStream(filename, FileMode.Append,
                                                   FileAccess.Write);
                    StreamWriter w = new StreamWriter(fs);

                    soapString = "SoapResponse";
                    w.WriteLine("-----" + soapString +
                                " at " + DateTime.Now);
                    w.Flush();
                    newStream.Position = 0;
                    Copy(newStream, fs);
                    w.Close();
                    newStream.Position = 0;

                    break;
                case LOGTYPES.LOG4NET:

                    Copy(oldStream, newStream);
                    newStream.Position = 0;
                    var sr = new StreamReader(newStream);
                    var myStr = sr.ReadToEnd();
                    soapString = "SoapResponse";

                    Log.Info("-----" + soapString + " at " + DateTime.Now + myStr);
                    

                    newStream.Position = 0;

                    break;
            }
        }

        /// <summary>
        /// Copy stream auxiliar
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        void Copy(Stream from, Stream to)
        {
            TextReader reader = new StreamReader(from);
            TextWriter writer = new StreamWriter(to);
            writer.WriteLine(reader.ReadToEnd());
            writer.Flush();
        }
    }

    /// Create a SoapExtensionAttribute for the SOAP Extension that can be
    /// applied to a Web service method.
    [AttributeUsage(AttributeTargets.Method)]
    public class TraceExtensionAttribute : SoapExtensionAttribute
    {

        private string filename = "c:\\log.txt";
        private int priority;

        /// <summary>
        /// ExtensionType
        /// </summary>
        public override Type ExtensionType
        {
            get { return typeof(TraceExtension); }
        }

        /// <summary>
        /// Priority
        /// </summary>
        public override int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        /// <summary>
        /// Filename
        /// </summary>
        public string Filename
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
            }
        }
    }

}