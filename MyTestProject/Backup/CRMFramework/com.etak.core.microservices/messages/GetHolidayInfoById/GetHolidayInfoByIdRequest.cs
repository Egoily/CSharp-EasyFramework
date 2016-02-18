using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetHolidayInfoById
{
    /// <summary>
    /// Get a HolidayInfo by ID
    /// </summary>
    public class GetHolidayInfoByIdRequest : RequestBase
    {
        /// <summary>
        /// Holiday Id, corresponding to the date with format yyyyMMdd
        /// </summary>
        public int HolidayId { get; set; }

        
    }
}
