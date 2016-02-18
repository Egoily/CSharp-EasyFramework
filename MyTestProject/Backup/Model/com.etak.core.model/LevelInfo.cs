using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class LevelInfo
    {

        #region Member
        private int? _ID = null;
        private int? _DealerID = null;
        private string _Business;
        private int? _ParentID = null;
        private string _Operation;
        private int? _OperationLevel = null;
        private int? _RecordLocation = null;
        private int? _CreateUserID = null;
        private DateTime? _CreateDate = null;
        private int? _Status = null;
        private DateTime? _UpdateDate = null;
        private int? _UpdateUserID = null;
        private string _ClassName;
        #endregion


        #region attribute
        public int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public string Business
        {
            get { return _Business; }
            set { _Business = value; }
        }
        public int? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        public string Operation
        {
            get { return _Operation; }
            set { _Operation = value; }
        }

        public int? OperationLevel
        {
            get { return _OperationLevel; }
            set { _OperationLevel = value; }
        }

        public int? RecordLocation
        {
            get { return _RecordLocation; }
            set { _RecordLocation = value; }
        }

        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public int? CreateUserID
        {
            get { return _CreateUserID; }
            set { _CreateUserID = value; }
        }
        public int? Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        public int? UpdateUserID
        {
            get { return _UpdateUserID; }
            set { _UpdateUserID = value; }
        }

        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }
        #endregion

    }
}