using com.etak.core.model.operation.contract.amount;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.BenefitTransfer
{
    /// <summary>
    /// Class for BenefitTransfer request  in DTO model 
    /// </summary>
    public class BenefitTransferRequestDTO : OrderRequestDTO, IJointCustomerIdDTOBasedRequest,IAmountBasedDTORequest
    {
        /// <summary>
        /// DestinationCustomerId
        /// </summary>
        public int DestinationCustomerId { get; set; }

        /// <summary>
        /// SourceCustomerId
        /// </summary>
        public int SourceCustomerId { get; set; }

        /// <summary>
        /// amount
        /// </summary>
        public decimal amount { get; set; }


        /// <summary>
        /// transfer type
        /// </summary>
        public int TransferType { get; set; }
    }
}
