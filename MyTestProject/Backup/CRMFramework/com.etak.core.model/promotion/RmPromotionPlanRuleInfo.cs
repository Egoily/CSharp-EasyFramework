using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-9-16 16:54:47
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-9-16 16:54:47
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-9-16 16:54:47
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-9-16 16:54:47
    /// </summary>
    [DataContract]
    [Serializable]
    public class RmPromotionPlanRuleInfo : PromotionModelBase
    {
        #region 构造函数
        public RmPromotionPlanRuleInfo()
        { }

        public RmPromotionPlanRuleInfo(RmPromotionPlanInfo rm_promotionplan, int ACTIVATIONTYPE, int SERVICETYPE, int SUBSERVICETYPE, int PACKAGEID, int BUSINESSTYPE, int CALCULATETYPE, int CALCULATEUNIT, decimal SCALAR, int RULEID, int RETURNMETHOD, int VALIDTIME, int REBATETYPE, decimal REBATEVALUE, int REBATEVALUEUNIT, int CURRENCYID, string RULEDESC, int SCANSTATUS, DateTime? LASTSCANTIME)
        {
            this._RM_PROMOTIONPLAN = rm_promotionplan;
            this._ACTIVATIONTYPE = ACTIVATIONTYPE;
            this._SERVICETYPE = SERVICETYPE;
            this._SUBSERVICETYPE = SUBSERVICETYPE;
            this._PACKAGEID = PACKAGEID;
            this._BUSINESSTYPE = BUSINESSTYPE;
            this._CALCULATETYPE = CALCULATETYPE;
            this._CALCULATEUNIT = CALCULATEUNIT;
            this._SCALAR = SCALAR;
            this._RULEID = RULEID;
            this._RETURNMETHOD = RETURNMETHOD;
            this._VALIDTIME = VALIDTIME;
            this._REBATETYPE = REBATETYPE;
            this._REBATEVALUE = REBATEVALUE;
            this._REBATEVALUEUNIT = REBATEVALUEUNIT;
            this._CURRENCYID = CURRENCYID;
            this._RULEDESC = RULEDESC;
            this._SCANSTATUS = SCANSTATUS;
            this._LASTSCANTIME = LASTSCANTIME;
        }
        #endregion

        #region 成员
        protected RmPromotionPlanInfo _RM_PROMOTIONPLAN;

        private int _PROMOTIONPLANRuleID;
        private int _PromotionPlanId;
        private int _ACTIVATIONTYPE;
        private int _SERVICETYPE;
        private int _SUBSERVICETYPE;
        private int _PACKAGEID;
        private int _BUSINESSTYPE;
        private int _CALCULATETYPE;
        private int _CALCULATEUNIT;
        private decimal _SCALAR;
        private int _RULEID;
        private int _RETURNMETHOD;
        private int _VALIDTIME;
        private int _REBATETYPE;
        private decimal _REBATEVALUE;
        private int _REBATEVALUEUNIT;
        private int _CURRENCYID;
        private string _RULEDESC;
        private int _SCANSTATUS;
        private DateTime? _LASTSCANTIME = null;
        #endregion


        #region 属性

        public RmPromotionPlanInfo RmPromotionPlanInfo
        {
            get { return _RM_PROMOTIONPLAN; }
            set { _RM_PROMOTIONPLAN = value; }
        }
        
        public int PromotionPlanRuleId
        {
            get { return _PROMOTIONPLANRuleID; }
            set { _PROMOTIONPLANRuleID = value; }
        }
        public int PromotionPlanId
        {
            get { return _PromotionPlanId; }
            set { _PromotionPlanId = value; }
        }

        public int ActivationType
        {
            get { return _ACTIVATIONTYPE; }
            set { _ACTIVATIONTYPE = value; }
        }

        public int ServiceType
        {
            get { return _SERVICETYPE; }
            set { _SERVICETYPE = value; }
        }

        public int SubServiceType
        {
            get { return _SUBSERVICETYPE; }
            set { _SUBSERVICETYPE = value; }
        }

        public int PackageId
        {
            get { return _PACKAGEID; }
            set { _PACKAGEID = value; }
        }

        public int BusinessType
        {
            get { return _BUSINESSTYPE; }
            set { _BUSINESSTYPE = value; }
        }

        public int CalculateType
        {
            get { return _CALCULATETYPE; }
            set { _CALCULATETYPE = value; }
        }

        public int CalculateUnit
        {
            get { return _CALCULATEUNIT; }
            set { _CALCULATEUNIT = value; }
        }

        public decimal Scalar
        {
            get { return _SCALAR; }
            set { _SCALAR = value; }
        }

        public int RuleId
        {
            get { return _RULEID; }
            set { _RULEID = value; }
        }

        public int ReturnMethod
        {
            get { return _RETURNMETHOD; }
            set { _RETURNMETHOD = value; }
        }

        public int ValidTime
        {
            get { return _VALIDTIME; }
            set { _VALIDTIME = value; }
        }

        public int RebateType
        {
            get { return _REBATETYPE; }
            set { _REBATETYPE = value; }
        }

        public decimal RebateValue
        {
            get { return _REBATEVALUE; }
            set { _REBATEVALUE = value; }
        }

        public int RebateValueUnit
        {
            get { return _REBATEVALUEUNIT; }
            set { _REBATEVALUEUNIT = value; }
        }

        public int CurrencyId
        {
            get { return _CURRENCYID; }
            set { _CURRENCYID = value; }
        }

        public string RuleDesc
        {
            get { return _RULEDESC; }
            set { _RULEDESC = value; }
        }

        public int ScanStatus
        {
            get { return _SCANSTATUS; }
            set { _SCANSTATUS = value; }
        }

        public DateTime? LastScanTime
        {
            get { return _LASTSCANTIME; }
            set { _LASTSCANTIME = value; }
        }

        #endregion

    }
}
