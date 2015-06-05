using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using com.etak.core.queue.Common;
using log4net;

namespace com.etak.core.queue.Client
{
    public abstract class AbstractXMLBackUpDequeuer<T> : IDequeuer<T> 
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string DumpedFilesExtension = ".dump.xml";
        private XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        private const String DumpFolderAppsettingsKey = "QueueDumpFolder";
        private const String DefaultDumpFolder = "./QueueDump/";
        private String DumpFolder = DefaultDumpFolder;

        #region IDequeuer<T> Members

        public abstract IList<QueueElementInformation<T>> Process(IList<QueueElementInformation<T>> CurrentBlock);

        public virtual void StartUp()
        {
            String DumpFolderInconfig = ConfigurationManager.AppSettings[DumpFolderAppsettingsKey];
            if (!String.IsNullOrEmpty(DumpFolderInconfig))
            {
                DumpFolder = DumpFolderInconfig;
            }
            DumpFolder += DumpFolder.EndsWith(@"/") ? "" : Path.AltDirectorySeparatorChar.ToString();
            
            if (!Directory.Exists(DumpFolder))
            {
                log.WarnFormat("Creating dump folder: {0}", DumpFolder);
                try
                {
                    Directory.CreateDirectory(DumpFolder);
                }
                catch (Exception ex)
                {
                    log.Error("Dump folder did not exist and creation failed", ex);
                }
            }
        }
     
        public void BackupElements(IList<T> CurrentEmergencyBlock)
        {           
            MemoryStream buffer = new MemoryStream();
            foreach (T request in CurrentEmergencyBlock)
            {
                serializer.Serialize(buffer, request);                
            }
            buffer.Seek(0, SeekOrigin.Begin);

            try
            {
                String NeedsSeparator = DumpFolder.EndsWith(@"/") ? "" : Path.AltDirectorySeparatorChar.ToString();
                String FileName = null;
                while (FileName == null)// If the file exists, rename the file.
                {
                    FileName = String.Format("{0}QueueDump_{1:yyyy-MM-dd_HHmmss.fffffff}_{2}", DumpFolder, DateTime.Now, DumpedFilesExtension);
                    if (File.Exists(FileName))
                        FileName = null;        
                }

                log.InfoFormat("Creating a new dump file: {0}", FileName);
                //Write requests to file
                using (FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write))
                {                   
                    using (StreamWriter streamWriter = new StreamWriter(fs))
                    {
                        buffer.WriteTo(streamWriter.BaseStream);
                        streamWriter.Flush();
                    }
                }
                log.InfoFormat("Dump file created");
                buffer = null;
            }
            catch (Exception ex)
            {
                log.Error("Error writing elements to the disk on backup action!");
                throw new Exception("Error writing elements to the disk on backup action!", ex);
            }
        }

        public IList<T> RecoverElements()
        {
            IList<T> elementsRecovered = null;
            String[] files = Directory.GetFiles(DumpFolder, "*" + DumpedFilesExtension);
            string file = files.FirstOrDefault();
            if (!string.IsNullOrEmpty(file))
            {
                using (FileStream fileStream = File.OpenRead(file))
                {                    
                   elementsRecovered =  (IList<T>) serializer.Deserialize(fileStream);
                }
                File.Delete(file);
            }
            return (elementsRecovered);
        }

        
        #endregion
    }
}
