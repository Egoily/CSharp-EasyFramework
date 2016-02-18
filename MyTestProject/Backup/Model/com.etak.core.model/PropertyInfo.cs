using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class PropertyInfo
    {
        private CustomerInfo _CustomerInfo;

        private int? _CustomerID = null;
        private int? _PropertyID = null;
        private int? _CustomerTypeID = null;
        private int? _LanguageID = null;
        private int? _PaymentMethodID = null;
        private int? _BillingMethodID = null;
        private bool _ParentBilling;
        private int? _TrafficTypeID = null;
        private int? _TaxPlanID = null;
        private bool _InvoiceDetails;
        private string _Email;
        private int? _InvoiceDueDate = null;
        private int? _WithDrawPeriod = null;
        private string _UserName;
        private string _PasswordDES;
        private string _PasswordMD5;
        private DateTime? _Birthday = null;
        private DateTime? _CreateDate = null;
        private int? _UserID = null;
        private int? _IDType = null;
        private string _IDNumber;
        private int? _CreditScore = null;
        private decimal? _OriginalDepositAmount = null;
        private decimal? _CurrentDepositAmount = null;
        private DateTime? _DepositDate = null;
        private int? _PendingStatus = null;
        private int? _LoyaltyPoint = null;
        private bool _AcceptNews;
        private DateTime? _LastLoyaltyDate = null;
        private bool _FF;
        private int? _BillingScenarioID = null;
        private int? _ContractPeriod = null;
        private int? _CreditTransferType = null;
        private string _VATNO;
        private int? _DepositStatus = null;
        private decimal? _CurrentDepositCredit = null;
        private int? _AutoTopupStatus = null;
        private decimal? _AutoTopupAmount = null;
        private string _ActionCode = null;
        private int? loginType = null;
        private DateTime? _dateUpdated;
        private DateTime? _idExpiryDate;
        private string _mailType;
        private string _subscriberType;
        private int? _contractNo;
        #region Added by Liny,2012-5-16,for SRS for DMC V0.3.docx
        private string _DMCEndUserId;
        #endregion
        private int? _NEEDLCSENDWELCOMESMS;
        private DateTime? _CPPCOUNTER_STARTDATE;
        private int? _CPPCOUNTER;

        #region add by Oliver,2014-08-01,for smartLibertad and prepaid plus upgrade
        private int? _NEXT_PACKAGEID;
        private DateTime? _NEXT_PACKAGE_DATE;
        #endregion add by Oliver

        private DateTime? _TopupChangePackageDate = null;

        #region FK object
        public virtual CustomerInfo CustomerInfo
        {
            get { return _CustomerInfo; }
            set { _CustomerInfo = value; }
        }
        #endregion

        public DateTime? TopupChangePackageDate
        {
            get
            {
                return _TopupChangePackageDate;
            }
            set
            {
                _TopupChangePackageDate = value;
            }
        }

        #region Attribute
        /// <summary>
        /// Id of this customer
        /// </summary>
        public virtual int? CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        /// <summary>
        /// Unique Id of this Property
        /// </summary>

        public virtual int? PropertyID
        {
            get { return _PropertyID; }
            set { _PropertyID = value; }
        }

        /// <summary>
        /// Customer Type Id
        /// </summary>
        public virtual int? CustomerTypeID
        {
            get { return _CustomerTypeID; }
            set { _CustomerTypeID = value; }
        }

        /// <summary>
        /// LanuguageID, eg: 1033
        /// </summary>
        public virtual int? LanguageID
        {
            get { return _LanguageID; }
            set { _LanguageID = value; }
        }
        /// <summary>
        /// PaymentMethodId
        /// </summary>
        public virtual int? PaymentMethodID
        {
            get { return _PaymentMethodID; }
            set { _PaymentMethodID = value; }
        }

        public virtual int? BillingMethodID
        {
            get { return _BillingMethodID; }
            set { _BillingMethodID = value; }
        }

        public virtual bool ParentBilling
        {
            get { return _ParentBilling; }
            set { _ParentBilling = value; }
        }

        public virtual int? TrafficTypeID
        {
            get { return _TrafficTypeID; }
            set { _TrafficTypeID = value; }
        }

        public virtual int? TaxPlanID
        {
            get { return _TaxPlanID; }
            set { _TaxPlanID = value; }
        }

        public virtual bool InvoiceDetails
        {
            get { return _InvoiceDetails; }
            set { _InvoiceDetails = value; }
        }

        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public virtual int? InvoiceDueDate
        {
            get { return _InvoiceDueDate; }
            set { _InvoiceDueDate = value; }
        }

        int? _CountryCode;
        public virtual int? CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }

        string _CPSCode;
        public virtual string CPSCode
        {
            get { return _CPSCode; }
            set { _CPSCode = value; }
        }
        int? _BILLINGENTITY;
        public virtual int? BillingEntity
        {
            get
            {
                return _BILLINGENTITY;
            }
            set
            {
                _BILLINGENTITY = value;
            }
        }
        public virtual int? WithDrawPeriod
        {
            get { return _WithDrawPeriod; }
            set { _WithDrawPeriod = value; }
        }



        public virtual string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public virtual string PasswordDES
        {
            get { return _PasswordDES; }
            set { _PasswordDES = value; }
        }

        public virtual string PasswordMD5
        {
            get { return _PasswordMD5; }
            set { _PasswordMD5 = value; }
        }

        public virtual DateTime? Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
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

        public virtual int? IDType
        {
            get { return _IDType; }
            set { _IDType = value; }
        }

        public virtual string IDNumber
        {
            get { return _IDNumber; }
            set { _IDNumber = value; }
        }

        public virtual int? CreditScore
        {
            get { return _CreditScore; }
            set { _CreditScore = value; }
        }

        public virtual decimal? OriginalDepositAmount
        {
            get { return _OriginalDepositAmount; }
            set { _OriginalDepositAmount = value; }
        }

        public virtual decimal? CurrentDepositAmount
        {
            get { return _CurrentDepositAmount; }
            set { _CurrentDepositAmount = value; }
        }

        public virtual DateTime? DepositDate
        {
            get { return _DepositDate; }
            set { _DepositDate = value; }
        }

        public virtual int? PendingStatus
        {
            get { return _PendingStatus; }
            set { _PendingStatus = value; }
        }

        public virtual int? LoyaltyPoint
        {
            get { return _LoyaltyPoint; }
            set { _LoyaltyPoint = value; }
        }

        public virtual bool AcceptNews
        {
            get { return _AcceptNews; }
            set { _AcceptNews = value; }
        }

        public virtual DateTime? LastLoyaltyDate
        {
            get { return _LastLoyaltyDate; }
            set { _LastLoyaltyDate = value; }
        }

        public virtual bool FF
        {
            get { return _FF; }
            set { _FF = value; }
        }

        public virtual int? BillingScenarioID
        {
            get { return _BillingScenarioID; }
            set { _BillingScenarioID = value; }
        }

        public virtual int? ContractPeriod
        {
            get { return _ContractPeriod; }
            set { _ContractPeriod = value; }
        }

        public virtual int? CreditTransferType
        {
            get { return _CreditTransferType; }
            set { _CreditTransferType = value; }
        }

        public virtual string VATNO
        {
            get { return _VATNO; }
            set { _VATNO = value; }
        }

        public virtual int? DepositStatus
        {
            get { return _DepositStatus; }
            set { _DepositStatus = value; }
        }

        public virtual decimal? CurrentDepositCredit
        {
            get { return _CurrentDepositCredit; }
            set { _CurrentDepositCredit = value; }
        }

        public virtual int? AutoTopupStatus
        {
            get { return _AutoTopupStatus; }
            set { _AutoTopupStatus = value; }
        }

        public virtual decimal? AutoTopupAmount
        {
            get { return _AutoTopupAmount; }
            set { _AutoTopupAmount = value; }
        }

        public virtual string ActionCode
        {
            get
            {
                return _ActionCode;
            }
            set
            {
                _ActionCode = value;
            }
        }

        #region Added by Liny,2012-5-16,for SRS for DMC V0.3.docx
        /// <summary>
        /// the End User Id of DMC platform
        /// </summary>
        public virtual string DMCEndUserId
        {
            get { return _DMCEndUserId; }
            set { _DMCEndUserId = value; }
        }
        #endregion
        /// <summary>
        /// Flag if need LC to send welcome SMS
        /// </summary>
        public virtual int? NEEDLCSENDWELCOMESMS
        {
            get { return _NEEDLCSENDWELCOMESMS; }
            set { _NEEDLCSENDWELCOMESMS = value; }
        }

        public virtual DateTime? CPPCOUNTER_STARTDATE
        {
            get { return _CPPCOUNTER_STARTDATE; }
            set { _CPPCOUNTER_STARTDATE = value; }
        }

        public virtual int? CPPCOUNTER
        {
            get { return _CPPCOUNTER; }
            set { _CPPCOUNTER = value; }
        }

        #region add by Oliver,2014-08-01,for smartLibertad and prepaid plus upgrade
        public virtual int? NEXT_PACKAGEID
        {
            get { return _NEXT_PACKAGEID; }
            set { _NEXT_PACKAGEID = value; }
        }

        public virtual DateTime? NEXT_PACKAGE_DATE
        {
            get { return _NEXT_PACKAGE_DATE; }
            set { _NEXT_PACKAGE_DATE = value; }
        }
        #endregion add by Oliver

        public int? LoginType
        {
            get
            {

                return loginType;
            }
            set
            {
                loginType = value;
            }
        }

        public DateTime? DateUpdated
        {
            get
            {
                return _dateUpdated;
            }
            set
            {
                _dateUpdated = value;
            }
        }

        public DateTime? IDExpiryDate
        {
            get
            {
                return _idExpiryDate;
            }
            set
            {
                _idExpiryDate = value;
            }
        }

        public string MailType
        {
            get
            {
                return _mailType;
            }
            set
            {
                _mailType = value;
            }
        }

        public string SubscriberType
        {
            get
            {
                return _subscriberType;
            }
            set
            {
                _subscriberType = value;
            }
        }

        public int? ContractNo
        {
            get
            {
                return _contractNo;
            }
            set
            {
                _contractNo = value;
            }
        }

        /// <summary>
        /// this is external Id which can be generated by MVNO's system, such as SAP etc..
        /// </summary>
        public virtual string ExternalId { get; set; }
        public virtual decimal Cashdeposit { get; set; }
        public virtual decimal Roamingdeposit { get; set; }
        public int? ReferrerCustomerID
        {
            get;
            set;
        }
        //10-29
        public int CustomerRole
        {
            get;
            set;
        }
        #endregion
      

        public virtual PropertyInfo Clone()
        {
            return this.MemberwiseClone() as PropertyInfo;
        }

        //add by damon,2013-02-18 for Low Balance Notification
        public decimal LowBalanceQuantity { get; set; }

        //add by damon,2013-02-18 for Balance Left Notification
        public long ServiceSwitch { get; set; }

        //add by sam 2014-07-29 for ProfileUpdateSource
        public int? ProfileUpdateSource { get; set; }
        public virtual decimal CreditAdjustment { get; set; }
        public virtual string AlemCheck { get; set; }
        public virtual DateTime? DocumentValidateTime { get; set; }
        public virtual int? DocumentValidateStatus { get; set; }
        public virtual string DocumentRejectReason { get; set; }
        /// <summary>
        /// Low Roaming Quantity for Roaming Deposit
        /// </summary>
        public virtual decimal LowRoamingQuantity { get; set; }
    }
}
