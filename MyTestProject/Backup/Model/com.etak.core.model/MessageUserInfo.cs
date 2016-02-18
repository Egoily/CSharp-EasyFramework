using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MessageUserInfo
    {
        #region 成员
        private int _ID;
        private int _UserID;
        private int _MessageID;
        private int _Status;
        private DateTime? _SendDate;
        #endregion


        #region 属性
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public int MessageID
        {
            get { return _MessageID; }
            set { _MessageID = value; }
        }

        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public DateTime? SendDate
        {
            get { return _SendDate; }
            set { _SendDate = value; }
        }

        #endregion
    }
}
