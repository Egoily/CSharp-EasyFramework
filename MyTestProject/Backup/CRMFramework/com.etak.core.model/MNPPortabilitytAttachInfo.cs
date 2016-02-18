using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MNPPortabilitytAttachInfo
    {
        private string referenceCode;
        private string attachmentName;
        private byte[] attachStream;
        private DateTime? saveDate;
        private int? status;

        public MNPPortabilitytAttachInfo()
        {

        }

        public MNPPortabilitytAttachInfo(string referenceCode, string attachName, byte[] attachStream, DateTime? saveDate, int? status)
        {
            this.referenceCode = referenceCode;
            this.attachmentName = attachName;
            this.attachStream = attachStream;
            this.saveDate = saveDate;
            this.status = status;

        }

        public virtual string ReferenceCode
        {
            get { return referenceCode; }
            set { referenceCode = value; }
        }

        public virtual string AttachmentName
        {
            get { return attachmentName; }
            set { attachmentName = value; }
        }

        public virtual byte[] AttachStream
        {
            get { return attachStream; }
            set { attachStream = value; }
        }

        public virtual DateTime? SaveDate
        {
            get { return saveDate; }
            set { saveDate = value; }
        }

        public virtual int? Status
        {
            get { return status; }
            set { status = value; }
        }


    }
}
