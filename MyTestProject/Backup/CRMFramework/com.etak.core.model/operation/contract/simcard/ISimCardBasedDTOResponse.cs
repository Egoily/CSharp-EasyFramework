using com.etak.core.model.resource;


namespace com.etak.core.model.operation.contract.simcard
{
    /// <summary>
    /// DTO response thaat contains a simcard
    /// </summary>
    public interface ISimCardBasedDTOResponse 
    {
        /// <summary>
        /// The simcard information
        /// </summary>
        SimCardDTO SimCard { get; set; }
    }
}
