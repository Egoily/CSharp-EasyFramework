using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class TTHistoryInfo
    {
        public TTHistoryInfo() { }

        public TTHistoryInfo(int id,string ticketNumber,int userid,string comments,DateTime updateTime,int commentType,string userName)
        {
            _ID = id;
            _TICKETNUMBER = ticketNumber;
            _USERID = userid;
            _COMMENTS = comments;
            _UPDATETIME = updateTime;
            _COMMENTTYPE = commentType;
            _UserName = userName;
        }

        #region 
        private int? _ID = null;
        private string _TICKETNUMBER;
        private int? _USERID = null;
        private string _COMMENTS;
        private DateTime? _UPDATETIME = null;
        private int? _COMMENTTYPE = null;
        private string _UserName;
        #endregion

        public int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string TICKETNUMBER
        {
            get { return _TICKETNUMBER; }
            set { _TICKETNUMBER = value; }
        }
        public int? USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }
        #region Attribute
        
        public string COMMENTS
        {
            get { return _COMMENTS; }
            set { _COMMENTS = value; }
        }   
        
        public DateTime? UPDATETIME
        {
            get { return _UPDATETIME; }
            set { _UPDATETIME = value; }
        }

        public int? COMMENTTYPE
        {
            get { return _COMMENTTYPE; }
            set { _COMMENTTYPE = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        #endregion

    }
}
