using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.ReserveMsisdn
{
    /// <summary>
    /// The DTO request for ReserveMsisdn, based on Msisdn for automapping
    /// </summary>
    public class ReserveMsisdnRequestDTO : OrderRequestDTO, IMsisdnBasedDTORequest
    {
        /// <summary>
        /// The msisdn to be reserved
        /// </summary>
        public string MSISDN { get; set; }

    }
}
