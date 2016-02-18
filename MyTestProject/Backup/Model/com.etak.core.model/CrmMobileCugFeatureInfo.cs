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
    public class CrmMobileCugFeatureInfo : ModelBase
    {
        #region 构造函数
        public CrmMobileCugFeatureInfo()
        { }

        public CrmMobileCugFeatureInfo(int CUGFEATUREID, CrmCustomersResourceMbInfo crm_customers_resourcemb, int BSGID, int INTERCUGRESTRICTION, int PREFERENTIALCUGINDEX)
        {
            this._CUGFEATUREID = CUGFEATUREID;
            this._CRM_CUSTOMERS_RESOURCEMB = crm_customers_resourcemb;
            this._BSGID = BSGID;
            this._INTERCUGRESTRICTION = INTERCUGRESTRICTION;
            this._PREFERENTIALCUGINDEX = PREFERENTIALCUGINDEX;
        }
        #endregion

        #region 成员
        private int _CUGFEATUREID;
        protected CrmCustomersResourceMbInfo _CRM_CUSTOMERS_RESOURCEMB;
        private int _BSGID;
        private int _INTERCUGRESTRICTION;
        private int _PREFERENTIALCUGINDEX;
        #endregion


        #region 属性
        public virtual int CUGFEATUREID
        {
            get { return _CUGFEATUREID; }
            set { _CUGFEATUREID = value; }
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

        public virtual int INTERCUGRESTRICTION
        {
            get { return _INTERCUGRESTRICTION; }
            set { _INTERCUGRESTRICTION = value; }
        }

        public virtual int PREFERENTIALCUGINDEX
        {
            get { return _PREFERENTIALCUGINDEX; }
            set { _PREFERENTIALCUGINDEX = value; }
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
            //CrmMobileCugFeatureHistoryInfo model = new CrmMobileCugFeatureHistoryInfo();
            //model.CopyPropertyDataFrom(this);

            //if (this.CrmCustomersResourceMbInfo != null) model.RESOURCEID = this.CrmCustomersResourceMbInfo.RESOURCEID; // Add by wood, Get resourceId for history.
            ////model.CUGFEATUREID = 0;
            //return model;
            return null;

        }
    }
}
