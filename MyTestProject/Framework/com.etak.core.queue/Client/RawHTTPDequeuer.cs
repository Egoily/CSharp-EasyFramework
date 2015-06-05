using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using com.etak.core.queue.Common;
using log4net;

namespace com.etak.core.queue.Client
{
    public class RawHTTPDequeuer : IDequeuer<String>
    {
        private static readonly ILog log = LogManager.GetLogger("RawHTTPDequeuer");
        private const string SendMethod = "POST";
        private const string DumpedFilesExtension = ".dump";
        private const string FileSpliter = "\r\n>>>\r\n";
        private const string ElementProcessedlogFormat = "Element processed {0}: \r\n[Infomation: TryCounter = {1}, ProcessedTimeStamp = {2}]\r\n[Request: {3}]\r\n[Responce: {4}]";
        private const Int32 RequestTimeOutMilliseconds = 50 * 1000;

        private readonly Uri QueueReceiverURL;
        private readonly String DumpFolder;

        public RawHTTPDequeuer(Uri ReceiverURL, String DestinationFolderOnOverflow)
        {
            if (ReceiverURL == null)
                throw new ArgumentException("Paremeter ReceiverURL can't be null");

            this.QueueReceiverURL = ReceiverURL;



            //check file directory availability and space
            if (!Directory.Exists(DestinationFolderOnOverflow))
            {
                try
                {
                    Directory.CreateDirectory(DestinationFolderOnOverflow);
                    log.Info("Directory path:" + DestinationFolderOnOverflow + " did not exist, but folder has been sucessfully created");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Directory path " + DestinationFolderOnOverflow + " does not exist and clould not be created", ex);
                }
            }

            //Check write rights on the destination folder
            FileIOPermission WritePermission = new FileIOPermission(FileIOPermissionAccess.Write, DestinationFolderOnOverflow);
            if (!SecurityManager.IsGranted(WritePermission))
            {
                throw new ArgumentException("The current user: " + Thread.CurrentPrincipal.Identity.Name + " does not have write permissions on destination folder: " + DestinationFolderOnOverflow);
            }

            //Check write rights on the destination folder
            FileIOPermission ReadPermission = new FileIOPermission(FileIOPermissionAccess.Read, DestinationFolderOnOverflow);
            if (!SecurityManager.IsGranted(ReadPermission))
            {
                throw new ArgumentException("The current user: " + Thread.CurrentPrincipal.Identity.Name + " does not have read permissions on destination folder: " + DestinationFolderOnOverflow);
            }

            //Check disk space on destination folder
            String DirectoryRoot = Directory.GetDirectoryRoot(DestinationFolderOnOverflow);
            DriveInfo d = new DriveInfo(DirectoryRoot);

            if (d.AvailableFreeSpace < 1024 * 1024 * 100)
            {
                throw new ArgumentException("The drive unit provided that contains " + DestinationFolderOnOverflow + " has only " + d.AvailableFreeSpace / (1024 * 1024) + " MB available");
            }

            //accept folder for destination
            this.DumpFolder = DestinationFolderOnOverflow;


        }

        /// <summary>
        /// Method that will write the block to a file in the path specified in the constructor
        /// </summary>
        /// 
        ///Because FileSpliter = "\r\n>>>\r\n";
        ///
        ///So the format is as following:
        ///<Request1>
        ///>>>
        ///<Request2>
        ///>>>
        ///
        ///In this way, we can get every single request easily.
        ///
        /// <param name="CurrentEmergencyBlock"></param>
        public void BackupElements(IList<String> CurrentEmergencyBlock)
        {
            StringBuilder requests = new StringBuilder();
            foreach (string request in CurrentEmergencyBlock)
            {
                requests.Append(request);
                requests.Append(FileSpliter);
            }

            try
            {
                String NeedsSeparator = DumpFolder.EndsWith(@"/") ? "" : Path.AltDirectorySeparatorChar.ToString();
                String FileName = String.Format("{0}{1}QueueDump_{2:yyyy-MM-dd_HHmmss.fffffff}_{3}", DumpFolder, NeedsSeparator, DateTime.Now, DumpedFilesExtension); //yyyy-MM-dd_HHmmss.fffffff costs 25ms, maybe a minor performance issue.
                while (File.Exists(FileName))// If the file exists, rename the file.
                {
                    FileName = String.Format("{0}{1}QueueDump_{2:yyyy-MM-dd_HHmmss.fffffff}_{3}", DumpFolder, NeedsSeparator, DateTime.Now.AddMilliseconds(1), DumpedFilesExtension);
                }

                //Write requests to file
                using (FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fs))
                    {
                        streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                        streamWriter.Write(requests.ToString());
                        streamWriter.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error writing elements to the disk on backup action!");
                throw new Exception("Error writing elements to the disk on backup action!", ex);
            }
        }


        public IList<QueueElementInformation<String>> Process(IList<QueueElementInformation<String>> CurrentBlock)
        {
            List<QueueElementInformation<String>> failedElements = new List<QueueElementInformation<String>>();

            while (CurrentBlock.Count > 0)
            {
                //Prepare The request
                QueueElementInformation<String> currentElement = CurrentBlock[0];
                string response = string.Empty;

                #region send request and get response
                try
                {
                    HttpWebRequest HttpTransport = GetWebRequestToSender();

                    using (Stream reqStream = HttpTransport.GetRequestStream())
                    {
                        Encoding encoding = Encoding.UTF8;
                        byte[] bs = Encoding.ASCII.GetBytes(currentElement.Element);
                        reqStream.Write(bs, 0, bs.Length);
                    }

                    //Send the request and get the response
                    using (HttpWebResponse HttpTransportResponse = (HttpWebResponse)HttpTransport.GetResponse())
                    {
                        using (Stream responseStream = HttpTransportResponse.GetResponseStream())
                        {
                            using (System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, System.Text.Encoding.UTF8))
                            {
                                response = reader.ReadToEnd();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    log.Warn("Processing element encounter exception.", ex);
                }
                #endregion

                currentElement.TryCounter++;
                currentElement.LastProcessedTimestamp = DateTime.Now;
                CurrentBlock.Remove(currentElement);

                bool isOk = false;
                if (!string.IsNullOrEmpty(response))
                {
                    isOk = response.Contains("<ErrorCode>Ok</ErrorCode>");
                }

                if (isOk)
                {
                    log.Info(string.Format(ElementProcessedlogFormat, "successfully", currentElement.TryCounter, currentElement.LastProcessedTimestamp,
                                                        currentElement.Element, response ?? ""));
                }
                else
                {
                    failedElements.Add(currentElement);

                    log.Info(string.Format(ElementProcessedlogFormat, "fails", currentElement.TryCounter, currentElement.LastProcessedTimestamp,
                                currentElement.Element.ToString(), (response == null ? "No valid response from server" : response)));
                }
            }

            return failedElements;
        }


        /// <summary>
        /// Method to create a WebRequest to the receiver, using HTTP 1.1 to enable socket reusing.
        /// The underlying framework takes care of reusing and does it whenever is possible.
        /// </summary>
        /// <returns>the connected request to the receiver</returns>
        private HttpWebRequest GetWebRequestToSender()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(QueueReceiverURL);
            request.ProtocolVersion = new System.Version("1.1");
            request.Method = SendMethod;
            request.KeepAlive = true;
            request.ContentType = "text/xml; charset=utf-8";
            //request.TransferEncoding = "UTF-8";
            request.SendChunked = false;
            request.Timeout = RequestTimeOutMilliseconds;
            request.Headers.Add("IsAlreadyQueued", "1");
            return (request);
        }

        public IList<String> ServiceIsOnline()
        {
            log.Info("Service is online again recovering files");
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the elements back from back up files.
        /// </summary>
        /// 
        /// Please see the comments of 'BackupElements' method.
        /// 
        /// <returns></returns>
        public IList<String> RecoverElements()
        {
            IList<String> elementsRecovered = null;

            String[] files = Directory.GetFiles(DumpFolder, "*" + DumpedFilesExtension);
            string file = files.FirstOrDefault();
            if (!string.IsNullOrEmpty(file))
            {
                using (FileStream fileStream = File.OpenRead(file))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream, System.Text.Encoding.UTF8))
                    {
                        string fileContent = reader.ReadToEnd();
                        string[] requests = fileContent.Split(new string[] { FileSpliter }, StringSplitOptions.RemoveEmptyEntries);
                        if (requests != null && requests.Length > 0)
                        {
                            elementsRecovered = new List<string>(requests.Length);
                            foreach (string request in requests)
                            {
                                elementsRecovered.Add(request);
                            }
                        }
                    }
                }
                File.Delete(file);
            }

            return (elementsRecovered);
        }

        public void StartUp()
        {
            //No action required.
        }
    }
}
