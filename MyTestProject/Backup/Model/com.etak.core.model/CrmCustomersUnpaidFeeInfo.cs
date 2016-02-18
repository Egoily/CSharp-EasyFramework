using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009-9-16 18:45:05
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009-9-16 18:45:05
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009-9-16 18:45:05
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009-9-16 18:45:05
    /// </summary>
    [DataContract]
    [Serializable]
    public class CrmCustomersUnpaidFeeInfo : PromotionModelBase
    {
        public virtual int PromotionPlanId { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual RmPromotionPlanInfo RmPromotionPlanInfo { get; set; }
        public virtual DateTime? FeeDate { get; set; }
        public virtual int FeeType { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual int StatusId { get; set; }
        public virtual DateTime? TryChargeDate { get; set; }
        public virtual string Remark { get; set; }
        public virtual long BatchId { get; set; }
       

        public override bool Equals(object obj)
        {
            CrmCustomersUnpaidFeeInfo value = obj as CrmCustomersUnpaidFeeInfo;
            if (value != null && value.CustomerId == this.CustomerId &&
                value.PromotionPlanId == this.PromotionPlanId && value.FeeDate == this.FeeDate)
            {
                return true;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
