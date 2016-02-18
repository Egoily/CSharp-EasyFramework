namespace com.etak.core.model.operation.messages
{
    /// <summary>
    /// The base type for any reposne of a bussiness operation that is an order
    /// </summary>
    abstract public class CreateNewOrderResponse : ResponseBase
    {
        /// <summary>
        /// The elephant talk order
        /// </summary>
        public virtual Order CreatedOrder { get; set; }
    }
}
