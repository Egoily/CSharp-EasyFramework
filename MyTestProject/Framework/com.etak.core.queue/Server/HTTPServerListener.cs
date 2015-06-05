using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using log4net;

namespace com.etak.core.queue.Server
{
    /// <summary>
    /// Exception thrown when attempting to a service already stopped.
    /// </summary>
    class ServiceIsStopped : Exception
    {
    }

    /// <summary>
    /// Htttp listener, with custom URL hander, each URL must provide an implementation of IHTTPRequest handler to
    /// process each request
    /// </summary>
    public class HTTPServerListener
    {
        private static readonly ILog Log = LogManager.GetLogger("HttpListener");

        private readonly HttpListener HTTPServer = new HttpListener();
        private Thread ListenThread;
        private Boolean IsRunning;
        readonly IDictionary<String, IHTTPRequestHandler> UrlDispatcherMapping;

        public HTTPServerListener(IDictionary <String, IHTTPRequestHandler> UrlDispatcherMapping)
        {
            //Parameter and environment checking
            if (!HttpListener.IsSupported)
            {
                Log.Error("HTTP listener is not suported in current platform");
                throw new Exception("Can't start HTTP listener, is not supported in current platform");
            }
            if (UrlDispatcherMapping == null)
            {
                throw new ArgumentException("UrlDispatcherMapping was null, unable to start listener"); 
            }
            if (!UrlDispatcherMapping.Any())
            {
                throw new ArgumentException("UrlDispatcherMapping was empty, unable to start listener, at least one URL and dispatcher mas be provided"); 
            }

            //Creating HTTP listener server
            this.UrlDispatcherMapping = new Dictionary<String, IHTTPRequestHandler>();
            foreach (String URL in UrlDispatcherMapping.Keys)
            {
                try
                {
                    String upperURL = URL.ToUpper();
                    this.UrlDispatcherMapping.Add(upperURL, UrlDispatcherMapping[URL]);
                    HTTPServer.Prefixes.Add(upperURL);
                }
                catch (Exception ex)
                {
                    Log.Error("The prefix " + URL + " Was not a valid prefix", ex);
                    throw new ArgumentException("The prefix " + URL + " Was not a valid prefix", ex);
                }
            }
        }

        /// <summary>
        /// Starts a new thread, starting the HTTP listener and processing the requests
        /// in that thread
        /// </summary>
        public void Start()
        {
            //Starting thread.
            ListenThread = new Thread(Run);
            ListenThread.Name = "HTTPListener";
            IsRunning = true;
            ListenThread.Start();
        }

        private void Run()
        {
            try
            {
                Log.Info("Starting HTTP server");
                HTTPServer.Start();
            }
            catch (Exception ex)
            {
                Log.Error("Unable to start HTTPServer, remind that it requires admin privileges to listen in a port and that the port is not already in use", ex);
            }

            //Loop to receive more requests.
            Log.Info("HTTP server started, waiting for requests");
            while (IsRunning)
            {
                try
                {
                    HttpListenerContext ContextRequest = null;
                    try
                    {
                        //Wait until the request arrive
                        Log.Debug("Waiting for request");
                        ContextRequest = HTTPServer.GetContext();
                        Log.Debug("HTTP request received");
                                          
                    }
                    catch (ThreadAbortException)
                    {
                        this.IsRunning = false;
                        throw new ServiceIsStopped();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Error receiving the request", ex);
                    }

                    //Process the request if successfully received.
                    if (null != ContextRequest)
                    {
                        try
                        {
                            IHTTPRequestHandler handler = null;
                            String UpperURI = ContextRequest.Request.Url.AbsoluteUri.ToUpper();
                            if (UrlDispatcherMapping.TryGetValue(UpperURI, out handler))
                            {
                                handler.ProcessRequest(ContextRequest);
                            }
                            else
                            {
                                Log.Warn("Received a request with an uknown handler, closing request:" + ContextRequest.Request.Url.AbsoluteUri);
                                ContextRequest.Response.Close();
                            }                            
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Error sending the request to the dispatcher", ex);
                        }
                    }
                }
                catch (ServiceIsStopped)
                {

                }
            }
        }
    }
}
