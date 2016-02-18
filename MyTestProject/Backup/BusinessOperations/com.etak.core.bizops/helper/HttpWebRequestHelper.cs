using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using com.etak.core.bizops.revenue.CallDREQuerySubscriberPromotion;

namespace com.etak.core.bizops.helper
{
    /// <summary>
    /// Helper for Call to HttpWebRequest in order to be able to mock it
    /// </summary>
    public class HttpWebRequestHelper : IHttpWebRequestHelper
    {
        #region Public Methods

        /// <summary>
        /// GetResponse
        /// </summary>
        /// <param name="url"></param>
        /// <param name="msec"></param>
        /// <param name="serializedRequest"></param>
        /// <returns></returns>
        public byte[] GetResponse(string url,int? msec, byte[] serializedRequest)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            if (msec.HasValue)   webRequest.Timeout = (int) msec;
            webRequest.Method = "POST";

            byte[] reqContent;
            reqContent = serializedRequest;
            webRequest.ContentLength = reqContent.Length;
            Stream stream = webRequest.GetRequestStream();
            stream.Write(reqContent, 0, reqContent.Length);
            stream.Close();

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            stream = response.GetResponseStream();
            byte[] b = ReadStreamToEnd(stream);
            stream.Close();
            response.Close();

            return (b);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// ReadStreamToEnd
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private byte[] ReadStreamToEnd(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] buffer = new byte[4096];

                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead <= 0)
                        break;

                    ms.Write(buffer, 0, bytesRead);
                }

                return ms.ToArray();
            }
        }

        #endregion
    }
}
