
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.UnFreezeCustomer
{
    /// <summary>
    /// Class for UnFreezeCustomer request  in DTO model 
    /// </summary>
    public class UnFreezeCustomerRequestDTO : OrderRequestDTO, IMsisdnBasedDTORequest
    {
        /// <summary>
        /// Msisdn used to get customer
        /// </summary>
        public string MSISDN { get; set; }
    }
}
