using com.etak.core.model;
using com.etak.core.model.operation.contract.amount;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.BenefitTransfer
{
    /// <summary>
    /// Input request for BenefitTransfer input in CORE model 
    /// </summary>
    public class BenefitTransferRequestInternal : CreateNewOrderRequest, IJointCustomerBasedRequest, IAmountBasedRequest
    {
        /// <summary>
        /// DestinationCustomerInfo
        /// </summary>
        public CustomerInfo DestinationCustomerInfo { get; set; }

        /// <summary>
        /// SourceCustomerInfo
        /// </summary>
        public CustomerInfo SourceCustomerInfo { get; set; }


        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// transfer type
        /// </summary>
        public int TransferType { get; set; }
    }
}
