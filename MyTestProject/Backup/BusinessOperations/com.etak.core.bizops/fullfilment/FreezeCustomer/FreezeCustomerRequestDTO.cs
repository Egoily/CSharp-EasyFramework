
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.FreezeCustomer
{
    /// <summary>
    /// Class for FreezeCustomer request  in DTO model 
    /// </summary>
    public class FreezeCustomerRequestDTO : OrderRequestDTO, IMsisdnBasedDTORequest 
    {
        /// <summary>
        /// Msisdn used to get customer
        /// </summary>
        public string MSISDN { get; set; }
    }
}
