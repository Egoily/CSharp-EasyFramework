using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MVNOPropertiesInfo
    {
        #region 
        private int? _DealerID = null;
        private string _VMO;
        private int? _BRSOperatorCode = null;
        private string _OperatorCode;
        private bool _FF;
        private string _RequestForm;
        private int? _PrepaidBillingDate = null;
        private int? _PostpaidBillingDate = null;
        private string _SMSRegularTime = null;
        private int? _SecurityNumberCount = 0;
        private bool? _QueryWithVAT = true;
        private bool? _AutoTopupScanFlagForDRE = false;
        private bool? _AutoTopupScanFlagForCRM = false;

        private decimal? _RoamingSpendingThreshold;
        private bool? _RoamingSpendingNotificationStatus = false;
        private decimal? _SpendingThreshold;
        private bool? _SpendingNotificationStatus = false;

        private DealerInfo _DealerInfo;
        #endregion
        
        #region Attribute
        public virtual int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public virtual string VMO
        {
            get { return _VMO; }
            set { _VMO = value; }
        }

        public virtual  int? BRSOperatorCode
        {
            get { return _BRSOperatorCode; }
            set { _BRSOperatorCode = value; }
        }

        public virtual string OperatorCode
        {
            get { return _OperatorCode; }
            set { _OperatorCode = value; }
        }

        public virtual bool FF
        {
            get { return _FF; }
            set { _FF = value; }
        }

        public virtual string RequestForm
        {
            get { return _RequestForm; }
            set { _RequestForm = value; }
        }

        public virtual int? PrepaidBillingDate
        {
            get { return _PrepaidBillingDate; }
            set { _PrepaidBillingDate = value; }
        }

        public virtual int? PostpaidBillingDate
        {
            get { return _PostpaidBillingDate; }
            set { _PostpaidBillingDate = value; }
        }

        public virtual DealerInfo DealerInfo
        {
            get { return _DealerInfo; }
            set { _DealerInfo = value; }
        }

        public virtual string SMSRegularTime
        {
            get { return _SMSRegularTime; }
            set { _SMSRegularTime = value; }
        }

        public virtual int? SecurityNumberCount
        {
            get { return _SecurityNumberCount; }
            set { _SecurityNumberCount = value; }
        }

        /// <summary>
        /// If VAT Included when making queries
        /// </summary>
        public virtual bool? QueryWithVAT
        {
            get { return _QueryWithVAT; }
            set { _QueryWithVAT = value; }
        }

        public virtual bool? AutoTopupScanFlagForDRE
        {
            get { return _AutoTopupScanFlagForDRE; }
            set { _AutoTopupScanFlagForDRE = value; }
        }
        public virtual bool? AutoTopupScanFlagForCRM
        {
            get { return _AutoTopupScanFlagForCRM; }
            set { _AutoTopupScanFlagForCRM = value; }
        }

        public virtual decimal? RoamingSpendingThreshold
        {
            get { return _RoamingSpendingThreshold; }
            set { _RoamingSpendingThreshold = value; }
        }
        public virtual decimal? SpendingThreshold
        {
            get { return _SpendingThreshold; }
            set { _SpendingThreshold = value; }
        }

        public virtual bool? RoamingSpendingNotificationStatus
        {
            get { return _RoamingSpendingNotificationStatus; }
            set { _RoamingSpendingNotificationStatus = value; }
        }
        public virtual bool? SpendingNotificationStatus
        {
            get { return _SpendingNotificationStatus; }
            set { _SpendingNotificationStatus = value; }
        }

        #endregion

        public MVNOPropertiesInfo() { }
        public MVNOPropertiesInfo(int dealerID, string smsRegularTime) 
        {
            this._DealerID = dealerID;
            this._SMSRegularTime = smsRegularTime;
        }
    }
}
