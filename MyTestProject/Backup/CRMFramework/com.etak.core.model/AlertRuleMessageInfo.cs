using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class AlertRuleMessageInfo
    {
        private int _MessageID;

        public int MessageID
        {
            get { return _MessageID; }
            set { _MessageID = value; }
        }
        private int _MNVOID;

        public int MNVOID
        {
            get { return _MNVOID; }
            set { _MNVOID = value; }
        }
        private string _MNVOName;

        public string MNVOName
        {
            get { return _MNVOName; }
            set { _MNVOName = value; }
        }
        private string _AlertRuleName;

        public string AlertRuleName
        {
            get { return _AlertRuleName; }
            set { _AlertRuleName = value; }
        }
        private string _MatterName;

        public string MatterName
        {
            get { return _MatterName; }
            set { _MatterName = value; }
        }
        private int _MessageType;

        public int MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; }
        }
        private int _ISSend;

        public int ISSend
        {
            get { return _ISSend; }
            set { _ISSend = value; }
        }
        private string _UNIT;

        public string UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
        private string _UNITHours;

        public string UNITHours
        {
            get { return _UNITHours; }
            set { _UNITHours = value; }
        }
        private string _UNITDate;

        public string UNITDate
        {
            get { return _UNITDate; }
            set { _UNITDate = value; }
        }
        private string _Sends;

        public string Sends
        {
            get { return _Sends; }
            set { _Sends = value; }
        }
        private string _Receivers;

        public string Receivers
        {
            get { return _Receivers; }
            set { _Receivers = value; }
        }
        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _CreateUser;

        public string CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        private DateTime _CreateDate;

        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
        private string _UpdateUser;

        public string UpdateUser
        {
            get { return _UpdateUser; }
            set { _UpdateUser = value; }
        }
        private DateTime _UpdateDate;

        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        private int _UserID;

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
    }
}
