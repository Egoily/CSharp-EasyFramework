using com.etak.core.model.operation.messages;


namespace com.etak.core.bizops.revenue.CallDREQuerySubscriberPromotion
{

    /// <summary>
    /// CallDREQuerySubscriberPromotionRequestDTO
    /// </summary>
    public class CallDREQuerySubscriberPromotionRequestDTO : OrderRequestDTO
    {
        /// <summary>
        /// msisdn
        /// </summary>
        public string msisdn { get; set; }

        /// <summary>
        /// promotionId
        /// </summary>
        public string promotionId { get; set; }


        /// <summary>
        /// millionSecond
        /// </summary>
        public int millionSecond { get; set; }

        /// <summary>
        /// serverURL
        /// </summary>
        public string serverURL { get; set; }

    }
}
