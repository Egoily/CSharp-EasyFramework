namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// Responses in core model that include CustomerInfo property
    /// </summary>
    public interface ICustomerBasedResponse 
    {
        /// <summary>
        /// The customer as a result of the operation
        /// </summary>
        CustomerInfo Customer { get; set; }
    }
}
