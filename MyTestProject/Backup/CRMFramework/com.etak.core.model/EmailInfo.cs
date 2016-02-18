using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class EmailInfo
    {
       

        #region 成员
        private long? emailId;
        private int? dealerId;
        private int? categoryId;        
        private string sender;
        private string emailFrom;
        private string emailTo;
        private string cc;
        private string bcc;
        private string emailSubject;
        private string subjectEncoding;
        private string body;
        private string bodyEncoding;
        private bool isBodyHtml = false;
        private bool searchByIsBodyHtml = false;
        private string replyTo;
        private int? priority;
        private string remark;
        private DateTime? createDate;
        private int? statusId;
        private DateTime? sentDate;
        private int? userId;
        private string _userName;
        private string _userPassword;
        private string _smtpHost;
        protected IList<EmailAttachmentInfo> emailAttachmentInfos;
        private int? _trySentTimes = 0;
        private int? _maxSentTimes;

        #region added by liny,2010-11-24

        private int? _customerId;
        public virtual int? CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        #endregion

        public virtual long? EmailId
        {
            get { return emailId; }
            set { emailId = value; }
        }
        public virtual int? DealerId
        {
            get { return dealerId; }
            set { dealerId = value; }
        }
        public virtual int? CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }
        public virtual string Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        public virtual string EmailFrom
        {
            get { return emailFrom; }
            set { emailFrom = value; }
        }
        public virtual string EmailTo
        {
            get { return emailTo; }
            set { emailTo = value; }
        }
        public virtual string CC
        {
            get { return cc; }
            set { cc = value; }
        }
        public virtual string Bcc
        {
            get { return bcc; }
            set { bcc = value; }
        }
        public virtual string EmailSubject
        {
            get { return emailSubject; }
            set { emailSubject = value; }
        }
        public virtual string SubjectEncoding
        {
            get { return subjectEncoding; }
            set { subjectEncoding = value; }
        }
        public virtual string Body
        {
            get { return body; }
            set { body = value; }
        }
        public virtual string BodyEncoding
        {
            get { return bodyEncoding; }
            set { bodyEncoding = value; }
        }
        public virtual bool IsBodyHtml
        {
            get { return isBodyHtml; }
            set { isBodyHtml = value; }
        }
        public virtual bool SearchByIsBodyHtml
        {
            get { return searchByIsBodyHtml; }
            set { searchByIsBodyHtml = value; }
        }
        public virtual string ReplyTo
        {
            get { return replyTo; }
            set { replyTo = value; }
        }
        public virtual int? Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        public virtual string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        public virtual DateTime? CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        public virtual int? StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }
        public virtual DateTime? SentDate
        {
            get { return sentDate; }
            set { sentDate = value; }
        }
        public virtual int? UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public virtual IList<EmailAttachmentInfo> EmailAttachmentInfos
        {
            get { return emailAttachmentInfos; }
            set { emailAttachmentInfos = value; }
        }

        public virtual string UserName
        {
            get { return _userName; }
            set { this._userName = value; }
        }

        public virtual string UserPassword
        {
            get { return _userPassword; }
            set { this._userPassword = value; }
        }

        public virtual string SmtpHost
        {
            get { return _smtpHost; }
            set { this._smtpHost = value; }
        }

        public virtual int? TrySentTimes
        {
            get { return _trySentTimes; }
            set { _trySentTimes = value; }
        }
        public virtual int? MaxSentTimes
        {
            get { return _maxSentTimes; }
            set { _maxSentTimes = value; }
        }
        #endregion
    }
}
