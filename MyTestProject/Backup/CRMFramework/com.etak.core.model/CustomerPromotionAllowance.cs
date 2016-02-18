using System;

namespace com.etak.core.model
{
    public class CustomerPromotionAllowance
    {
        public virtual long PromotionAllowanceId { get; set; }
        public virtual CrmCustomersPromotionInfo CustomerPromotion { get; set; }
        public virtual DateTime PeriodStart { get; set; }
        public virtual DateTime? PeriodEnd { get; set; }
        public virtual decimal Allowance { get; set; }
        public virtual decimal InitialAllowance { get; set; }
    }
}
