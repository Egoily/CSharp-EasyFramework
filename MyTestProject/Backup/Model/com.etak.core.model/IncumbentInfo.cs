using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2010/9/30 14:07:27
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2010/9/30 14:07:27
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2010/9/30 14:07:27
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2010/9/30 14:07:27
    /// </summary>
    [DataContract]
    [Serializable]
    public class IncumbentInfo
    {
        #region 构造函数
        public IncumbentInfo()
        { }

        public IncumbentInfo(int ID, int CUSTOMERID, int SERVICETYPEID, int SUBSERVICETYPEID, string TARIFFCODE, string OPERATORID, string CARRIER, int CALLTYPEID, int TIMECATEGORYID, int UNITCATEGORYID1, int UNITCATEGORYID2, decimal TARIFF1, decimal TARIFF2, decimal TARIFF3, decimal TARIFF4, decimal SETUP1, decimal SETUP2, decimal CB, int CURRENCYID, DateTime? STARTDATE, DateTime? ENDDATE)
        {
            this._ID = ID;
            this._CUSTOMERID = CUSTOMERID;
            this._SERVICETYPEID = SERVICETYPEID;
            this._SUBSERVICETYPEID = SUBSERVICETYPEID;
            this._TARIFFCODE = TARIFFCODE;
            this._OPERATORID = OPERATORID;
            this._CARRIER = CARRIER;
            this._CALLTYPEID = CALLTYPEID;
            this._TIMECATEGORYID = TIMECATEGORYID;
            this._UNITCATEGORYID1 = UNITCATEGORYID1;
            this._UNITCATEGORYID2 = UNITCATEGORYID2;
            this._TARIFF1 = TARIFF1;
            this._TARIFF2 = TARIFF2;
            this._TARIFF3 = TARIFF3;
            this._TARIFF4 = TARIFF4;
            this._SETUP1 = SETUP1;
            this._SETUP2 = SETUP2;
            this._CB = CB;
            this._CURRENCYID = CURRENCYID;
            this._STARTDATE = STARTDATE;
            this._ENDDATE = ENDDATE;
        }
        #endregion

        #region 成员
        private int _ID;
        private int _CUSTOMERID;
        private int _SERVICETYPEID;
        private int _SUBSERVICETYPEID;
        private string _TARIFFCODE;
        private string _OPERATORID;
        private string _CARRIER;
        private int _CALLTYPEID;
        private int _TIMECATEGORYID;
        private int _UNITCATEGORYID1;
        private int _UNITCATEGORYID2;
        private decimal _TARIFF1;
        private decimal _TARIFF2;
        private decimal _TARIFF3;
        private decimal _TARIFF4;
        private decimal _SETUP1;
        private decimal _SETUP2;
        private decimal _CB;
        private int _CURRENCYID;
        private DateTime? _STARTDATE;
        private DateTime? _ENDDATE;
        #endregion


        #region 属性
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int CustomerId
        {
            get { return _CUSTOMERID; }
            set { _CUSTOMERID = value; }
        }

        public int DealerId
        {
            get;
            set;
        }

        public int ServiceTypeId
        {
            get { return _SERVICETYPEID; }
            set { _SERVICETYPEID = value; }
        }

        public int SubServiceTypeId
        {
            get { return _SUBSERVICETYPEID; }
            set { _SUBSERVICETYPEID = value; }
        }

        public string TariffCode
        {
            get { return _TARIFFCODE; }
            set { _TARIFFCODE = value; }
        }

        public string OperatorId
        {
            get { return _OPERATORID; }
            set { _OPERATORID = value; }
        }

        public string OperatorName
        {
            get;
            set;
        }

        public string TimeCategoryName
        {
            get;
            set;
        }
        public string CallTypeName
        {
            get;
            set;
        }


        public string  Carrier
        {
            get { return _CARRIER; }
            set { _CARRIER = value; }
        }
        int _TypeId;
        public int TypeId
        {
            get { return _TypeId; }
            set { _TypeId = value; }
        }

        public int CallTypeId
        {
            get { return _CALLTYPEID; }
            set { _CALLTYPEID = value; }
        }

        public int TimeCategoryId
        {
            get { return _TIMECATEGORYID; }
            set { _TIMECATEGORYID = value; }
        }

        public int UnitCategoryId1
        {
            get { return _UNITCATEGORYID1; }
            set { _UNITCATEGORYID1 = value; }
        }

        public int UnitCategoryId2
        {
            get { return _UNITCATEGORYID2; }
            set { _UNITCATEGORYID2 = value; }
        }
        public string UnitCategoryName1
        {
            get;
            set;
        }

        public string UnitCategoryName2
        {
            get;
            set;
        }
        public string SubServiceTypeName
        {
            get;
            set;
        }
        public decimal Tariff1
        {
            get { return _TARIFF1; }
            set { _TARIFF1 = value; }
        }

        public decimal Tariff2
        {
            get { return _TARIFF2; }
            set { _TARIFF2 = value; }
        }

        public decimal Tariff3
        {
            get { return _TARIFF3; }
            set { _TARIFF3 = value; }
        }

        public decimal Tariff4
        {
            get { return _TARIFF4; }
            set { _TARIFF4 = value; }
        }

        public decimal Setup1
        {
            get { return _SETUP1; }
            set { _SETUP1 = value; }
        }

        public decimal Setup2
        {
            get { return _SETUP2; }
            set { _SETUP2 = value; }
        }

        public decimal CB
        {
            get { return _CB; }
            set { _CB = value; }
        }

        public int CurrencyId
        {
            get { return _CURRENCYID; }
            set { _CURRENCYID = value; }
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

        #endregion

    }
}
