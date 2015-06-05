using System;
using System.IO;
using System.Linq;
using System.Text;
using com.etak.core.queue.Common;
using com.etak.core.queue.Messages;
using com.etak.core.queue.Serialization;
using log4net;

namespace com.etak.core.queue.Server
{
    /// <summary>
    /// Possible results of processing a message
    /// </summary>
    public enum DREProcessorResultCodes
    {
        /// <summary>
        /// There was an unknown error processing the message
        /// </summary>
        UnkwonError = -1,

        /// <summary>
        /// The message was processed correctly
        /// </summary>
        OK = 0,
        
        /// <summary>
        /// Unable to read the incoming message
        /// </summary>
        ErrorReadingMessage = 1,
        ErrorParsinRequest = 2,        
        ErrorEnqueuing = 3,
    }

    /// <summary>
    /// Exception thrown when there's an error processing an element in the queues.
    /// </summary>
    public class ProccessRequestException : Exception
    {
        /// <summary>
        /// Constructor for the exception
        /// </summary>
        /// <param name="text">Descriptive information about the exception</param>
        /// <param name="ex">Inner exception that cause the error</param>
        public ProccessRequestException(String text, Exception ex) : base(text, ex) { }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Type of the elements that will be sent/received inside the QueueDeliverMessageRequest </typeparam>
    public class HTTPQueueRequestReceiver<T> : IHTTPRequestHandler
    {
        private static readonly ILog Log = LogManager.GetLogger("QueueReceiverProcessor");
        private const Int32 MaxRequestSize = 10 * 1024 * 1024;
        
        private readonly QueueThread<T> Queue;
        private readonly ISerializer<QueueDeliverMessageRequest<T>> _serializer;
        private readonly ISerializer<QueueDeliverMessageResponse> _responseSerializer;
        private static Boolean SimulateReceiverDown = true;

        #region Constructors
        /// <summary>
        /// Constructor for the receveir
        /// </summary>
        /// <param name="queueConfig">The configuration to use in the queue</param>
        /// <param name="dequer">the implementation of the dequeuer for the received message</param>
        public HTTPQueueRequestReceiver(QueueThreadConfiguration queueConfig, IDequeuer<T> dequer)
        {
            _serializer = new JSONSerializer<QueueDeliverMessageRequest<T>>();
            _responseSerializer = new JSONSerializer<QueueDeliverMessageResponse>();
            Queue = new QueueThread<T>(queueConfig, dequer);
            Queue.Start();
        }
        #endregion

        #region IHTTPRequestHandler Members
        public void ProcessRequest(System.Net.HttpListenerContext context)
        {            
            if (context.Request.ContentLength64 > MaxRequestSize)
            {
                throw new Exception("Request size was too big, rejecting");
            }

            #region Initialize response to default message
            QueueDeliverMessageResponse response = new QueueDeliverMessageResponse();
            response.ElementsProccessed = 0;
            response.Result = DeliverResult.Error;
            response.ResultCode = (int)DREProcessorResultCodes.UnkwonError;
            response.ResultMessage = "UnkwonError";
            #endregion
          
            try
            {
                #region Read the request
                const int bufferSize = 2048;
                Byte[] buffer = new Byte[bufferSize];
                MemoryStream stream = new MemoryStream();

                try
                {

                    Stream requestStream = context.Request.InputStream;
                    Boolean HasData = true;
                    Int32 totalRead = 0;
                    while (HasData)
                    {
                        int readed = requestStream.Read(buffer, 0, bufferSize);
                        totalRead += readed;
                        if (readed == 0)
                        {
                            HasData = false;
                        }
                        else
                        {
                            stream.Write(buffer, 0, readed);
                        }
                        if (totalRead == context.Request.ContentLength64)
                        {
                            HasData = false;
                        }
                    }
                    if (SimulateReceiverDown)
                    {
                        throw new Exception("Receiver unavailable demo");
                    }
                }
                catch (Exception ex)
                {
                    response.ElementsProccessed = 0;
                    response.Result = DeliverResult.Error;
                    response.ResultCode = (int)DREProcessorResultCodes.ErrorReadingMessage;
                    response.ResultMessage = "Error reading the message";
                    String readed = Encoding.UTF8.GetString(stream.ToArray());
                    throw new ProccessRequestException(response.ResultMessage + " Readed:" + readed, ex);
                }

                #endregion

                #region Deserialize the request
                QueueDeliverMessageRequest<T> incomingMessage;
                //Read the request
                try
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    incomingMessage = _serializer.Deserialize(stream);
                }
                catch (Exception ex)
                {
                    response.ElementsProccessed = 0;
                    response.Result = DeliverResult.Error;
                    response.ResultCode = (int)DREProcessorResultCodes.ErrorParsinRequest;
                    response.ResultMessage = "Error parsing the request";
                    String Readed = Encoding.UTF8.GetString(stream.ToArray());
                    throw new ProccessRequestException(response.ResultMessage + " Readed:" + Readed, ex);
                }
                #endregion 

                #region Enqueue The message and create ok error message
                try
                {
                    //Enqueue the elements
                    Log.Debug("Elements received enqueueing: " + incomingMessage.Elements.Count() + " elements");
                    Queue.Enqueue(incomingMessage.Elements);

                    //Records were successgully enqueued Return OK
                    response.ElementsProccessed = incomingMessage.Elements.Count();
                    response.Result = DeliverResult.OK;
                    response.ResultCode = (int)DREProcessorResultCodes.OK;
                    response.ResultMessage = "Processed";
                }
                catch (Exception ex)
                {
                    response.ElementsProccessed = 0;
                    response.Result = DeliverResult.Error;
                    response.ResultCode = (int)DREProcessorResultCodes.ErrorEnqueuing;
                    response.ResultMessage = "Error enquing the message the request";
                    String Readed = Encoding.UTF8.GetString(stream.ToArray());
                    throw new ProccessRequestException(response.ResultMessage + " Readed:" + Readed, ex);
                }
                #endregion
            }
            catch (ProccessRequestException ex)
            {
                Log.Error("Handled error during operation, records were not proccessed:", ex);
            }
            catch (Exception ex)
            {
                Log.Error("Unknown error handling request, records were not proccessed:", ex);
            }
            finally
            {
                #region Send response
                          
                MemoryStream responseBuffer = _responseSerializer.Serialize(response);
                //String JsonString = Encoding.UTF8.GetString(responseBuffer.ToArray());
                context.Response.ContentLength64 = responseBuffer.Length;
                responseBuffer.WriteTo(context.Response.OutputStream);
                context.Response.OutputStream.Close();
                #endregion
            }
        }
        #endregion
    }
}
