using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2010-10-16 13:06:57
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2010-10-16 13:06:57
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2010-10-16 13:06:57
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2010-10-16 13:06:57
    /// </summary>
    [DataContract]
    [Serializable]
    public class CrmCustomersResourceMbPropertyInfo : ModelBase
    {
        #region 构造函数
        public CrmCustomersResourceMbPropertyInfo()
        {

        }

        public CrmCustomersResourceMbPropertyInfo(int PROPERTYID, CrmCustomersResourceMbInfo crm_customers_resourcemb,bool SMSSTATUS)
        {
            this._PROPERTYID = PROPERTYID;
            this._CRM_CUSTOMERS_RESOURCEMB = crm_customers_resourcemb;
            this._SMSSTATUS = SMSSTATUS;
        }
        #endregion

        #region 成员
        private int _PROPERTYID;
        protected CrmCustomersResourceMbInfo _CRM_CUSTOMERS_RESOURCEMB;
        private bool _SMSSTATUS;
        private decimal? mAXDAILYDATAONROAMING;
        private int? _PREPAIDBALANCELOWERSENDSMSSTATUS;
        private int? _PREPAIDBALANCEEXHAUSIONSENDSMSSTATUS;
        private int? _POSTPAIDCREDITLOWERSENDSMSSTATUS;
        private int? _POSTPAIDCREDITEXHAUSIONSENDSMSSTATUS;
        private DateTime? _CHANGEPREPAIDBALANCELOWERSENDSMSSTATUSTIME;
        private DateTime? _CHANGEPREPAIDBALANCEEXHAUSIONSENDSMSSTATUSTIME;
        private DateTime? _CHANGEPOSTPAIDCREDITLOWERSENDSMSSTATUSTIME;
        private DateTime? _CHANGEPOSTPAIDCREDITEXHAUSIONSENDSMSSTATUSTIME;
        #endregion


        #region 属性

        public virtual int? PREPAIDBALANCELOWERSENDSMSSTATUS
        {
            get { return _PREPAIDBALANCELOWERSENDSMSSTATUS; }
            set { _PREPAIDBALANCELOWERSENDSMSSTATUS = value; }
        }
        public virtual int? PREPAIDBALANCEEXHAUSIONSENDSMSSTATUS
        {
            get { return _PREPAIDBALANCEEXHAUSIONSENDSMSSTATUS; }
            set { _PREPAIDBALANCEEXHAUSIONSENDSMSSTATUS = value; }
        }
        public virtual int? POSTPAIDCREDITLOWERSENDSMSSTATUS
        {
            get { return _POSTPAIDCREDITLOWERSENDSMSSTATUS; }
            set { _POSTPAIDCREDITLOWERSENDSMSSTATUS = value; }
        }
        public virtual int? POSTPAIDCREDITEXHAUSIONSENDSMSSTATUS
        {
            get { return _POSTPAIDCREDITEXHAUSIONSENDSMSSTATUS; }
            set { _POSTPAIDCREDITEXHAUSIONSENDSMSSTATUS = value; }
        }

        public virtual DateTime? CHANGEPREPAIDBALANCELOWERSENDSMSSTATUSTIME
        {
            get { return _CHANGEPREPAIDBALANCELOWERSENDSMSSTATUSTIME; }
            set { _CHANGEPREPAIDBALANCELOWERSENDSMSSTATUSTIME = value; }
        }

        public virtual DateTime? CHANGEPREPAIDBALANCEEXHAUSIONSENDSMSSTATUSTIME
        {
            get { return _CHANGEPREPAIDBALANCEEXHAUSIONSENDSMSSTATUSTIME; }
            set { _CHANGEPREPAIDBALANCEEXHAUSIONSENDSMSSTATUSTIME = value; }
        }

        public virtual DateTime? CHANGEPOSTPAIDCREDITLOWERSENDSMSSTATUSTIME
        {
            get { return _CHANGEPOSTPAIDCREDITLOWERSENDSMSSTATUSTIME; }
            set { _CHANGEPOSTPAIDCREDITLOWERSENDSMSSTATUSTIME = value; }
        }

        public virtual DateTime? CHANGEPOSTPAIDCREDITEXHAUSIONSENDSMSSTATUSTIME
        {
            get { return _CHANGEPOSTPAIDCREDITEXHAUSIONSENDSMSSTATUSTIME; }
            set { _CHANGEPOSTPAIDCREDITEXHAUSIONSENDSMSSTATUSTIME = value; }
        }

        public virtual int PROPERTYID
        {
            get { return _PROPERTYID; }
            set { _PROPERTYID = value; }
        }

        public virtual CrmCustomersResourceMbInfo CrmCustomersResourceMbInfo
        {
            get { return _CRM_CUSTOMERS_RESOURCEMB; }
            set { _CRM_CUSTOMERS_RESOURCEMB = value; }
        }

        public virtual bool SMSSTATUS
        {
            get { return _SMSSTATUS; }
            set { _SMSSTATUS = value; }
        }

        public virtual decimal? MAXDAILYDATAONROAMING
        {
            get { return mAXDAILYDATAONROAMING; }
            set{mAXDAILYDATAONROAMING=value;}
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

    }
}
