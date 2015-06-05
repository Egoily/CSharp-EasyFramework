using System.Net;

namespace com.etak.core.queue.Server
{
    /// <summary>
    /// Interface for a handler of HTTP requests
    /// </summary>
    public interface IHTTPRequestHandler 
    {
        /// <summary>
        /// Processes one HTTP request
        /// </summary>
        /// <param name="context">the request that needs to be processed</param>
        void ProcessRequest(HttpListenerContext context);
    }
}
