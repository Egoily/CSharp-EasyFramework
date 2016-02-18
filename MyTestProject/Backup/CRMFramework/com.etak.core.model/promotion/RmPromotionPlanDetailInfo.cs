using System;
using System.Collections.Generic;
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
    public class RmPromotionPlanDetailInfo : PromotionModelBase
    {
        #region 构造函数
        public RmPromotionPlanDetailInfo()
        {
            this.RmPromotionPlanInfo = new RmPromotionPlanInfo();
        }

        public RmPromotionPlanDetailInfo(int PROMOTIONPLANDETAILID, RmPromotionPlanInfo rm_promotionplan, int PROMOTIONTYPEID, int SERVICETYPEID, int SUBSERVICETYPEID, int NUMBERCATEGORYID, int DATECATEGORYID, string COUNTRYCODE, int CALLDIRECTIONID, decimal LIMITPERCALL, decimal LIMITPERDAY, decimal LIMIT, int LIMITUNIT, int CURRENCYID, int PROMOTIONMETHODID, decimal SETUP, decimal PROMPT, decimal TARIFF1, decimal TARIFF2, int UNITCATEGORYID, int DISCOUNTMETHODID, DateTime? STARTDATE, DateTime? ENDDATE, int DATECATEGORYTYPEID, string WHITELIST, int OVERLIMITRATEPLANID, int BLACKLISTID)
        {
            this.PromotionPlanDetailId = PROMOTIONPLANDETAILID;
            this.RmPromotionPlanInfo = rm_promotionplan;
            this.PromotionTypeId = PROMOTIONTYPEID;
            this.ServiceTypeId = SERVICETYPEID;
            this.SubServiceTypeId = SUBSERVICETYPEID;
            this.NumberCategoryId = NUMBERCATEGORYID;
            this.DateCategoryId = DATECATEGORYID;
            this.CountryCode = COUNTRYCODE;
            this.CallDirectionId = CALLDIRECTIONID;
            this.LimitPerCall = LIMITPERCALL;
            this.LimitPerDay = LIMITPERDAY;
            this.Limit = LIMIT;
            this.LimitUnit = LIMITUNIT;
            this.CurrencyId = CURRENCYID;
            this.PromotionMethodId = PROMOTIONMETHODID;
            this.Setup = SETUP;
            this.Prompt = PROMPT;
            this.Tariff1 = TARIFF1;
            this.Tariff2 = TARIFF2;
            this.UnitCategoryId = UNITCATEGORYID;
            this.DiscountMethodId = DISCOUNTMETHODID;
            this.StartDate = STARTDATE;
            this.EndDate = ENDDATE;
            this.DateCategoryId = DATECATEGORYTYPEID;
            this.WhiteList = WHITELIST;
            this.OverLimitRateplanId = OVERLIMITRATEPLANID;
            this.BlackListId = BLACKLISTID;

        }
        #endregion

        #region 成员
        private int? _RATEPLANID = -1;
        private int? _BASEPROMOTIONPLANDETAILID = -1;
        private decimal? _maximumAllowBalance = 0;
        #endregion

        #region 属性

        virtual public int PromotionPlanDetailId
        {
            get;
            set;
        }

        virtual public RmPromotionPlanInfo RmPromotionPlanInfo
        {
            get;
            set;
        }


        virtual public int PromotionTypeId
        {
            get;
            set;
        }


        virtual public int ServiceTypeId
        {
            get;
            set;
        }


        virtual public int SubServiceTypeId
        {
            get;
            set;
        }


        virtual public int NumberCategoryId
        {
            get;
            set;
        }


        virtual public int DateCategoryId
        {
            get;
            set;
        }


        virtual public string CountryCode
        {
            get;
            set;
        }


        virtual public int CallDirectionId
        {
            get;
            set;
        }


        virtual public decimal LimitPerCall
        {
            get;
            set;
        }


        virtual public decimal LimitPerDay
        {
            get;
            set;
        }


        virtual public decimal Limit
        {
            get;
            set;
        }


        virtual public int LimitUnit
        {
            get;
            set;
        }


        virtual public int CurrencyId
        {
            get;
            set;
        }


        virtual public int PromotionMethodId
        {
            get;
            set;
        }


        virtual public decimal Setup
        {
            get;
            set;
        }


        virtual public decimal Prompt
        {
            get;
            set;
        }


        virtual public decimal Tariff1
        {
            get;
            set;
        }


        virtual public decimal Tariff2
        {
            get;
            set;
        }


        virtual public int UnitCategoryId
        {
            get;
            set;
        }


        virtual public int DiscountMethodId
        {
            get;
            set;
        }


        virtual public DateTime? StartDate
        {
            get;
            set;
        }


        virtual public DateTime? EndDate
        {
            get;
            set;
        }


        virtual public int DateCategoryTypeId
        {
            get;
            set;
        }


        virtual public string WhiteList
        {
            get;
            set;
        }


        virtual public decimal UsageFee
        {
            get;
            set;
        }


        virtual public int ApplyOnRoaming
        {
            get;
            set;
        }


        virtual public bool ApplyOnSuperOnNet
        {
            get;
            set;
        }

        virtual public int? RatePlanId
        {
            get
            {
                return _RATEPLANID;
            }
            set
            {
                _RATEPLANID = value;
            }
        }

        virtual public int? BasePromotionPlanDetailId
        {
            get
            {
                return _BASEPROMOTIONPLANDETAILID;
            }
            set
            {
                _BASEPROMOTIONPLANDETAILID = value;
            }
        }

        string _PROMOTIONPLANDETAILNAME = "";
        virtual public string PromotionPlanDetailName
        {
            get
            {
                return _PROMOTIONPLANDETAILNAME;
            }
            set
            {
                _PROMOTIONPLANDETAILNAME = value;
            }
        }

        virtual public bool DeleteFlag
        {
            get;
            set;
        }


        virtual public int OverLimitRateplanId
        {
            get;
            set;
        }


        virtual public int BlackListId
        {
            get;
            set;
        }


        //added by neil at 2013/11/6
        virtual public decimal? MaximumAllowedBalance
        {
            get { return _maximumAllowBalance; }
            set { _maximumAllowBalance = value; }
        }

        virtual public Int32? WalletTypeId { get; set; }

        public virtual APIVisible APIVisible { get; set; }

        public virtual TimeUnits PeriodUnit { get; set; }
        public virtual int PeriodCount { get; set; }
        public virtual int StartPeriodNumber { get; set; }
        public virtual int EndPeriodNumber { get; set; }
        public virtual int Periodicity { get; set; }
        public virtual int CycleRepeatCount { get; set; }
        public virtual int PreRenewalActionsMinutesOffset { get; set; }
        public virtual IList<AbstractPromotionRenewAction> PreRenewActions { get; set; }
        public virtual IList<AbstractPromotionRenewAction> RenewActions { get; set; }
        public virtual decimal PricePerLimitUnit { get; set; }
        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            RmPromotionPlanDetailInfo objBoxed = obj as RmPromotionPlanDetailInfo;

            if (ReferenceEquals(null, objBoxed))
                return false;

            return (objBoxed.PromotionPlanDetailId.Equals(this.PromotionPlanDetailId));           
        }

        public override int GetHashCode()
        {
            return PromotionPlanDetailId.GetHashCode() + RatePlanId.GetHashCode();
        }


    }
}
