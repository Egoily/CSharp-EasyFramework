using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetHolidayInfoById
{
    /// <summary>
    /// Response with the HolidayInfo obtained. If it's null, it means that it isn't exist (and it's not a holiday day)
    /// </summary>
    public class GetHolidayInfoByIdResponse : ResponseBase
    {
        /// <summary>
        /// HolidayInfo obtained
        /// </summary>
        public HolidayInfo HolidayInfo { get; set; }

    }
}
