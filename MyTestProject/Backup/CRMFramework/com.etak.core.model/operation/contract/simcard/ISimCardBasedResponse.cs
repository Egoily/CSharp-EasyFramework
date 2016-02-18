
namespace com.etak.core.model.operation.contract.simcard
{
    /// <summary>
    /// Core response that contains a sim card (SimCardInfo)
    /// </summary>
    public interface ISimCardBasedResponse
    {
        /// <summary>
        /// The sim card on wich the request is based
        /// </summary>
        SIMCardInfo SimCard { get; set; }
    }
}
