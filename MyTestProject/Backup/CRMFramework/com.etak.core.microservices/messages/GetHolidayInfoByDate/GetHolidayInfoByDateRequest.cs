using System;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetHolidayInfoByDate
{
    /// <summary>
    /// Get HolidayInfo using a Date as a Input Parameter
    /// </summary>
    public class GetHolidayInfoByDateRequest : RequestBase
    {
        /// <summary>
        /// Date to be used to get the HolidayInfo
        /// </summary>
        public DateTime Date { get; set; }

    }
    
}
