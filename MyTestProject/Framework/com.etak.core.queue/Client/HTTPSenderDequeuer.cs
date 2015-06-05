using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using com.etak.core.queue.Common;
using com.etak.core.queue.Messages;
using com.etak.core.queue.Serialization;
using log4net;

namespace com.etak.core.queue.Client
{
    public class HTTPSenderDequeuer <T> : IDequeuer<T>
    {
        private static readonly ILog log = log4net.LogManager.GetLogger("HTTPSenderDequeuer");
        private const string SendMethod = "POST";
        private const string DumpedFilesExtension = ".dump";

        //private String SerializerClassName = "AsyncronousQueueCommons.ET.Util.AsyncronousQueue.Common.JSONSerializer";
        //private String SerializerAssemblyName = "AsyncronousQueueCommons";
        private readonly Uri QueueReceiverURL;
        private readonly String DumpFolder;
        private readonly ISerializer<QueueDeliverMessageRequest<T>> RequestSerializer = null;
        private readonly ISerializer<QueueDeliverMessageResponse> ResponseSerializer = null;

        public HTTPSenderDequeuer(Uri ReceiverURL, SerializationType SerializationType, String DestinationFolderOnOverflow)
        {
            if (ReceiverURL == null)
                throw new ArgumentException("Paremeter ReceiverURL can't be null");

            this.QueueReceiverURL = ReceiverURL;
            
            switch(SerializationType)
            {
                case SerializationType.JSON:
                     RequestSerializer = new JSONSerializer<QueueDeliverMessageRequest<T>>();
                     ResponseSerializer = new JSONSerializer<QueueDeliverMessageResponse> ();
                     break;
                case SerializationType.XML:
                     RequestSerializer = new XMLSerializer<QueueDeliverMessageRequest<T>>();
                     ResponseSerializer = new XMLSerializer<QueueDeliverMessageResponse>();
                     break;
                default:
                     throw new ArgumentException("Unssuported Serialization");
            }

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
        /// <param name="CurrentEmergencyBlock"></param>
        public void BackupElements(IList<T> CurrentEmergencyBlock)
        {
            QueueDeliverMessageRequest<T> Message = new QueueDeliverMessageRequest<T>();
            Message.Elements = CurrentEmergencyBlock;
            MemoryStream Buffer = null;
            FileStream outputFile = null;
            try
            {
                Buffer = RequestSerializer.Serialize(Message);

                String NeedsSeparator = DumpFolder.EndsWith(@"/") ? "" : Path.AltDirectorySeparatorChar.ToString();
                String FileName = String.Format("{0}{1}QueueDump_{2:yyyy-MM-dd_HHmmss.fffffff}_{3}", DumpFolder, NeedsSeparator, DateTime.Now, DumpedFilesExtension);
                outputFile = File.OpenWrite(FileName);
                Buffer.WriteTo(outputFile);
            }
            catch (Exception ex)
            {
                log.Error("Error writing elements to the disk on backup action!");
                throw new Exception("Error writing elements to the disk on backup action!", ex);
            }
            finally
            {
                if (Buffer != null)
                {   
                    Buffer.Close();
                }
                if (outputFile != null)
                {
                    outputFile.Close();
                }
            }
        }

        public IList<QueueElementInformation<T>> Process(IList<QueueElementInformation<T>> CurrentBlock)
        {
            List<QueueElementInformation<T>> failedElements = new List<QueueElementInformation<T>>();

            List<T> requestBlock = CurrentBlock.Select(x => x.Element).ToList();

            HttpWebRequest HttpTransport = GetWebRequestToSender();
            QueueDeliverMessageRequest<T> Message = new QueueDeliverMessageRequest<T>();
            Message.Elements = requestBlock;

            MemoryStream EncodedMessage = RequestSerializer.Serialize(Message);
            //EncodedMessage.Seek(0, SeekOrigin.Begin);
            //String JsonString = Encoding.UTF8.GetString(EncodedMessage.ToArray());
            //QueueDeliverMessageRequest<T> Message2 = RequestSerializer.Deserialize(EncodedMessage);


            //Send The request
            HttpTransport.ContentLength = EncodedMessage.Length;
            EncodedMessage.Seek(0, SeekOrigin.Begin);
            EncodedMessage.WriteTo(HttpTransport.GetRequestStream());


            HttpWebResponse HttpTransportResponse = (HttpWebResponse)HttpTransport.GetResponse();
            QueueDeliverMessageResponse response = ResponseSerializer.Deserialize(HttpTransportResponse.GetResponseStream());
            //Close the response so we can reuse the undelying connection.
            HttpTransportResponse.Close();
            if (response.Result == DeliverResult.OK && response.ElementsProccessed == Message.Elements.Count())
            {
                //after processed clear the local queue
                CurrentBlock.Clear();
                //System is up, should we check dumped files?
                //foreach (String file in Directory.GetFiles(this.DumpFolder))
                //{
                //  if (file.EndsWith(".dump", StringComparison.OrdinalIgnoreCase))
                //{
                //  IList<IDeliverable> DumpedFiles = 
                //}
                //}
            }
            else
            {                
                log.Error("Unable to send all the request to the HTTP destination: " + (response==null ? " No valid response from server" : response.ToString()));
                throw new Exception("Receiver did not processed the request");
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
            //request.TransferEncoding = "UTF-8";
            request.SendChunked = false;
            request.Timeout = 50 *1000;
            return (request);
        }

        public IList<T> ServiceIsOnline()
        {
            log.Info("Service is online again recovering files");
            return RecoverElements();
        }

        public IList<T> RecoverElements()
        {
            String[] files = Directory.GetFiles(DumpFolder, DumpedFilesExtension);
            IList<T> elementsRecovered = null;
            IEnumerator<String> iterator = files.AsEnumerable().GetEnumerator();

            while (iterator.MoveNext())
            {
                String s = iterator.Current;
                FileStream fileStream = null;
                try
                {
                    fileStream = File.OpenRead(s);
                    QueueDeliverMessageRequest<T> Message = this.RequestSerializer.Deserialize(fileStream);
                    elementsRecovered = Message.Elements;
                    fileStream.Close();
                    File.Delete(s);
                }
                catch (Exception ex)
                {
                    log.Error("Error reading elements in the backup in file: " + s, ex);
                }
                finally
                {
                    if (fileStream != null)
                        fileStream.Close();
                }
            }
            return (elementsRecovered);
        }

        public void StartUp()
        {
            //No action required.
        }
    }
}
