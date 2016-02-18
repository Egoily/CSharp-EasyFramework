using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.operation.contract;

namespace com.etak.core.bizops.assurance.ApplyChargeAndSchedule
{
    /// <summary>
    /// Configuration class for ApplyChargeAndScheduleConfiguration
    /// </summary>
    public class ApplyChargeAndScheduleConfiguration : BasicOperationConfiguration
    {
        /// <summary>
        /// Determines if an event has to be send once the charge has been added
        /// </summary>
        public Boolean SendChargeEvent { get; set; }
    }
}
