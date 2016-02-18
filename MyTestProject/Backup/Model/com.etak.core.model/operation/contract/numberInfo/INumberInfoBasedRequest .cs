
namespace com.etak.core.model.operation.contract.numberInfo
{
    /// <summary>
    /// Operation that is based in  a  number in the NPM pool
    /// </summary>
    public interface INumberInfoBasedRequest
    {
        /// <summary>
        /// The number in which the request is based on
        /// </summary>
        NumberInfo NumberInPool { get; set; }
    }
}
