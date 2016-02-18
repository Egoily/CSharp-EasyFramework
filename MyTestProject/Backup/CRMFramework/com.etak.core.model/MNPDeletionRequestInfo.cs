using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MNPDeletionRequestInfo
    {
        #region 构造函数
        public MNPDeletionRequestInfo()
        { }

        public MNPDeletionRequestInfo(int requestId, string msisdn, DateTime requestDate, bool isSent, DateTime? sendDate, int userId)
        {
            this.requestId = requestId;
            this.msisdn = msisdn;            
            this.requestDate = requestDate;
            this.isSent = isSent;
            this.sendDate = sendDate;
            this.userId = userId;
        }
        #endregion

        #region 成员
        private int requestId;
        private string msisdn;
        private bool isSent;
        private DateTime requestDate;
        private DateTime? sendDate = null;
        private int userId;

        #endregion

        #region 属性
        public virtual int RequestId
        {
            get { return requestId; }
            set { requestId = value; }
        }

        public virtual string MSISDN
        {
            get { return msisdn; }
            set { msisdn = value; }
        }        

        public virtual DateTime RequestDate
        {
            get { return requestDate; }
            set { requestDate = value; }
        }

        public virtual bool IsSent
        {
            get { return isSent; }
            set { isSent = value; }
        }

        public virtual DateTime? SendDate
        {
            get { return sendDate; }
            set { sendDate = value; }
        }

        public virtual int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        #endregion
    }
}
