using com.etak.core.model.operation;

namespace com.etak.core.bizops.revenue.CallDREQuerySubscriberPromotion
{
    /// <summary>
    /// Order of CallDREQuerySubscriberPromotionOrder
    /// </summary>
    public class CallDREQuerySubscriberPromotionOrder : Order
    {

        /// <summary>
        /// Discriminator for CallDREQuerySubscriberPromotionOrder
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.CallDREQuerySubscriberPromotionOrder; }

        }
    }
}
