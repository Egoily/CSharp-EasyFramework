using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class NumberBatchInfo
    {
        #region 构造函数
        public NumberBatchInfo()
        { }

        public NumberBatchInfo(int? BatchID, string SNStart, string SNEnd, int? Count, int? ShareType, string DealerID, DateTime? CreateDate, int? UserID)
        {
            this._BatchID = BatchID;
            this._SNStart = SNStart;
            this._SNEnd = SNEnd;
            this._Count = Count;
            this._ShareType = ShareType;
            this._DealerID = DealerID;
            this._CreateDate = CreateDate;
            this._UserID = UserID;
        }
        #endregion

        #region 成员
        private int? _BatchID;
        private string _SNStart;
        private string _SNEnd;
        private int? _Count;
        private int? _ShareType;
        private string _DealerID;
        private DateTime? _CreateDate;
        private int? _UserID;
        private string _UserName;

       
        #endregion


        #region 属性
        public virtual int? BatchID
        {
            get { return _BatchID; }
            set { _BatchID = value; }
        }

        public virtual string SNStart
        {
            get { return _SNStart; }
            set { _SNStart = value; }
        }

        public virtual string SNEnd
        {
            get { return _SNEnd; }
            set { _SNEnd = value; }
        }

        public virtual int? Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        public virtual int? ShareType
        {
            get { return _ShareType; }
            set { _ShareType = value; }
        }

        public virtual string DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public virtual DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public virtual int? UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public virtual string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        #endregion

    }
}
