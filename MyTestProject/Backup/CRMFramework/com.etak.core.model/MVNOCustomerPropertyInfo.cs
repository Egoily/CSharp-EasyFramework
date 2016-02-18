using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MVNOCustomerPropertyInfo
    {
        private CustomerInfo _CustomerInfo;

        private int? _PropertyID = null;        
        private int? _Nationality = null;        
        private int? _JobTitle = null;
        private string _OtherJobTitle;
        private int? _ENABLECELLID;
        private string _TeamID;
        private int? _GoalAlertingStatus;
        private string _Handset;
        //add by devid 20110121
        private int? _GoalUpdateStatus;
        private int? _CustomerAgentTypeid;

        //add by devid 20110216 ， adding field GoalCredit for clubmobiel
        private int? _GoalCredit;

        #region added by Liny,2010-06-07,because of table changing

        private string _DeliveryAddress;
        private string _Imei;
        private int? _AdvertisementBarring;
        private int? _RecurringTopupStatus;
        private DateTime? _RecurringTopupDate;
        private decimal? _RecurringTopupAmount;
        private int? _MinimumTopupStatus;
        private decimal? _MinimumAmount;
        private decimal? _MinimumTopupAmount;


        public string DeliveryAddress
        {
            get
            {
                return _DeliveryAddress;
            }
            set
            {
                _DeliveryAddress = value;
            }
        }

        public string Imei
        {
            get
            {
                return _Imei;
            }
            set
            {
                _Imei = value;
            }
        }

        public int? AdvertisementBarring
        {
            get
            {
                return _AdvertisementBarring;
            }
            set
            {
                _AdvertisementBarring = value;
            }
        }

        public int? RecurringTopupStatus
        {
            get
            {
                return _RecurringTopupStatus;
            }
            set
            {
                _RecurringTopupStatus = value;
            }
        }

        public DateTime? RecurringTopupDate
        {
            get
            {
                return _RecurringTopupDate;
            }
            set
            {
                _RecurringTopupDate = value;
            }
        }

        public decimal? RecurringTopupAmount
        {
            get
            {
                return _RecurringTopupAmount;
            }
            set
            {
                _RecurringTopupAmount = value;
            }
        }

        public int? MinimumTopupStatus
        {
            get
            {
                return _MinimumTopupStatus;
            }
            set
            {
                _MinimumTopupStatus = value;
            }
        }

        public decimal? MinimumAmount
        {
            get
            {
                return _MinimumAmount;
            }
            set
            {
                _MinimumAmount = value;
            }
        }

        public decimal? MinimumTopupAmount
        {
            get
            {
                return _MinimumTopupAmount;
            }
            set
            {
                _MinimumTopupAmount = value;
            }
        }
    
        #endregion

        #region Add by rabi 2010-06-21

        private bool _UPDATEDONLINE;
        private DateTime? _UPDATEDONLINEDATE;
        public bool UPDATEDONLINE
        {
            get
            {
                return _UPDATEDONLINE;
            }
            set
            {
                _UPDATEDONLINE = value;
            }
        }
        public DateTime? UPDATEDONLINEDATE
        {
            get
            {
                return _UPDATEDONLINEDATE;
            }
            set
            {
                _UPDATEDONLINEDATE = value;
            }
        }
        #endregion


        #region Add by winson 2010-08-23
        private DateTime? _FIRSTLOGINTIME;
        public DateTime? FIRSTLOGINTIME
        {
            get
            {
                return _FIRSTLOGINTIME;
            }
            set
            {
                _FIRSTLOGINTIME = value;
            }
        }
        #endregion

        #region Add by Liny, 2010-08-24

        private DateTime? _REGISTERDATE;
        public DateTime? REGISTERDATE
        {
            get
            {
                return _REGISTERDATE;
            }
            set
            {
                _REGISTERDATE = value;
            }
        }

        #endregion

        #region added by Liny,2010-09-07,for UpdateCustomerAndEmail api

        private DateTime? _AddedDate;
        public DateTime? AddedDate
        {
            get
            {
                return _AddedDate;
            }
            set
            {
                _AddedDate = value;
            }
        }

        #endregion

        #region  added by Liny,2010-09-15,for ben customer type

        private int? _BenCustomerType;
        public int? BenCustomerType
        {
            get { return _BenCustomerType; }
            set { _BenCustomerType = value; }
        }

        #endregion

        #region  added by Liny,2011-07-08,for first roaming call

        private DateTime? _FirstRoamingTime;
        public DateTime? FirstRoamingTime
        {
            get { return _FirstRoamingTime; }
            set { _FirstRoamingTime = value; }
        }

        #endregion

        #region add by richard 20120307 for autotopup
        private int? _autoTopupStatus;
        private decimal? _autoTopupAmount;
        private decimal? _autoTopupThreshold;
        private int? _autoTopupSendFlag;
        #endregion
       
        #region FK object
        public virtual CustomerInfo CustomerInfo
        {
            get { return _CustomerInfo; }
            set { _CustomerInfo = value; }
        }
        #endregion


        #region Attribute
       

        public virtual int? PropertyID
        {
            get { return _PropertyID; }
            set { _PropertyID = value; }
        }

        public virtual int? Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }

        public virtual int? JobTitle
        {
            get { return _JobTitle; }
            set { _JobTitle = value; }
        }

        public virtual string OtherJobTitle
        {
            get { return _OtherJobTitle; }
            set { _OtherJobTitle = value; }
        }

        public virtual int? ENABLECELLID
        {
            get { return _ENABLECELLID; }
            set { _ENABLECELLID = value; }
        }


        public virtual string TeamID
        {
            get { return _TeamID; }
            set { _TeamID = value; }
        }

        public virtual string Handset
        {
            get { return _Handset; }
            set { _Handset = value; }
        }

        public virtual int? GoalAlertingStatus
        {
            get { return _GoalAlertingStatus; }
            set { _GoalAlertingStatus = value; }
        }

        public virtual int? GoalUpdateStatus
        {
            get { return _GoalUpdateStatus; }
            set { _GoalUpdateStatus = value; }
        }

        //add by devid 20110125
        public virtual int? CustomerAgentTypeid 
        {
            get { return _CustomerAgentTypeid; }
            set { _CustomerAgentTypeid = value;}
        }

        //add by devid 20110216 ， adding field GoalCredit for clubmobiel
        public virtual int? GoalCredit
        {
            get { return _GoalCredit; }
            set { _GoalCredit = value; }
        }

        #region add by richard 20120307 for autotopup
        public virtual int? AutoTopupStatus     
        {
            get { return _autoTopupStatus; }
            set { _autoTopupStatus = value; }
        }


        public virtual decimal? AutoTopupAmount
        {
            get { return _autoTopupAmount; }
            set { _autoTopupAmount = value; }
        }

        public virtual decimal? AutoTopupThreshold
        {
            get { return _autoTopupThreshold; }
            set { _autoTopupThreshold = value; }
        }

        public virtual int? AutoTopupSendFlag
        {
            get { return _autoTopupSendFlag; }
            set { _autoTopupSendFlag = value; }
        }

        #endregion
        #endregion

        public virtual MVNOCustomerPropertyInfo Clone()
        {
            return this.MemberwiseClone() as MVNOCustomerPropertyInfo;
        }
    }
}
