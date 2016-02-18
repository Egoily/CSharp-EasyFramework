using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Definition of a charge in the catalog
    /// </summary>
    [DataContract]
    public class RecurringChargeCatalogDTO :  ChargeCatalogDTO
    {
        /// <summary>
        /// unit of time that represent the base unit for the periods in the cycle
        /// </summary>
        [DataMember] public TimeUnits Periodicity;

        /// <summary>
        /// number of the period when the cycle starts (eg. for a periodicity of “week”, 3 means that the cycle starts at week 3.)
        /// </summary>
        [DataMember] public Int32 StartPeriodNumber;

        /// <summary>
        /// number of periods the cycle runs upon.
        /// </summary>
        [DataMember] public Int32 PeriodCount;

        /// <summary>
        /// number of time the cycle has to be repeated.
        /// </summary>
        [DataMember] public Int32 CycleRepeatCount;
    }
}