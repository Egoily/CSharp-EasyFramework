using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MessageInfo
    {
        #region 成员
        private int _MessageID;
        private string _MessageText;
        private DateTime _Createdate;
        private DateTime _StartDate;
        private int _ExpireMinutes;
        private string _UserName;
        private int _UserID;
        
        #endregion
        
        
        #region 属性
        public int MessageID
        {
            get { return _MessageID; }
            set { _MessageID = value; }
        }
        public string MessageText
        {
            get { return _MessageText; }
            set { _MessageText = value; }
        }
        
        public DateTime Createdate
        {
            get { return _Createdate; }
            set { _Createdate = value; }
        }
        
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        
        public int ExpireMinutes
        {
            get { return _ExpireMinutes; }
            set { _ExpireMinutes = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public virtual int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        #endregion
    }
}
