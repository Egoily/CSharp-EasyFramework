
using com.etak.core.model.resource;

namespace com.etak.core.model.operation.contract.numberInfo
{
    /// <summary>
    /// Response in DTO form that contains a number info.
    /// </summary>
    public interface INumberInfoBasedDTOResponse
    {
        /// <summary>
        /// Information about the number in DTO form.
        /// </summary>
        MSISDNResourceDTO NumberInPool { get; set; }
    }
}
