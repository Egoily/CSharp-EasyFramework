using System;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.CalculateNextPeriod
{
    /// <summary>
    /// Calculates the next dates and periods for a standart ET periodicty specification
    /// </summary>
    public class CalculateNextPeriodRequest : RequestBase
    {
        #region catalog definition of the periodicity
        
        /// <summary>
        /// A unit of time that represent the base unit for the periods in the cycle, following the TimeUnit specification.
        /// </summary>
        public virtual TimeUnits PeriodUnit { get; set; }
        
        /// <summary>
        /// The number of periods between two consecutive charges 
        /// </summary>
        /// <example>(1 if periodunitid is Monthly and the charge has to be charged every month, 3 in the same case but the charge happens every 3 months).</example>
        public virtual int PeriodCount { get; set; }

        /// <summary>
        /// the number of the period when the cycle starts 
        /// </summary>
        /// <example>(eg. for a periodunitid of “week” and a periodunitcount of 1, 3 means that the cycle starts at week 3). The first period of the cycle is number 1.</example>
        public virtual int StartPeriodNumber { get; set; }
        
        /// <summary>
        /// the number of the period when the cycle ends
        /// </summary>
        /// <example> (eg. for a periodunitid of “week” and a periodunitcount of 1, 6 means that the cycle stops at week 6). The last period of the cycle is the period number "periodicity"</example>
        public virtual int EndPeriodNumber { get; set; }

        /// <summary>
        /// the number of periods within a cycle.
        /// </summary>
        public virtual int Periodicity { get; set; }

        /// <summary>
        /// the number of time the cycle has to be repeated. 
        /// </summary>
        public virtual int CycleRepeatCount { get; set; }
        #endregion
        
        #region Instance fields

        /// <summary>
        /// The last ocurrence of the period.
        /// </summary>
        public DateTime NextDate { get; set; }


        /// <summary>
        /// The next period for the instance
        /// </summary>
        public Int32 NextPeriodNumber { get; set; }

        /// <summary>
        /// The current cycle number of the instance
        /// </summary>
        public Int32 CurrentCycleNumber { get; set; }

       

        
        #endregion

      
    }
}
