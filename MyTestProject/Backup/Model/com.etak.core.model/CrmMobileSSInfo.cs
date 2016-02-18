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
    public class CrmMobileSSInfo : ModelBase
    {
        #region 构造函数
        public CrmMobileSSInfo()
        { }

        public CrmMobileSSInfo(int SSID, CrmCustomersResourceMbInfo crm_customers_resourcemb, bool AOCC, bool AOCI, bool CALLWAITING, bool CALLHOLD, bool CLIP, bool CLIPOVERRIDE, bool CLIR, int CLIRPRESENTATIONMODE, bool COLP, bool COLPOVERRIDE, bool COLR, bool ECT, bool MULTIPARTY)
        {
            this._SSID = SSID;
            this._CRM_CUSTOMERS_RESOURCEMB = crm_customers_resourcemb;
            this._AOCC = AOCC;
            this._AOCI = AOCI;
            this._CALLWAITING = CALLWAITING;
            this._CALLHOLD = CALLHOLD;
            this._CLIP = CLIP;
            this._CLIPOVERRIDE = CLIPOVERRIDE;
            this._CLIR = CLIR;
            this._CLIRPRESENTATIONMODE = CLIRPRESENTATIONMODE;
            this._COLP = COLP;
            this._COLPOVERRIDE = COLPOVERRIDE;
            this._COLR = COLR;
            this._ECT = ECT;
            this._MULTIPARTY = MULTIPARTY;
        }
        #endregion

        #region 成员
        private int _SSID;
        protected CrmCustomersResourceMbInfo _CRM_CUSTOMERS_RESOURCEMB;
        private bool _AOCC;
        private bool _AOCI;
        private bool _CALLWAITING;
        private bool _CALLHOLD;
        private bool _CLIP;
        private bool _CLIPOVERRIDE;
        private bool _CLIR;
        private int _CLIRPRESENTATIONMODE;
        private bool _COLP;
        private bool _COLPOVERRIDE;
        private bool _COLR;
        private bool _ECT;
        private bool _MULTIPARTY;
        #endregion


        #region 属性
        public virtual int SSID
        {
            get { return _SSID; }
            set { _SSID = value; }
        }

        public virtual CrmCustomersResourceMbInfo CrmCustomersResourceMbInfo
        {
            get { return _CRM_CUSTOMERS_RESOURCEMB; }
            set { _CRM_CUSTOMERS_RESOURCEMB = value; }
        }

        public virtual bool AOCC
        {
            get { return _AOCC; }
            set { _AOCC = value; }
        }

        public virtual bool AOCI
        {
            get { return _AOCI; }
            set { _AOCI = value; }
        }

        public virtual bool CALLWAITING
        {
            get { return _CALLWAITING; }
            set { _CALLWAITING = value; }
        }

        public virtual bool CALLHOLD
        {
            get { return _CALLHOLD; }
            set { _CALLHOLD = value; }
        }

        public virtual bool CLIP
        {
            get { return _CLIP; }
            set { _CLIP = value; }
        }

        public virtual bool CLIPOVERRIDE
        {
            get { return _CLIPOVERRIDE; }
            set { _CLIPOVERRIDE = value; }
        }

        public virtual bool CLIR
        {
            get { return _CLIR; }
            set { _CLIR = value; }
        }

        public virtual int CLIRPRESENTATIONMODE
        {
            get { return _CLIRPRESENTATIONMODE; }
            set { _CLIRPRESENTATIONMODE = value; }
        }

        public virtual bool COLP
        {
            get { return _COLP; }
            set { _COLP = value; }
        }

        public virtual bool COLPOVERRIDE
        {
            get { return _COLPOVERRIDE; }
            set { _COLPOVERRIDE = value; }
        }

        public virtual bool COLR
        {
            get { return _COLR; }
            set { _COLR = value; }
        }

        public virtual bool ECT
        {
            get { return _ECT; }
            set { _ECT = value; }
        }

        public virtual bool MULTIPARTY
        {
            get { return _MULTIPARTY; }
            set { _MULTIPARTY = value; }
        }

        bool _DeleateFlag;
        public virtual bool DeleteFlag
        {
            get { return _DeleateFlag; }
            set { _DeleateFlag = value; }
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
            //CrmMobileSsHistoryInfo model = new CrmMobileSsHistoryInfo();
            //model.CopyPropertyDataFrom(this);

            //if (this.CrmCustomersResourceMbInfo != null) model.RESOURCEID = this.CrmCustomersResourceMbInfo.RESOURCEID; // Add by wood, Get resourceId for history.
            ////model.SSID = 0;
            //return model;
            return null;

        }
    }
}
