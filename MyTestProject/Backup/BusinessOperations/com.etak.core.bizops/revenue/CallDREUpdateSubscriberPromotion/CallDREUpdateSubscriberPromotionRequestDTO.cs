using com.etak.core.model.operation.messages;


namespace com.etak.core.bizops.revenue.CallDREUpdateSubscriberPromotion
{

    /// <summary>
    /// CallDREQuerySubscriberPromotionRequestDTO
    /// </summary>
    public class CallDREUpdateSubscriberPromotionRequestDTO : OrderRequestDTO
    {
        /// <summary>
        /// Msisdn
        /// </summary>
        public string Msisdn { get; set; }

        /// <summary>
        /// PromotionId
        /// </summary>
        public long PromotionId { get; set; }

        /// <summary>
        /// IncrementValue
        /// </summary>
        public float IncrementValue { get; set; }

        /// <summary>
        /// DecrementValue
        /// </summary>
        public float DecrementValue { get; set; }

        /// <summary>
        /// serverURL
        /// </summary>
        public string ServerURL { get; set; }


    }
}
