using System;


namespace com.etak.core.model.operation.contract.simcard
{
    /// <summary>
    /// DTO request that is based on an ICCID
    /// </summary>
    public interface IICCIDBasedDTORequest 
    {
        /// <summary>
        /// The ICCCID that the request is based on
        /// </summary>
        String ICCID { get; set; }
    }
}
