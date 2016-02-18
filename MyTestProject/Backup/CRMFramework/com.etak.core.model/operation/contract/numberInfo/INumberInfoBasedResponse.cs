
namespace com.etak.core.model.operation.contract.numberInfo
{
    /// <summary>
    /// Response for core model that contains a numberinfo property
    /// </summary>
    public interface INumberInfoBasedResponse
    {
        /// <summary>
        /// The number information
        /// </summary>
        NumberInfo NumberInPool { get; set; }
    }
}
