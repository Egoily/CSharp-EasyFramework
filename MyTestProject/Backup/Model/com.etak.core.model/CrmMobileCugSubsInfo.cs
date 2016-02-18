using System;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-7-28 11:42:07
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-7-28 11:42:07
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-7-28 11:42:07
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-7-28 11:42:07
    /// </summary>
    [Serializable]
    public class CrmMobileCugSubsInfo : ModelBase
    {
        #region 构造函数
        public CrmMobileCugSubsInfo()
        { }

        public CrmMobileCugSubsInfo(int CUGSUBSID, CrmCustomersResourceMbInfo crm_customers_resourcemb, string CUGINTERLOCK, int CUGINDEX, int INTRACUGOPTION, string BSGLIST)
        {
            this._CUGSUBSID = CUGSUBSID;
            this._CRM_CUSTOMERS_RESOURCEMB = crm_customers_resourcemb;
            this._CUGINTERLOCK = CUGINTERLOCK;
            this._CUGINDEX = CUGINDEX;
            this._INTRACUGOPTION = INTRACUGOPTION;
            this._BSGLIST = BSGLIST;
        }
        #endregion

        #region 成员
        private int _CUGSUBSID;
        protected CrmCustomersResourceMbInfo _CRM_CUSTOMERS_RESOURCEMB;
        private string _CUGINTERLOCK;
        private int _CUGINDEX;
        private int _INTRACUGOPTION;
        private string _BSGLIST;
        #endregion


        #region 属性
        public virtual int CUGSUBSID
        {
            get { return _CUGSUBSID; }
            set { _CUGSUBSID = value; }
        }

        public virtual CrmCustomersResourceMbInfo CrmCustomersResourceMbInfo
        {
            get { return _CRM_CUSTOMERS_RESOURCEMB; }
            set { _CRM_CUSTOMERS_RESOURCEMB = value; }
        }

        public virtual string CUGINTERLOCK
        {
            get { return _CUGINTERLOCK; }
            set { _CUGINTERLOCK = value; }
        }

        public virtual int CUGINDEX
        {
            get { return _CUGINDEX; }
            set { _CUGINDEX = value; }
        }

        public virtual int INTRACUGOPTION
        {
            get { return _INTRACUGOPTION; }
            set { _INTRACUGOPTION = value; }
        }

        public virtual string BSGLIST
        {
            get { return _BSGLIST; }
            set { _BSGLIST = value; }
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
            //CrmMobileCugSubsHistoryInfo model = new CrmMobileCugSubsHistoryInfo();
            //model.CopyPropertyDataFrom(this);

            //if (this.CrmCustomersResourceMbInfo != null) model.RESOURCEID = this.CrmCustomersResourceMbInfo.RESOURCEID; // Add by wood, Get resourceId for history.
            ////model.CUGSUBSID = 0;
            //return model;
            return null;

        }
    }
}
