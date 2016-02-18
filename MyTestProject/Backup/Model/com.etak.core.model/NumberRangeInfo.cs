using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class NumberRangeInfo
    {
        #region 构造函数
        public NumberRangeInfo()
        { }

        public NumberRangeInfo(long LocationID, string Range1, string Range2, string NDC, int? OperatorID,
            string NumberTypeID, DateTime? StartDate)
        {
            this._LocationID = LocationID;
            this._Range1 = Range1;
            this._Range2 = Range2;
            this._NDC = NDC;
            this._OperatorID = OperatorID;
            this._NumberTypeID = NumberTypeID;
            this._StartDate = StartDate;
        }
        #endregion

        #region 成员

        private long _LocationID;
        private string _Range1;
        private string _Range2;
        private string _NDC;
        private int? _OperatorID;
        private string _NumberTypeID;
        private DateTime? _StartDate;
        #endregion


        #region 属性
        public virtual long LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        public virtual string Range1
        {
            get { return _Range1; }
            set { _Range1 = value; }
        }
        public virtual string Range2
        {
            get { return _Range2; }
            set { _Range2 = value; }
        }
        public virtual string NDC
        {
            get { return _NDC; }
            set { _NDC = value; }
        }
        public virtual int? OperatorID
        {
            get { return _OperatorID; }
            set { _OperatorID = value; }
        }
        public virtual string NumberTypeID
        {
            get { return _NumberTypeID; }
            set { _NumberTypeID = value; }
        }
        public virtual DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        #endregion
    }
}
