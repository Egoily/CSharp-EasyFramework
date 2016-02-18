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
    public class CrmMobileMultipleImsiInfo : ModelBase
    {
        #region 构造函数
        public CrmMobileMultipleImsiInfo()
        { }

        public CrmMobileMultipleImsiInfo(int IMSIID, CrmCustomersResourceMbInfo crm_customers_resourcemb, string ALTIMSI, string ALTMSISDN, bool PUBLISHED, bool DISPLAYED)
        {
            this._IMSIID = IMSIID;
            this._CRM_CUSTOMERS_RESOURCEMB = crm_customers_resourcemb;
            this._ALTIMSI = ALTIMSI;
            this._ALTMSISDN = ALTMSISDN;
            this._PUBLISHED = PUBLISHED;
            this._DISPLAYED = DISPLAYED;
        }
        #endregion

        #region 成员
        private int _IMSIID;
        protected CrmCustomersResourceMbInfo _CRM_CUSTOMERS_RESOURCEMB;
        private string _ALTIMSI;
        private string _ALTMSISDN;
        private bool _PUBLISHED;
        private bool _DISPLAYED;
        #endregion


        #region 属性
        public virtual int IMSIID
        {
            get { return _IMSIID; }
            set { _IMSIID = value; }
        }

        public virtual CrmCustomersResourceMbInfo CrmCustomersResourceMbInfo
        {
            get { return _CRM_CUSTOMERS_RESOURCEMB; }
            set { _CRM_CUSTOMERS_RESOURCEMB = value; }
        }

        public virtual string ALTIMSI
        {
            get { return _ALTIMSI; }
            set { _ALTIMSI = value; }
        }

        public virtual string ALTMSISDN
        {
            get { return _ALTMSISDN; }
            set { _ALTMSISDN = value; }
        }

        public virtual bool PUBLISHED
        {
            get { return _PUBLISHED; }
            set { _PUBLISHED = value; }
        }

        public virtual bool DISPLAYED
        {
            get { return _DISPLAYED; }
            set { _DISPLAYED = value; }
        }

        int _STATUS;
        public virtual int Status
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        bool _DeleateFlag;
        public virtual bool DeleteFlag
        {
            get { return _DeleateFlag; }
            set { _DeleateFlag = value; }
        }
        bool isNew;
        public virtual bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
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
            //CrmMobileMultipleImsiHistoryInfo model = new CrmMobileMultipleImsiHistoryInfo();
            //model.CopyPropertyDataFrom(this);

            //if (this.CrmCustomersResourceMbInfo != null) model.RESOURCEID = this.CrmCustomersResourceMbInfo.RESOURCEID; // Add by wood, Get resourceId for history.
            ////model.IMSIID = 0;
            //return model;
            return null;

        }
    }
}
