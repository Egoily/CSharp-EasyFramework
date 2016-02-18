using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using com.etak.core.queue.Common;
using log4net;

namespace com.etak.core.queue.Client
{
    /// <summary>
    /// Uses a folder to persist the elements in an XML file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractXMLBackUpDequeuer<T> : IDequeuer<T> 
    {
        // ReSharper disable once StaticFieldInGenericType
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private const string DumpedFilesExtension = ".dump.xml";
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(List<T>));
        private const String DumpFolderAppsettingsKey = "QueueDumpFolder";
        private const String DefaultDumpFolder = "./QueueDump/";
        private String _dumpFolder = DefaultDumpFolder;

        #region IDequeuer<T> Members

        /// <summary>
        /// Method that must be implemented by the actual processor that process what has been dequeud
        /// </summary>
        /// <param name="currentBlock">the list of elements to process</param>
        /// <returns></returns>
        public abstract IList<QueueElementInformation<T>> Process(IList<QueueElementInformation<T>> currentBlock);

        public virtual void StartUp()
        {
            String dumpFolderInconfig = ConfigurationManager.AppSettings[DumpFolderAppsettingsKey];
            if (!String.IsNullOrEmpty(dumpFolderInconfig))
            {
                _dumpFolder = dumpFolderInconfig;
            }
            _dumpFolder += _dumpFolder.EndsWith(@"/") ? "" : Path.AltDirectorySeparatorChar.ToString(CultureInfo.InvariantCulture);
            
            if (!Directory.Exists(_dumpFolder))
            {
                log.WarnFormat("Creating dump folder: {0}", _dumpFolder);
                try
                {
                    Directory.CreateDirectory(_dumpFolder);
                }
                catch (Exception ex)
                {
                    log.Error("Dump folder did not exist and creation failed", ex);
                }
            }
        }

        /// <summary>
        /// Method that will be invoked when the queue is overflowing or the thread must exit and the queue is not empty
        /// </summary>
        /// <param name="currentEmergencyBlock">list of elements</param>
        public void BackupElements(IList<T> currentEmergencyBlock)
        {           
            MemoryStream buffer = new MemoryStream();
            foreach (T request in currentEmergencyBlock)
            {
                _serializer.Serialize(buffer, request);                
            }
            buffer.Seek(0, SeekOrigin.Begin);

            try
            {
                String fileName = null;
                while (fileName == null)// If the file exists, rename the file.
                {
                    fileName = String.Format("{0}QueueDump_{1:yyyy-MM-dd_HHmmss.fffffff}_{2}", _dumpFolder, DateTime.Now, DumpedFilesExtension);
                    if (File.Exists(fileName))
                        fileName = null;        
                }

                log.InfoFormat("Creating a new dump file: {0}", fileName);
                //Write requests to file
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {                   
                    using (StreamWriter streamWriter = new StreamWriter(fs))
                    {
                        buffer.WriteTo(streamWriter.BaseStream);
                        streamWriter.Flush();
                    }
                }
                log.InfoFormat("Dump file created");
            }
            catch (Exception ex)
            {
                log.Error("Error writing elements to the disk on backup action!");
                throw new Exception("Error writing elements to the disk on backup action!", ex);
            }
        }

        /// <summary>
        /// Method that will be invoked when the getting the elements back from backup files.
        /// </summary>
        /// <returns>list of elements</returns>
        public IList<T> RecoverElements()
        {
            IList<T> elementsRecovered = null;
            String[] files = Directory.GetFiles(_dumpFolder, "*" + DumpedFilesExtension);
            string file = files.FirstOrDefault();
            if (!string.IsNullOrEmpty(file))
            {
                using (FileStream fileStream = File.OpenRead(file))
                {                    
                   elementsRecovered =  (IList<T>) _serializer.Deserialize(fileStream);
                }
                File.Delete(file);
            }
            return (elementsRecovered);
        }

        
        #endregion
    }
}
