using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009/8/18 AM 10:12:23
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009/8/18 AM 10:12:23
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009/8/18 AM 10:12:23
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009/8/18 AM 10:12:23
    /// </summary>
    [DataContract]
    [Serializable]
    public class HistoryInfo
    {
        #region 构造函数
        public HistoryInfo()
        { }

        public HistoryInfo(long? ID, int? dealerID, int? operationCode, string refrenceCode, string msisdn, int? topupType, string pin, int? status,
            DateTime? statusDate, decimal? amount, int? currencyID, int? taxCode, int? preOperationCode, decimal? preBalance,
            string operationCause, string remark, bool used, DateTime? usedTime,
            string paymentCardNumber, string expirationDate, decimal? amountWithTax, int? userID, int? customerID,
            string bonusConfigIds, string bonusConfigName, decimal? topupAmount, decimal? bonusAmount, string bonusPromotionIds, string bonusPromotionName,int topupAccount,
            decimal? bonusAmountWithTAX, decimal? normalAmount, decimal? normalAmountWithTAX, int? validityDays)
        {
            this._ID = ID;
            this._DealerID = dealerID;
            this._OperationCode = operationCode;
            this._RefrenceCode = refrenceCode;
            this._Msisdn = msisdn;
            this._TopupType = topupType;
            this._Pin = pin;
            this._Status = status;
            this._StatusDate = statusDate;
            this._Amount = amount;
            this._CurrencyID = currencyID;
            this._TaxCode = taxCode;
            this._PreOperationCode = preOperationCode;
            this._PreBalance = preBalance;
            this._OperationCause = operationCause;
            this._Reamrk = remark;
            this._Used = used;
            this._UsedTime = usedTime;
            this._PaymentCardNumber = paymentCardNumber;
            this._ExpirationDate = expirationDate;
            this._AmountWithTax = amountWithTax;
            this._UserID = userID;
            this._CustomerID = customerID;
            this._bonusConfigIds = bonusConfigIds;
            this._bonusConfigName = bonusConfigName;
            this._topupAmount = topupAmount;
            this._bonusAmount = bonusAmount;
            this._TopupAccount = topupAccount;
            this._bonusPromotionIds = bonusPromotionIds;
            this._bonusPromotionName = bonusPromotionName;
            this._topupAmount = topupAccount;
            this._bonusAmountWithTAX = bonusAmountWithTAX;
            this._normalAmount = normalAmount;
            this._normalAmountWithTAX = normalAmountWithTAX;
            this._validityDays = validityDays;
        }
        #endregion

        #region 成员
        private long? _ID;
        private int? _DealerID;
        private long? _OperationCode;
        private string _RefrenceCode;
        private string _Msisdn;
        private int? _TopupType;
        private string _Pin;
        private int? _Status;
        private DateTime? _StatusDate;
        private decimal? _Amount;
        private int? _CurrencyID;
        private int? _TaxCode;
        private long? _PreOperationCode;
        private decimal? _PreBalance;
        private string _OperationCause;
        private string _Reamrk;
        private bool _Used;
        private DateTime? _UsedTime;
        private string _PaymentCardNumber;
        private string _ExpirationDate;
        //added by neil at 2014/07/02
        private DateTime? _ExpirationDate2;
        private decimal? _AmountWithTax;
        private int? _UserID;
        private string _UserName;

        private string _bonusConfigIds;
        private string _bonusConfigName;
        private decimal? _topupAmount;
        private decimal? _bonusAmount;
        private string _bonusPromotionIds;
        private string _bonusPromotionName;

        private int? _CustomerID;
        private string _VcEncrypt;

        private int? _TopupAccount;
        private decimal? _bonusAmountWithTAX;
        private decimal? _normalAmount;
        private decimal? _normalAmountWithTAX;
        private decimal? _topupBonusVAT;

        private decimal? _topupBouns;
        private decimal? _bonusForNormal;
        private decimal? _bonusForNormalVAT;
        private decimal? _bonusForPromoted;
        private decimal? _bonusForPromotedVAT;
        //add by damon 2013-09-16
        private int? _adjustmentCode;
        private int? _validityDays;//add by figo
        #endregion


        #region 属性
        public virtual long? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public virtual long? OperationCode
        {
            get { return _OperationCode; }
            set { _OperationCode = value; }
        }

        public virtual string RefrenceCode
        {
            get { return _RefrenceCode; }
            set { _RefrenceCode = value; }
        }

        public virtual string Msisdn
        {
            get { return _Msisdn; }
            set { _Msisdn = value; }
        }

        public virtual int? TopupType
        {
            get { return _TopupType; }
            set { _TopupType = value; }
        }

        public virtual string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }

        public virtual int? Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public virtual DateTime? StatusDate
        {
            get { return _StatusDate; }
            set { _StatusDate = value; }
        }

        public virtual decimal? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public virtual int? CurrencyID
        {
            get { return _CurrencyID; }
            set { _CurrencyID = value; }
        }

        public virtual int? TaxCode
        {
            get { return _TaxCode; }
            set { _TaxCode = value; }
        }

        public virtual long? PreOperationCode
        {
            get { return _PreOperationCode; }
            set { _PreOperationCode = value; }
        }

        public virtual decimal? PreBalance
        {
            get { return _PreBalance; }
            set { _PreBalance = value; }
        }

        public virtual string OperationCause
        {
            get { return _OperationCause; }
            set { _OperationCause = value; }
        }

        public virtual string Reamrk
        {
            get { return _Reamrk; }
            set { _Reamrk = value; }
        }

        public virtual bool Used
        {
            get { return _Used; }
            set { _Used = value; }
        }

        public virtual DateTime? UsedTime
        {
            get { return _UsedTime; }
            set { _UsedTime = value; }
        }

        public virtual string PaymentCardNumber
        {
            get { return _PaymentCardNumber; }
            set { _PaymentCardNumber = value; }
        }

        public virtual string ExpirationDate
        {
            get { return _ExpirationDate; }
            set { _ExpirationDate = value; }
        }
        public virtual DateTime? ExpirationDate2
        {
            get { return _ExpirationDate2; }
            set { _ExpirationDate2 = value; }
        }
        public virtual decimal? AmountWithTax
        {
            get { return _AmountWithTax; }
            set { _AmountWithTax = value; }
        }

        public virtual int? UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }


        public virtual string VcEncrypt
        {
            get { return _VcEncrypt; }
            set { _VcEncrypt = value; }
        }

        public virtual int? CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        public virtual string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public virtual string BonusConfigIds
        {
            get { return _bonusConfigIds; }
            set { _bonusConfigIds = value; }
        }
        public virtual string BonusConfigName
        {
            get { return _bonusConfigName; }
            set { _bonusConfigName = value; }
        }
        public virtual decimal? TopupAmount
        {
            get { return _topupAmount ; }
            set { _topupAmount = value; }
        }
        //<!--根据充值规则送的promoted金额 （不含税）-->
        public virtual decimal? BonusAmount
        {
            get { return _bonusAmount ; }
            set { _bonusAmount = value; }
        }
        //<!--根据充值规则送的promoted金额 （含税）-->
        public virtual decimal? BonusAmountWithTAX
        {
            get { return _bonusAmountWithTAX; }
            set { _bonusAmountWithTAX = value; }
        }
        //<!--根据充值规则送的 Normal 金额 （含税）-->
        public virtual decimal? NormalAmount
        {
            get { return _normalAmount; }
            set { _normalAmount = value; }
        }
        //<!--根据充值规则送的 Normal 金额 （含税）-->
        public virtual decimal? NormalAmountWithTAX
        {
            get { return _normalAmountWithTAX; }
            set { _normalAmountWithTAX = value; }
        }
        public virtual string BonusPromotionIds
        {
            get { return _bonusPromotionIds; }
            set { _bonusPromotionIds = value; }
        }
        public virtual string BonusPromotionName
        {
            get { return _bonusPromotionName; }
            set { _bonusPromotionName = value; }
        }

        public virtual decimal? TopupBonusVAT
        {
            get { return _topupBonusVAT; }
            set { _topupBonusVAT = value; }
        }

        public virtual decimal? TopupBonus
        {
            get { return _topupBouns; }
            set { _topupBouns = value; }
        }

        public virtual decimal? BonusForNormal
        {
            get { return _bonusForNormal; }
            set { _bonusForNormal = value; }
        }

        public virtual decimal? BonusForNormalVAT
        {
            get { return _bonusForNormalVAT; }
            set { _bonusForNormalVAT = value; }
        }

        public virtual decimal? BonusForPromoted
        {
            get { return _bonusForPromoted; }
            set { _bonusForPromoted = value; }
        }

        public virtual decimal? BonusForPromotedVAT
        {
            get { return _bonusForPromotedVAT; }
            set { _bonusForPromotedVAT = value; }
        }
        /// <summary>
        /// 0 top up to normal account
        /// 1 top up to promoted account
        /// </summary>
        public virtual int? TopupAccount
        {
            get { return _TopupAccount; }
            set { _TopupAccount = value; }
        }

        //add by damon 2013-09-16
        public virtual int? AdjustmentCode
        {
            get { return _adjustmentCode; }
            set { _adjustmentCode = value; }
        }

        public virtual int? ValidityDays
        {
            get { return _validityDays; }
            set { _validityDays = value; }
        }
        
        #endregion

    }
}
