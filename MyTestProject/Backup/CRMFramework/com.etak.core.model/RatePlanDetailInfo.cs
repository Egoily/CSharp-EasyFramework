using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class RatePlanDetailInfo
    {
        #region 成员
        private int? _RatePlanDetailID = null;
        private int? _RatePlanID = null;
        private string _CountryCode;
        private int? _ProviderID = null;
        private int? _CallTypeID = null;
        private int? _CallDirectionID = null;
        private int? _TimeCategoryID = null;
        private int? _UnitCategoryID = null;
        private int? _CurrencyID = null;
        private decimal? _Tariff1 = null;
        private decimal? _Tariff2 = null;
        private decimal? _Setup = null;
        private DateTime? _StartDate = null;
        private DateTime? _EndDate = null;
        private decimal? _Prompt = null;
        private int? _Zone1 = null;
        private int? _Zone2 = null;
        private bool? _Preferred;
        private string _OperatorCode;
        private int? _Quality = 0;
        private int? _RatePlanCategoryID = null;
        private int? _Restriction = null;
        #endregion


        #region Attribuer
        public int? Restriction
        {
            get { return _Restriction; }
            set { _Restriction = value; }
        }

        public int? RatePlanDetailID
        {
            get { return _RatePlanDetailID; }
            set { _RatePlanDetailID = value; }
        }

        public int? RatePlanID
        {
            get { return _RatePlanID; }
            set { _RatePlanID = value; }
        }
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }

        public int? ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; }
        }

        public int? CallTypeID
        {
            get { return _CallTypeID; }
            set { _CallTypeID = value; }
        }

        public int? CallDirectionID
        {
            get { return _CallDirectionID; }
            set { _CallDirectionID = value; }
        }

        public int? TimeCategoryID
        {
            get { return _TimeCategoryID; }
            set { _TimeCategoryID = value; }
        }

        public int? UnitCategoryID
        {
            get { return _UnitCategoryID; }
            set { _UnitCategoryID = value; }
        }

        public int? CurrencyID
        {
            get { return _CurrencyID; }
            set { _CurrencyID = value; }
        }

        public decimal? Tariff1
        {
            get { return _Tariff1; }
            set { _Tariff1 = value; }
        }

        public decimal? Tariff2
        {
            get { return _Tariff2; }
            set { _Tariff2 = value; }
        }

        public decimal? Setup
        {
            get { return _Setup; }
            set { _Setup = value; }
        }

        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public decimal? Prompt
        {
            get { return _Prompt; }
            set { _Prompt = value; }
        }

        public int? Zone1
        {
            get { return _Zone1; }
            set { _Zone1 = value; }
        }

        public int? Zone2
        {
            get { return _Zone2; }
            set { _Zone2 = value; }
        }

        public bool? Preferred
        {
            get { return _Preferred; }
            set { _Preferred = value; }
        }

        public string OperatorCode
        {
            get { return _OperatorCode; }
            set { _OperatorCode = value; }
        }

        public int? Quality
        {
            get { return _Quality; }
            set { _Quality = value; }
        }

        public int? RatePlanCategoryID
        {
            get { return _RatePlanCategoryID; }
            set { _RatePlanCategoryID = value; }
        }
        #endregion
    }
}