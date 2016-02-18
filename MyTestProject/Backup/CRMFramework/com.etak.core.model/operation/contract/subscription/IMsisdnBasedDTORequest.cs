using System;


namespace com.etak.core.model.operation.contract.subscription
{
    /// <summary>
    /// DTO request that is based on an input MSISDN
    /// </summary>
    public interface IMsisdnBasedDTORequest
    {
        /// <summary>
        /// The msisdn that the request is based on
        /// </summary>
        String MSISDN { get; set; }
    }
}
