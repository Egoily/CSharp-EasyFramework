using System;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for HolidayInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="THolidayInfo">the type of entity managed, is or extends HolidayInfo</typeparam>
    public class HolidayInfoRepositoryNH<THolidayInfo>
        : NHibernateRepository<THolidayInfo, Int32>,
       IHolidayInfoRepository<THolidayInfo> where THolidayInfo : HolidayInfo
    {
    }
}
