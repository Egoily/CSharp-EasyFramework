using com.etak.core.model.operation;

namespace com.etak.core.bizops.revenue.CallDREUpdateSubscriberPromotion
{
    /// <summary>
    ///Order of  CallDREQuerySubscriberPromotionOrder
    /// </summary>
    public class CallDREUpdateSubscriberPromotionOrder : Order
    {

        /// <summary>
        /// Discriminator for CallDREUpdateSubscriberPromotionOrder
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.CallDREUpdateSubscriberPromotionOrder; }

        }
    }
}
