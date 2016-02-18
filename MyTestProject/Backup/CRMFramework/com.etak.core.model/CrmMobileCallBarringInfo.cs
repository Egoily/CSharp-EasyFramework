using System;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-7-28 11:42:06
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-7-28 11:42:06
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-7-28 11:42:06
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-7-28 11:42:06
    /// </summary>
    [Serializable]
    public class CrmMobileCallBarringInfo : ModelBase
    {
        #region 构造函数
        public CrmMobileCallBarringInfo()
        { }

        public CrmMobileCallBarringInfo(int CALLBARRINGID, CrmCustomersResourceMbInfo crm_customers_resourcemb, int BSGID, bool BAOCPROV, bool BOICPROV, bool BOICEXHCPROV, bool BAICPROV, bool BICROAMPROV, int BAOCACT, int BOICACT, int BOICEXHCACT, int BAICACT, int BICROAMACT, bool BAOCIND, bool BOICIND, bool BOICEXHCIND, bool BAICIND, bool BICROAMIND)
        {
            this._CALLBARRINGID = CALLBARRINGID;
            this._CRM_CUSTOMERS_RESOURCEMB = crm_customers_resourcemb;
            this._BSGID = BSGID;
            this._BAOCPROV = BAOCPROV;
            this._BOICPROV = BOICPROV;
            this._BOICEXHCPROV = BOICEXHCPROV;
            this._BAICPROV = BAICPROV;
            this._BICROAMPROV = BICROAMPROV;
            this._BAOCACT = BAOCACT;
            this._BOICACT = BOICACT;
            this._BOICEXHCACT = BOICEXHCACT;
            this._BAICACT = BAICACT;
            this._BICROAMACT = BICROAMACT;
            this._BAOCIND = BAOCIND;
            this._BOICIND = BOICIND;
            this._BOICEXHCIND = BOICEXHCIND;
            this._BAICIND = BAICIND;
            this._BICROAMIND = BICROAMIND;
        }
        #endregion

        #region 成员
        private int _CALLBARRINGID;
        protected CrmCustomersResourceMbInfo _CRM_CUSTOMERS_RESOURCEMB;
        private int _BSGID;
        private bool _BAOCPROV;
        private bool _BOICPROV;
        private bool _BOICEXHCPROV;
        private bool _BAICPROV;
        private bool _BICROAMPROV;
        private int _BAOCACT;
        private int _BOICACT;
        private int _BOICEXHCACT;
        private int _BAICACT;
        private int _BICROAMACT;
        private bool _BAOCIND;
        private bool _BOICIND;
        private bool _BOICEXHCIND;
        private bool _BAICIND;
        private bool _BICROAMIND;
        #endregion


        #region 属性
        public virtual int CALLBARRINGID
        {
            get { return _CALLBARRINGID; }
            set { _CALLBARRINGID = value; }
        }

        public virtual CrmCustomersResourceMbInfo CrmCustomersResourceMbInfo
        {
            get { return _CRM_CUSTOMERS_RESOURCEMB; }
            set { _CRM_CUSTOMERS_RESOURCEMB = value; }
        }

        public virtual int BSGID
        {
            get { return _BSGID; }
            set { _BSGID = value; }
        }

        public virtual bool BAOCPROV
        {
            get { return _BAOCPROV; }
            set { _BAOCPROV = value; }
        }

        public virtual bool BOICPROV
        {
            get { return _BOICPROV; }
            set { _BOICPROV = value; }
        }

        public virtual bool BOICEXHCPROV
        {
            get { return _BOICEXHCPROV; }
            set { _BOICEXHCPROV = value; }
        }

        public virtual bool BAICPROV
        {
            get { return _BAICPROV; }
            set { _BAICPROV = value; }
        }

        public virtual bool BICROAMPROV
        {
            get { return _BICROAMPROV; }
            set { _BICROAMPROV = value; }
        }

        public virtual int BAOCACT
        {
            get { return _BAOCACT; }
            set { _BAOCACT = value; }
        }

        public virtual int BOICACT
        {
            get { return _BOICACT; }
            set { _BOICACT = value; }
        }

        public virtual int BOICEXHCACT
        {
            get { return _BOICEXHCACT; }
            set { _BOICEXHCACT = value; }
        }

        public virtual int BAICACT
        {
            get { return _BAICACT; }
            set { _BAICACT = value; }
        }

        public virtual int BICROAMACT
        {
            get { return _BICROAMACT; }
            set { _BICROAMACT = value; }
        }

        public virtual bool BAOCIND
        {
            get { return _BAOCIND; }
            set { _BAOCIND = value; }
        }

        public virtual bool BOICIND
        {
            get { return _BOICIND; }
            set { _BOICIND = value; }
        }

        public virtual bool BOICEXHCIND
        {
            get { return _BOICEXHCIND; }
            set { _BOICEXHCIND = value; }
        }

        public virtual bool BAICIND
        {
            get { return _BAICIND; }
            set { _BAICIND = value; }
        }

        public virtual bool BICROAMIND
        {
            get { return _BICROAMIND; }
            set { _BICROAMIND = value; }
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

        public override ModelBase CreateHistory()
        {
            //CrmMobileCallBarringHistoryInfo model = new CrmMobileCallBarringHistoryInfo();
            //model.CopyPropertyDataFrom(this);

            //if (this.CrmCustomersResourceMbInfo != null) model.RESOURCEID = this.CrmCustomersResourceMbInfo.RESOURCEID; // Add by wood, Get resourceId for history.
            ////model.CALLBARRINGID = 0;
            return null;
        }
        #endregion

    }
}
