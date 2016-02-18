using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-12-24 12:56:22
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-12-24 12:56:22
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-12-24 12:56:22
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-12-24 12:56:22
    /// </summary>
    [DataContract]
    [Serializable]
    public class DealerRatePlanInfo
    {
        #region 构造函数
        public DealerRatePlanInfo()
        { }

        public DealerRatePlanInfo(int? RATEPLANDETAILID, int? CUSTOMERID, int? CALLTYPEID, int? TIMECATEGORYID, int? SERVICETYPEID, int? SUBSERVICETYPEID, string OPERATORID, int? CURRENCYID, int? UNITCATEGORYID, string TARIFFCODE, decimal? TARIFF1, decimal? TARIFF2, decimal? SETUP1, decimal? TARIFF3, decimal? TARIFF4, decimal? SETUP2, decimal? CB, DateTime? STARTDATE, DateTime? ENDDATE)
        {
            this._RATEPLANDETAILID = RATEPLANDETAILID;
            this._DealerId = CUSTOMERID;
            this._CALLTYPEID = CALLTYPEID;
            this._TIMECATEGORYID = TIMECATEGORYID;
            this._SERVICETYPEID = SERVICETYPEID;
            this._SUBSERVICETYPEID = SUBSERVICETYPEID;
            this._OPERATORID = OPERATORID;
            this._CURRENCYID = CURRENCYID;
            this._UNITCATEGORYID = UNITCATEGORYID;
            this._TARIFFCODE = TARIFFCODE;
            this._TARIFF1 = TARIFF1;
            this._TARIFF2 = TARIFF2;
            this._SETUP1 = SETUP1;
            this._TARIFF3 = TARIFF3;
            this._TARIFF4 = TARIFF4;
            this._SETUP2 = SETUP2;
            this._CB = CB;
            this._STARTDATE = STARTDATE;
            this._ENDDATE = ENDDATE;
        }
        #endregion

        #region 成员
        private int? _RATEPLANDETAILID;
        private int? _DealerId;
        private int? _CALLTYPEID;
        private int? _TIMECATEGORYID;
        private int? _SERVICETYPEID;
        private int? _SUBSERVICETYPEID;
        private string _OPERATORID;
        private int? _CURRENCYID;
        private int? _UNITCATEGORYID;
        private string _TARIFFCODE;
        private decimal? _TARIFF1;
        private decimal? _TARIFF2;
        private decimal? _SETUP1;
        private decimal? _TARIFF3;
        private decimal? _TARIFF4;
        private decimal? _SETUP2;
        private decimal? _CB;
        private DateTime? _STARTDATE;
        private DateTime? _ENDDATE;
        #endregion


        #region 属性
        public int? RatePlanDetailId
        {
            get { return _RATEPLANDETAILID; }
            set { _RATEPLANDETAILID = value; }
        }

        public int? DealerId
        {
            get { return _DealerId; }
            set { _DealerId = value; }
        }

        public int? CallTypeId
        {
            get { return _CALLTYPEID; }
            set { _CALLTYPEID = value; }
        }

        public int? TimeCategoryId
        {
            get { return _TIMECATEGORYID; }
            set { _TIMECATEGORYID = value; }
        }

        public int? ServiceTypeId
        {
            get { return _SERVICETYPEID; }
            set { _SERVICETYPEID = value; }
        }

        public int? SubServiceTypeId
        {
            get { return _SUBSERVICETYPEID; }
            set { _SUBSERVICETYPEID = value; }
        }

        public string OperatorId
        {
            get { return _OPERATORID; }
            set { _OPERATORID = value; }
        }

        public int? CurrencyId
        {
            get { return _CURRENCYID; }
            set { _CURRENCYID = value; }
        }

        public int? UnitCategoryId
        {
            get { return _UNITCATEGORYID; }
            set { _UNITCATEGORYID = value; }
        }

        public string TariffCode
        {
            get { return _TARIFFCODE; }
            set { _TARIFFCODE = value; }
        }

        public decimal? Tariff1
        {
            get { return _TARIFF1; }
            set { _TARIFF1 = value; }
        }

        public decimal? Tariff2
        {
            get { return _TARIFF2; }
            set { _TARIFF2 = value; }
        }

        public decimal? Setup1
        {
            get { return _SETUP1; }
            set { _SETUP1 = value; }
        }

        public decimal? Tariff3
        {
            get { return _TARIFF3; }
            set { _TARIFF3 = value; }
        }

        public decimal? Tariff4
        {
            get { return _TARIFF4; }
            set { _TARIFF4 = value; }
        }

        public decimal? Setup2
        {
            get { return _SETUP2; }
            set { _SETUP2 = value; }
        }

        public decimal? CB
        {
            get { return _CB; }
            set { _CB = value; }
        }

        public DateTime? StartDate
        {
            get { return _STARTDATE; }
            set { _STARTDATE = value; }
        }

        public DateTime? EndDate
        {
            get { return _ENDDATE; }
            set { _ENDDATE = value; }
        }
        bool _DeleteFlag;
        public virtual bool DeleteFlag
        {
            get { return _DeleteFlag; }
            set { _DeleteFlag = value; }
        }
        #endregion


    }
}
