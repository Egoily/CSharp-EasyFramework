using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetHolidayInfoByDate
{
    /// <summary>
    /// Response for GetHolidayInfoByDate
    /// </summary>
    public class GetHolidayInfoByDateResponse : ResponseBase
    {
        /// <summary>
        /// HolidayInfo obtained
        /// </summary>
        public HolidayInfo HolidayInfo { get; set; }

    }
}
