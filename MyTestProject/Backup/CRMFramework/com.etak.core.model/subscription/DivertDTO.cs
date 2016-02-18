namespace com.etak.core.model.subscription
{
    /// <summary>
    /// Now wraped operation to be implemented internal
    /// </summary>
    /// <author>IgnasiG</author>
    /// <datetime>24/10/2014-12:38</datetime>
    public class DivertDTO
    {
        /// <summary>
        /// Msidn of the Divert
        /// </summary>
        public string MSISDN { get; set; }

        /// <summary>
        /// Divert Service Object DTO object
        /// </summary>
        public DivertServiceDTO Service;
        /// <summary>
        /// Active flag
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Wait Time
        /// </summary>
        public int WaitTime { get; set; }
    }
}
