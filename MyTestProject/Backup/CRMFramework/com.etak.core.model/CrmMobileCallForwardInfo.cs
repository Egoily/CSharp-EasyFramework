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
    public class CrmMobileCallForwardInfo : ModelBase
    {
        #region 构造函数
        public CrmMobileCallForwardInfo()
        { }

        public CrmMobileCallForwardInfo(int CALLFORWARDID, CrmCustomersResourceMbInfo crm_customers_resourcemb, int CALLFORWARDTYPE, int BSGID, bool PROVISIONSTATE, bool NOTIFYTOCGPARTY, bool PRESENTMSISDN, bool NOTIFYTOFWDINGPARTY, int ACTSTATE, string FTN, string FTNSUBADDR, string DEFAULTFTN, string NOREPLYCONDTIMER)
        {
            this._CALLFORWARDID = CALLFORWARDID;
            this._CRM_CUSTOMERS_RESOURCEMB = crm_customers_resourcemb;
            this._CALLFORWARDTYPE = CALLFORWARDTYPE;
            this._BSGID = BSGID;
            this._PROVISIONSTATE = PROVISIONSTATE;
            this._NOTIFYTOCGPARTY = NOTIFYTOCGPARTY;
            this._PRESENTMSISDN = PRESENTMSISDN;
            this._NOTIFYTOFWDINGPARTY = NOTIFYTOFWDINGPARTY;
            this._ACTSTATE = ACTSTATE;
            this._FTN = FTN;
            this._FTNSUBADDR = FTNSUBADDR;
            this._DEFAULTFTN = DEFAULTFTN;
            this._NOREPLYCONDTIMER = NOREPLYCONDTIMER;
        }
        #endregion

        #region 成员
        private int _CALLFORWARDID;
        protected CrmCustomersResourceMbInfo _CRM_CUSTOMERS_RESOURCEMB;
        private int _CALLFORWARDTYPE;
        private int _BSGID;
        private bool _PROVISIONSTATE;
        private bool _NOTIFYTOCGPARTY;
        private bool _PRESENTMSISDN;
        private bool _NOTIFYTOFWDINGPARTY;
        private int _ACTSTATE;
        private string _FTN;
        private string _FTNSUBADDR;
        private string _DEFAULTFTN;
        private string _NOREPLYCONDTIMER;
        #endregion


        #region 属性
        public virtual int CALLFORWARDID
        {
            get { return _CALLFORWARDID; }
            set { _CALLFORWARDID = value; }
        }

        public virtual CrmCustomersResourceMbInfo CrmCustomersResourceMbInfo
        {
            get { return _CRM_CUSTOMERS_RESOURCEMB; }
            set { _CRM_CUSTOMERS_RESOURCEMB = value; }
        }

        public virtual int CALLFORWARDTYPE
        {
            get { return _CALLFORWARDTYPE; }
            set { _CALLFORWARDTYPE = value; }
        }

        public virtual int BSGID
        {
            get { return _BSGID; }
            set { _BSGID = value; }
        }

        public virtual bool PROVISIONSTATE
        {
            get { return _PROVISIONSTATE; }
            set { _PROVISIONSTATE = value; }
        }

        public virtual bool NOTIFYTOCGPARTY
        {
            get { return _NOTIFYTOCGPARTY; }
            set { _NOTIFYTOCGPARTY = value; }
        }

        public virtual bool PRESENTMSISDN
        {
            get { return _PRESENTMSISDN; }
            set { _PRESENTMSISDN = value; }
        }

        public virtual bool NOTIFYTOFWDINGPARTY
        {
            get { return _NOTIFYTOFWDINGPARTY; }
            set { _NOTIFYTOFWDINGPARTY = value; }
        }

        public virtual int ACTSTATE
        {
            get { return _ACTSTATE; }
            set { _ACTSTATE = value; }
        }

        public virtual string FTN
        {
            get { return _FTN; }
            set { _FTN = value; }
        }

        public virtual string FTNSUBADDR
        {
            get { return _FTNSUBADDR; }
            set { _FTNSUBADDR = value; }
        }

        public virtual string DEFAULTFTN
        {
            get { return _DEFAULTFTN; }
            set { _DEFAULTFTN = value; }
        }

        public virtual string NOREPLYCONDTIMER
        {
            get { return _NOREPLYCONDTIMER; }
            set { _NOREPLYCONDTIMER = value; }
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
