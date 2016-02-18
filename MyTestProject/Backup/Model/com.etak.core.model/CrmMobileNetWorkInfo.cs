using System;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-7-28 11:42:08
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-7-28 11:42:08
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-7-28 11:42:08
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-7-28 11:42:08
    /// </summary>
    [Serializable]
    public class CrmMobileNetWorkInfo : ModelBase
    {
        #region 构造函数
        public CrmMobileNetWorkInfo()
        { }

        public CrmMobileNetWorkInfo(int NETWORKID, CrmCustomersResourceMbInfo crm_customers_resourcemb, int NetworkType, int PDPCONTEXTID, int QosGuaranteedBitRateUp, int QosGuaranteedBitRateDown, string APN, bool MMS, bool WAP, int QosMaxBitRateUp, int QosMaxBitRateDown, bool OBOPRE, bool OBOPRI)
        {
            this._NETWORKID = NETWORKID;
            this._CRM_CUSTOMERS_RESOURCEMB = crm_customers_resourcemb;
            this._NetworkType = NetworkType;
            this._PDPCONTEXTID = PDPCONTEXTID;
            this._QosGuaranteedBitRateUp = QosGuaranteedBitRateUp;
            this._QosGuaranteedBitRateDown = QosGuaranteedBitRateDown;
            this._APN = APN;
            this._MMS = MMS;
            this._WAP = WAP;
            this._QosMaxBitRateUp = QosMaxBitRateUp;
            this._QosMaxBitRateDown = QosMaxBitRateDown;
            this._OBOPRE = OBOPRE;
            this._OBOPRI = OBOPRI;
        }
        #endregion

        #region 成员
        private int _NETWORKID;
        protected CrmCustomersResourceMbInfo _CRM_CUSTOMERS_RESOURCEMB;
        private int _NetworkType;
        private int _PDPCONTEXTID;
        private string _PDPContextAddress;
        private int _QosGuaranteedBitRateUp;
        private int _QosGuaranteedBitRateDown;
        private string _APN;
        private bool _MMS;
        private bool _WAP;
        private int _QosMaxBitRateUp;
        private int _QosMaxBitRateDown;
        private bool _OBOPRE;
        private bool _OBOPRI;
        #endregion


        #region 属性
        public virtual int NETWORKID
        {
            get { return _NETWORKID; }
            set { _NETWORKID = value; }
        }

        public virtual CrmCustomersResourceMbInfo CrmCustomersResourceMbInfo
        {
            get { return _CRM_CUSTOMERS_RESOURCEMB; }
            set { _CRM_CUSTOMERS_RESOURCEMB = value; }
        }

        public virtual int NetworkType
        {
            get { return _NetworkType; }
            set { _NetworkType = value; }
        }

        public virtual int PDPCONTEXTID
        {
            get { return _PDPCONTEXTID; }
            set { _PDPCONTEXTID = value; }
        }

        public virtual string PDPContextAddress
        {
            get { return _PDPContextAddress; }
            set { _PDPContextAddress = value; }
        }

        public virtual int QosGuaranteedBitRateUp
        {
            get { return _QosGuaranteedBitRateUp; }
            set { _QosGuaranteedBitRateUp = value; }
        }

        public virtual int QosGuaranteedBitRateDown
        {
            get { return _QosGuaranteedBitRateDown; }
            set { _QosGuaranteedBitRateDown = value; }
        }

        public virtual string APN
        {
            get { return _APN; }
            set { _APN = value; }
        }

        public virtual bool MMS
        {
            get { return _MMS; }
            set { _MMS = value; }
        }

        public virtual bool WAP
        {
            get { return _WAP; }
            set { _WAP = value; }
        }

        public virtual int QosMaxBitRateUp
        {
            get { return _QosMaxBitRateUp; }
            set { _QosMaxBitRateUp = value; }
        }

        public virtual int QosMaxBitRateDown
        {
            get { return _QosMaxBitRateDown; }
            set { _QosMaxBitRateDown = value; }
        }

        public virtual bool OBOPRE
        {
            get { return _OBOPRE; }
            set { _OBOPRE = value; }
        }

        public virtual bool OBOPRI
        {
            get { return _OBOPRI; }
            set { _OBOPRI = value; }
        }

        bool _DeleateFlag;
        public virtual bool DeleteFlag
        {
            get { return _DeleateFlag; }
            set { _DeleateFlag = value; }
        }


        int _FreeWebsiteThreshold;
        public virtual int FreeWebsiteThreshold
        {
            get { return _FreeWebsiteThreshold; }
            set { _FreeWebsiteThreshold = value; }
        }

        int _AdultContentBarringStatus;
        public virtual int AdultContentBarringStatus
        {
            get { return _AdultContentBarringStatus; }
            set { _AdultContentBarringStatus = value; }
        }

        int _FreeWebsiteStatus;
        public virtual int FreeWebsiteStatus
        {
            get { return _FreeWebsiteStatus; }
            set { _FreeWebsiteStatus = value; }
        }

        int _id;
        int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }      
        #endregion
        public override ModelBase CreateHistory()
        {
            //CrmMobileNetWorkHistoryInfo model = new CrmMobileNetWorkHistoryInfo();
            //model.CopyPropertyDataFrom(this);

            //if (this.CrmCustomersResourceMbInfo != null) model.RESOURCEID = this.CrmCustomersResourceMbInfo.RESOURCEID; // Add by wood, Get resourceId for history.
            ////model.NETWORKID = 0;
            //return model;
            return null;

        }
    }
}
