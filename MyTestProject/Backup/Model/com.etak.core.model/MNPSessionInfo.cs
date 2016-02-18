using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MNPSessionInfo
    {
        private int Id;
        private string operatorCode;
        private DateTime createTime;
        private string sessionId;
        private string userName;

        public MNPSessionInfo()
        {

        }

        public MNPSessionInfo(int Id,string operatorCode,DateTime createTime, string sessionId)
        {
            this.Id = Id;
            this.operatorCode = operatorCode;
            this.sessionId = sessionId;
            this.createTime = createTime;
        }

        public virtual int ID
        {
            get { return Id; }
            set { Id = value; }
        }


        public virtual string OperatorCode
        {
            get { return operatorCode; }
            set { operatorCode = value; }
        }

        public virtual DateTime CreateTime
        {

            get { return createTime; }
            set { createTime = value; }
        }
        

        public virtual string SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }

        public virtual string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}
