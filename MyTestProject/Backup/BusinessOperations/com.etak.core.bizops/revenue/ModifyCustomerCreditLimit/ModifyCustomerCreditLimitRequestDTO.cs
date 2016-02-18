using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.ModifyCustomerCreditLimit
{
    /// <summary>
    /// Order request DTO of ModifyCustomerCreditLimitBizOp
    /// </summary>
    public class ModifyCustomerCreditLimitRequestDTO : OrderRequestDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// CustomerDTO whose Credit Limit will be modified 
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// New Credit Limit
        /// </summary>
        public decimal NewCreditLimit { get; set; }
    }
}
