using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    [Serializable]
    public class BillCycle
    {
        /// <summary>
        /// Unique Id of the bill cycle
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// The dealer to which this bill cycle belongs
        /// </summary>
        public virtual DealerInfo VMNO { get; set; }

        /// <summary>
        /// Textual description for the bill cycle
        /// </summary>
        public virtual MultiLingualDescription Description { get; set; }

        /// <summary>
        /// The day that the Cycle runs
        /// </summary>
        public virtual Int16 RunDay { get; set; }

        /// <summary>
        /// The  day of the month up to which the usage has to be taken into account
        /// </summary>
        public virtual Int16 CutOffDay { get; set; }
        
        /// <summary>
        /// Specifies the unit of time in which PeriodQuantity is expressed
        /// </summary>
        public virtual TimeUnits PeriodUnit { get; set; }
        
        /// <summary>
        /// The amount of time that to look for charges to be billed
        /// </summary>
        public virtual Int32 PeriodQuantity { get; set; }

        /// <summary>
        /// MISSING DESCRIPTION  
        /// </summary>
        public virtual Int32 DaysUntilLate { get; set; }

    }
}