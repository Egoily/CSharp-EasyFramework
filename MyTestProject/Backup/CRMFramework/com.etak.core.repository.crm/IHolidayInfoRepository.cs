using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository Interface for <typeparamref name="THolidayInfo"/> entity
    /// </summary>
    /// <typeparam name="THolidayInfo">The type of the entity managed is or extends HolidayInfo</typeparam>
    public interface IHolidayInfoRepository<THolidayInfo> : IRepository<THolidayInfo, Int32>
        where THolidayInfo : HolidayInfo
    {

    }
}
