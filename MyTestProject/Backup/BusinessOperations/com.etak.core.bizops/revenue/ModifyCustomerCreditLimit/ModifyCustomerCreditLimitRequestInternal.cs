using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.ModifyCustomerCreditLimit
{
    /// <summary>
    /// Request Internal of ModifyCustomerCreditLimitBizOp
    /// </summary>
    public class ModifyCustomerCreditLimitRequestInternal : CreateNewOrderRequest, ICustomerBasedRequest
    {
        /// <summary>
        /// Customer Info that is going to be modified his credit limit
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary>
        /// New Credit Limit
        /// </summary>
        public virtual decimal NewCreditLimit { get; set; }
 
    }
}
