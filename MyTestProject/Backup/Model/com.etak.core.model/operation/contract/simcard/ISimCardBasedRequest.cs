
namespace com.etak.core.model.operation.contract.simcard
{
    /// <summary>
    /// Core request that is based on a sim card (SimCardInfo)
    /// </summary>
    public interface ISimCardBasedRequest
    {
        /// <summary>
        /// The sim card on wich the request is based
        /// </summary>
        SIMCardInfo SimCard { get; set; }
    }
}
