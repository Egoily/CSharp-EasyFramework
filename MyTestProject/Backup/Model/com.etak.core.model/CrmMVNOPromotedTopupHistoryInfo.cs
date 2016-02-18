using System;

namespace com.etak.core.model
{
    [Serializable]
    public class CrmMVNOPromotedTopupHistoryInfo
    {
        #region 构造函数
        public CrmMVNOPromotedTopupHistoryInfo()
        { }

        public CrmMVNOPromotedTopupHistoryInfo(int MVNOID, int CUSTOMERID, long PROMOTIONID, string MSISDN, string ORDERCODE, decimal BEFORECREDIT, decimal AFTERCREDIT, DateTime? BEFOREENDDATE, DateTime? AFTERENDDATE, DateTime OPERATIONDATE)
        {
            this._MVNOID = MVNOID;
            this._CUSTOMERID = CUSTOMERID;
            this._PROMOTIONID = PROMOTIONID;
            this._MSISDN = MSISDN;
            this._ORDERCODE = ORDERCODE;
            this._BEFORECREDIT = BEFORECREDIT;
            this._AFTERCREDIT = AFTERCREDIT;
            this._BEFOREENDDATE = BEFOREENDDATE;
            this._AFTERENDDATE = AFTERENDDATE;
            this._OPERATIONDATE = OPERATIONDATE;
        }
        #endregion

        #region 成员
        private long _LogID;
        private int _MVNOID;
        private int _CUSTOMERID;
        private long _PROMOTIONID;
        private string _MSISDN;
        private string _ORDERCODE;
        private decimal _BEFORECREDIT;
        private decimal _AFTERCREDIT;
        private DateTime? _BEFOREENDDATE;
        private DateTime? _AFTERENDDATE;
        private DateTime _OPERATIONDATE;
        #endregion


        public override bool Equals(object obj)
        {
            CrmMVNOPromotedTopupHistoryInfo value = obj as CrmMVNOPromotedTopupHistoryInfo;
            if (value != null && value.LogID == this.LogID)
            {
                return true;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        #region 属性
        public long LogID
        {
            get { return this._LogID; }
            set { this._LogID = value; }
        }

        public int MVNOID
        {
            get { return this._MVNOID; }
            set { this._MVNOID = value; }
        }

        public int CustomerId
        {
            get { return _CUSTOMERID; }
            set { _CUSTOMERID = value; }
        }

        public long PromotionId
        {
            get { return this._PROMOTIONID; }
            set { this._PROMOTIONID = value; }
        }

        public string MSISDN
        {
            get { return this._MSISDN; }
            set { this._MSISDN = value; }
        }

        public string OrderCode
        {
            get { return this._ORDERCODE; }
            set { this._ORDERCODE = value; }
        }

        public decimal BeforeCredit
        {
            get { return this._BEFORECREDIT; }
            set { this._BEFORECREDIT = value; }
        }

        public decimal AfterCredit
        {
            get { return this._AFTERCREDIT; }
            set { this._AFTERCREDIT = value; }
        }

        public DateTime? BeforeEndDate
        {
            get { return this._BEFOREENDDATE; }
            set { this._BEFOREENDDATE = value; }
        }

        public DateTime? AfterEndDate
        {
            get { return this._AFTERENDDATE; }
            set { this._AFTERENDDATE = value; }
        }

        public DateTime OperationDate
        {
            get { return this._OPERATIONDATE; }
            set { this._OPERATIONDATE = value; }
        }
        #endregion
    }
}
