namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// Core Request that is based/contains CustomerInfo
    /// </summary>
    public interface ICustomerBasedRequest 
    {
        /// <summary>
        /// Core model CustomerInfo of the request.
        /// </summary>
        CustomerInfo Customer { get; set; }
    }
}
