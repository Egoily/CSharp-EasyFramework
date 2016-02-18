using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class EmailAttachmentInfo
    {
        #region 构造函数
        public EmailAttachmentInfo()
        { }

        public EmailAttachmentInfo(int attachmentId, EmailInfo emailInfo, string remark, string fileName, string filePath)
        {
            this.attachmentId = attachmentId;
            this.emailInfo = emailInfo;
            this.remark = remark;
            this.fileName = fileName;
            this.filePath = filePath;
        }
        #endregion

        #region 成员
        private Int64 attachmentId;
        protected EmailInfo emailInfo;
        private string remark;
        private string fileName;
        private string filePath;
        private byte[] fileContent;
              
        #endregion


        #region 属性
        public virtual Int64 AttachmentId
        {
            get { return attachmentId; }
            set { attachmentId = value; }
        }

        public virtual EmailInfo EmailInfo
        {
            get { return emailInfo; }
            set { emailInfo = value; }
        }

        public virtual string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public virtual string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public virtual string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public virtual byte[] FileContent
        {
            get { return fileContent; }
            set { fileContent = value; }
        }
        #endregion
    }
}
