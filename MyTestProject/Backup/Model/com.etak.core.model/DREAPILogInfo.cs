using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class DREAPILogInfo
    {
       

        #region 成员
        private long _operationID;
        private string _requestId = string.Empty;
        private string _description = string.Empty;
        private string _remark = string.Empty;
        private int? _resultTypeID = (int)ResultType.Correct;
        private string _shortCode = string.Empty;
        private string _userMessages = string.Empty;
        private string _systemMessages = string.Empty;
        private string _name1;
        private string _name2;
        private string _name3;
        private string _name4;
        private string _name5;
        private string _name6;
        private string _name7;
        private string _name8;
        private string _name9;
        private string _name10;
        private string _value1;
        private string _value2;
        private string _value3;
        private string _value4;
        private string _value5;
        private string _value6;
        private string _value7;
        private string _value8;
        private string _value9;
        private string _value10;
        private int? _userID = -1;
        private int? _dealerID;
        private DateTime? _operationDate = DateTime.Now;

        #endregion

        #region 属性

        public virtual long OperationID
        {
            get { return _operationID; }
            set { _operationID = value; }
        }

        public virtual string RequestID
        {
            get { return _requestId; }
            set { _requestId = value; }
        }

        public virtual int? UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public virtual int? DealerID
        {
            get { return _dealerID; }
            set { _dealerID = value; }
        }

        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        
        public virtual string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        public virtual DateTime? OperationDate
        {
            get { return _operationDate; }
            set { _operationDate = value; }
        }

        public virtual int? ResultTypeID
        {
            get { return _resultTypeID; }
            set { _resultTypeID = value; }
        }

        public virtual string ShortCode
        {
            get { return _shortCode; }
            set { _shortCode = value; }
        }

        public virtual string UserMessages
        {
            get { return _userMessages; }
            set { _userMessages = value; }
        }

        public virtual string SystemMessages
        {
            get { return _systemMessages; }
            set { _systemMessages = value; }
        }

        public virtual string Name1
        {
            get { return _name1; }
            set { _name1 = value; }
        }

        public virtual string Name2
        {
            get { return _name2; }
            set { _name2 = value; }
        }

        public virtual string Name3
        {
            get { return _name3; }
            set { _name3 = value; }
        }

        public virtual string Name4
        {
            get { return _name4; }
            set { _name4 = value; }
        }

        public virtual string Name5
        {
            get { return _name5; }
            set { _name5 = value; }
        }

        public virtual string Name6
        {
            get { return _name6; }
            set { _name6 = value; }
        }

        public virtual string Name7
        {
            get { return _name7; }
            set { _name7 = value; }
        }

        public virtual string Name8
        {
            get { return _name8; }
            set { _name8 = value; }
        }

        public virtual string Name9
        {
            get { return _name9; }
            set { _name9 = value; }
        }

        public virtual string Name10
        {
            get { return _name10; }
            set { _name10 = value; }
        }

        public virtual string Value1
        {
            get { return _value1; }
            set { _value1 = value; }
        }

        public virtual string Value2
        {
            get { return _value2; }
            set { _value2 = value; }
        }

        public virtual string Value3
        {
            get { return _value3; }
            set { _value3 = value; }
        }

        public virtual string Value4
        {
            get { return _value4; }
            set { _value4 = value; }
        }

        public virtual string Value5
        {
            get { return _value5; }
            set { _value5 = value; }
        }

        public virtual string Value6
        {
            get { return _value6; }
            set { _value6 = value; }
        }

        public virtual string Value7
        {
            get { return _value7; }
            set { _value7 = value; }
        }

        public virtual string Value8
        {
            get { return _value8; }
            set { _value8 = value; }
        }

        public virtual string Value9
        {
            get { return _value9; }
            set { _value9 = value; }
        }

        public virtual string Value10
        {
            get { return _value10; }
            set { _value10 = value; }
        }

        #endregion

    }
}
