using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-9-16 17:53:04
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-9-16 17:53:04
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-9-16 17:53:04
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-9-16 17:53:04
    /// </summary>
    [DataContract]
    [Serializable]
    public class CrmCustomersPromotionInfo : PromotionModelBase
    {
       
        public CrmCustomersPromotionInfo()
        {
            this.Customer = new CustomerInfo();
            this.PromotionDetail = new RmPromotionPlanDetailInfo();
        }

        public CrmCustomersPromotionInfo(CustomerInfo customer, RmPromotionPlanDetailInfo PROMOTIONPLANDETAIL, decimal CURRENTLIMIT, bool ACTIVE, DateTime? STARTDATE, DateTime? ENDDATE)
        {
            this.Customer = customer;
            this.PromotionDetail = PROMOTIONPLANDETAIL;
            this.CurrentLimit = CURRENTLIMIT;
            this.Active = ACTIVE;
            this.StartDate = STARTDATE;
            this.EndDate = ENDDATE;
        }




        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            CrmCustomersPromotionInfo value = obj as CrmCustomersPromotionInfo;

            if (ReferenceEquals(null, value))
                return false;

            if (value.PromotionId == this.PromotionId && value.StartDate == this.StartDate && value.EndDate == this.EndDate)
                return true;


            return false;
        }

        public override int GetHashCode()
        {
            return(PromotionId.GetHashCode());
        }

     
        virtual public long PromotionId { get; set; }
        virtual public string WhiteList { get; set; }
        virtual public int CustomerId { get; set; }        
        virtual public bool IsBasePromotion { get; set; }
        virtual public decimal CurrentLimit { get; set; }
        virtual public bool Active { get; set; }
        virtual public DateTime? StartDate { get; set; }
        virtual public DateTime? EndDate { get; set; }
        virtual public DateTime? FirstUsed { get; set; }      
        virtual public int RenewalCount { get; set; }
        virtual public int RenewAutomatically { get; set; }
        virtual public int DeActiveReason { get; set; }
        virtual public string BatchNo { get; set; }
        virtual public long BatchId { get; set; }
        virtual public DateTime? RenewDate { get; set; }
        virtual public int? Priority { get; set; }
        virtual public int? ActiveWithoutCredit { get; set; }

        virtual public CustomerInfo Customer { get; set; }
        public virtual RmPromotionPlanDetailInfo PromotionDetail { get; set; }
        public virtual string MSISDN { get; set; }
        public virtual decimal InitialLimit { get; set; }
        public virtual DateTime? NextRenewDate { get; set; }
        public virtual DateTime? PreRenewalActionsDate { get; set; }
        public virtual int? NextPeriodNumber { get; set; }
        public virtual int? CurrentCycleNumber { get; set; }
        public virtual bool PreActionsExecuted { get; set; }
        public virtual bool ActionsExecuted { get; set; }
    }
}
