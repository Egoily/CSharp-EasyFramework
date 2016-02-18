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
    public class CrmMobileCallWaitingInfo : ModelBase
    {
        #region 构造函数
        public CrmMobileCallWaitingInfo()
        { }

        public CrmMobileCallWaitingInfo(int CALLWAITINGID, CrmCustomersResourceMbInfo crm_customers_resourcemb, int BSGID, bool ACTIVATION)
        {
            this._CALLWAITINGID = CALLWAITINGID;
            this._CRM_CUSTOMERS_RESOURCEMB = crm_customers_resourcemb;
            this._BSGID = BSGID;
            this._ACTIVATION = ACTIVATION;
        }
        #endregion

        #region 成员
        private int _CALLWAITINGID;
        protected CrmCustomersResourceMbInfo _CRM_CUSTOMERS_RESOURCEMB;
        private int _BSGID;
        private bool _ACTIVATION;
        #endregion


        #region 属性
        public virtual int CALLWAITINGID
        {
            get { return _CALLWAITINGID; }
            set { _CALLWAITINGID = value; }
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

        public virtual bool ACTIVATION
        {
            get { return _ACTIVATION; }
            set { _ACTIVATION = value; }
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
            return null;
        }
    }
}
