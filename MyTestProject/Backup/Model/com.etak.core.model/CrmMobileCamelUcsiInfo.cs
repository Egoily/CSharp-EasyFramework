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
    ///Last Modify Date     :    2013-06-19 11:42:07
    /// </summary>
    [Serializable]
    public class CrmMobileCamelUcsiInfo : ModelBase
    {
        #region 构造函数
        public CrmMobileCamelUcsiInfo()
        { }

        public CrmMobileCamelUcsiInfo(long CamelUCsiID, CrmCustomersResourceMbInfo crm_customers_resourcemb, int GSMSCFID, int SERVICECODE)
        {
            this._CamelUCsiID = CamelUCsiID;
            this._CRM_CUSTOMERS_RESOURCEMB = crm_customers_resourcemb;
            this._GSMSCFID = GSMSCFID;
            this._SERVICECODE = SERVICECODE;
        }
        #endregion

        #region 成员
        private long _CamelUCsiID; // Modified by wood, use "long" instead of "int"
        protected CrmCustomersResourceMbInfo _CRM_CUSTOMERS_RESOURCEMB;
        private int _GSMSCFID;
        private int _SERVICECODE;
        #endregion


        #region 属性
        public virtual long CamelUCsiID
        {
            get { return _CamelUCsiID; }
            set { _CamelUCsiID = value; }
        }

        public virtual CrmCustomersResourceMbInfo CrmCustomersResourceMbInfo
        {
            get { return _CRM_CUSTOMERS_RESOURCEMB; }
            set { _CRM_CUSTOMERS_RESOURCEMB = value; }
        }

        public virtual int GSMSCFID
        {
            get { return _GSMSCFID; }
            set { _GSMSCFID = value; }
        }

        public virtual int SERVICECODE
        {
            get { return _SERVICECODE; }
            set { _SERVICECODE = value; }
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
            //CrmMobileCamelUcsiHistoryInfo model = new CrmMobileCamelUcsiHistoryInfo();
            //model.CopyPropertyDataFrom(this);

            //if (this.CrmCustomersResourceMbInfo != null) model.RESOURCEID = this.CrmCustomersResourceMbInfo.RESOURCEID; // Add by wood, Get resourceId for history.
            ////model.CamelUCsiID = 0;
            //return model;
            return null;

        }
    }
}
