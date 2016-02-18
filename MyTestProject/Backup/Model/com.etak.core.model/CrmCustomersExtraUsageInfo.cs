using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2010-5-21 16:53:57
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2010-5-21 16:53:57
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2010-5-21 16:53:57
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2010-5-21 16:53:57
    /// </summary>
    [DataContract]
    [Serializable]
    public class CrmCustomersExtraUsageInfo
    {
        #region 构造函数
        public CrmCustomersExtraUsageInfo()
        { }

        public CrmCustomersExtraUsageInfo(long ID, int DEALERID, int CUSTOMERID, int TYPEID, decimal AMOUNT, int CURRENCYID, int TAXCODE, decimal AMOUNTWITHTAX, bool ISBILLED, DateTime? BILLEDTIME, DateTime? CREATEDATE, int USERID, int PROMOTIONPLANID, int PAYMENTTYPE, int STATUS, string DESCRIPTION, string BAK1, string BAK2, string BAK3)
        {
            this._ID = ID;
            this._DEALERID = DEALERID;
            this._CUSTOMERID = CUSTOMERID;
            this._TYPEID = TYPEID;
            this._AMOUNT = AMOUNT;
            this._CURRENCYID = CURRENCYID;
            this._TAXCODE = TAXCODE;
            this._AMOUNTWITHTAX = AMOUNTWITHTAX;
            this._ISBILLED = ISBILLED;
            this._BILLEDTIME = BILLEDTIME;
            this._CREATEDATE = CREATEDATE;
            this._USERID = USERID;
            this._PROMOTIONPLANID = PROMOTIONPLANID;
            this._PAYMENTTYPE = PAYMENTTYPE;
            this._STATUS = STATUS;
            this._DESCRIPTION = DESCRIPTION;
            this._BAK1 = BAK1;
            this._BAK2 = BAK2;
            this._BAK3 = BAK3;
        }
        #endregion

        #region 成员
        private long _ID;
        private int _DEALERID;
        private int _CUSTOMERID;
        private int _TYPEID;
        private decimal _AMOUNT;
        private int _CURRENCYID;
        private int _TAXCODE;
        private decimal _AMOUNTWITHTAX;
        private bool _ISBILLED;
        private DateTime? _BILLEDTIME;
        private DateTime? _CREATEDATE;
        private int _USERID;
        private int _PROMOTIONPLANID;
        private int _PAYMENTTYPE;
        private int _STATUS;
        private string _DESCRIPTION;
        private string _BAK1;
        private string _BAK2;
        private string _BAK3;
        #endregion


        #region 属性
        public long ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int DealerId
        {
            get { return _DEALERID; }
            set { _DEALERID = value; }
        }

        public int CustomerId
        {
            get { return _CUSTOMERID; }
            set { _CUSTOMERID = value; }
        }

        public int TypeId
        {
            get { return _TYPEID; }
            set { _TYPEID = value; }
        }

        public decimal Amount
        {
            get { return _AMOUNT; }
            set { _AMOUNT = value; }
        }

        public int CurrencyId
        {
            get { return _CURRENCYID; }
            set { _CURRENCYID = value; }
        }

        public int TaxCode
        {
            get { return _TAXCODE; }
            set { _TAXCODE = value; }
        }

        public decimal AmountWithTax
        {
            get { return _AMOUNTWITHTAX; }
            set { _AMOUNTWITHTAX = value; }
        }

        public bool IsBilled
        {
            get { return _ISBILLED; }
            set { _ISBILLED = value; }
        }

        public DateTime? BilledTime
        {
            get { return _BILLEDTIME; }
            set { _BILLEDTIME = value; }
        }

        public DateTime? CreateDate
        {
            get { return _CREATEDATE; }
            set { _CREATEDATE = value; }
        }

        public int UserId
        {
            get { return _USERID; }
            set { _USERID = value; }
        }

        public int PromotionPlanId
        {
            get { return _PROMOTIONPLANID; }
            set { _PROMOTIONPLANID = value; }
        }

        public int PaymentType
        {
            get { return _PAYMENTTYPE; }
            set { _PAYMENTTYPE = value; }
        }

        public int Status
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public string Description
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }

        public string Bak1
        {
            get { return _BAK1; }
            set { _BAK1 = value; }
        }

        public string Bak2
        {
            get { return _BAK2; }
            set { _BAK2 = value; }
        }

        public string Bak3
        {
            get { return _BAK3; }
            set { _BAK3 = value; }
        }

        #endregion

    }
}
