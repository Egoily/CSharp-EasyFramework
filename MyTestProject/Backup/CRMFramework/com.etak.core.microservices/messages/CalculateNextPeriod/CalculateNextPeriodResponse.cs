
using System;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.CalculateNextPeriod
{
    /// <summary>
    /// Reponse for CalculateNextPeriod
    /// </summary>
    public class CalculateNextPeriodResponse : ResponseBase
    {
        #region Instance fields
        /// <summary>
        /// The next period for the instance
        /// </summary>
        public Int32 NextPeriodNumber { get; set; }

        /// <summary>
        /// The current cycle number of the instance
        /// </summary>
        public Int32 CurrentCycleNumber { get; set; }

        /// <summary>
        /// The next ocurrence defined by the periodicity
        /// </summary>
        public DateTime NextDate { get; set; }

        /// <summary>
        /// Determines if this date was a match, (start period, end period)
        /// </summary>
        public Boolean PeriodMatched { get; set; }

        #endregion
    }
}
