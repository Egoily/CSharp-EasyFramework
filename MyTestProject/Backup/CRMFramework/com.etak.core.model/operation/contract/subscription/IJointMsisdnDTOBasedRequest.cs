using System;

namespace com.etak.core.model.operation.contract.subscription
{
    /// <summary>
    /// DTO Request to perform a transfer or operation
    /// based on two MSISDNs
    /// </summary>
    public interface IJointMsisdnDTOBasedRequest
    {
        /// <summary>
        /// The source MSISDN to perform the operation
        /// </summary>
        String SourceMSISDN { get; set; }

        /// <summary>
        /// The destination MSISDN to perform the operation
        /// </summary>
        String DestinationMSISDN { get; set; }
    }
}
