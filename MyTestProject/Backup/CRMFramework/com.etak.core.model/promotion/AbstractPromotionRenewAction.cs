using com.etak.core.model.revenueManagement;

namespace com.etak.core.model
{
    public abstract class AbstractPromotionRenewAction
    {
        public virtual int Id { get; set; }
        public virtual RmPromotionPlanDetailInfo PreRefferredPromotion { get; set; }
        public virtual RmPromotionPlanDetailInfo RefferredPromotion { get; set; }
        public virtual int Priority { get; set; }
        public virtual string ActionType { get; set; }
        public virtual string ConfigValue1 { get; set; }
        public virtual string ConfigValue2 { get; set; }
        public abstract void Renew(CustomerInfo info, BillRun oblBillRun, BillRun newBillRun);
    }
}
