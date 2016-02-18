using Apache.NMS;
using com.etak.core.jms.listener;
using com.etak.eventsystem.model;
using com.etak.eventsystem.model.events;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Text;
using System.Xml;

namespace com.etak.eventsystem.jmsReceiver
{
    /// <summary>
    /// Event system receiver over JMS,
    /// </summary>
    /// <typeparam name="TExecutor">The type that will instantiated per message to proccess the message, must implement EventSystemContract</typeparam>
    public class JMSEventMessageAdapter<TExecutor> : IMessageProcessor where TExecutor : EventSystemContract
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly ContractDescription ServiceDescription = ContractDescription.GetContract(typeof(EventSystemContract));
        private static readonly String ContractOperationName = "ProcessEvent";
        
        public void ProcessMessage(IMessage message)
        {
            if (message == null)
            {
                Log.Error("Received a null message from the queue can't process");
                throw new InvalidMessageException();
            }

            ITextMessage textMessage = message as ITextMessage;
            if (textMessage == null)
            {
                Log.Error("Received a message from the queue that is not a text message, can't process");
                throw new InvalidMessageException();
            }
            Log.DebugFormat("Converting event soap to object: {0}", textMessage.Text);

            //Convert from soap to Event.
            //Cast is needed as signature of UnMarshallMessage has been changed to return Object and allow to share one function for simple events as well
            Object returnedEvent = ReadOperationParameters(Encoding.UTF8.GetBytes(textMessage.Text), ContractOperationName)[0];
            Event ev = (Event)returnedEvent; 

            //Create an instance of the executor
            EventSystemContract executor = Activator.CreateInstance<TExecutor>();

            //Invoke the executor
            executor.ProcessEvent(ev);
        }

        public static object[] ReadOperationParameters(byte[] message, string operationContractName)
        {
            object[] parameters;

            using (MemoryStream stringReader = new MemoryStream(message))
            using (XmlReader messageReader = XmlReader.Create(stringReader))
            {
                try
                {
                    System.ServiceModel.Channels.Message soapMessage = System.ServiceModel.Channels.Message.CreateMessage(messageReader, 1024 * 1024, System.ServiceModel.Channels.MessageVersion.Soap12WSAddressing10);
                    XmlDictionaryReader bodyReader = soapMessage.GetReaderAtBodyContents();

                    //By message body
                    operationContractName = bodyReader.LocalName;

                    OperationDescription operationDescription;
                    // here we need to decide if the message is full Event System or Simplified version with unknown message's structure in payload tag

                    // the original Saudi Event system with Rich Events
                    //By Headers? operationContractName = soapMessage.Headers.Action;
                    operationDescription = ServiceDescription.Operations.Single(op => op.Name == operationContractName);

                    MessageDescription inputMessage = operationDescription.Messages.Single(msgDescription => msgDescription.Direction == MessageDirection.Input);
                    MessageBodyDescription bodyDescription = inputMessage.Body;

                    bodyReader.ReadStartElement(bodyDescription.WrapperName, bodyDescription.WrapperNamespace);
                    parameters = new object[inputMessage.Body.Parts.Count];

                    foreach (var part in bodyDescription.Parts)
                    {
                        var dcs = new DataContractSerializer(part.Type, part.Name, part.Namespace, operationDescription.KnownTypes);
                        parameters[part.Index] = dcs.ReadObject(bodyReader, true);
                    }
                    bodyReader.ReadEndElement();
                }
                catch (Exception ex)
                {
                    String errorMessage;

                    stringReader.Seek(0, SeekOrigin.Begin);
                    using (TextReader reader = new StreamReader(stringReader))
                    {
                        errorMessage = reader.ReadToEnd();
                    }

                    Log.ErrorFormat("Error parsing the message, Error:{0}, message: {1}", ex.Message, errorMessage);
                    throw new InvalidMessageException();
                }
            }
            return parameters;
        }
    }
}
