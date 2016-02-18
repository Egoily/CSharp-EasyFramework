using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.inventory
{
    /// <summary>
    /// cost history of device
    /// </summary>
    public class PhysicalResourceCost
    {
        /// <summary>
        /// unique Id of device cost
        /// </summary>
        public virtual long Id { get; set; } 

        /// <summary>
        /// StandardCost of device
        /// </summary>
        public virtual decimal StandardCost { get; set; }
        /// <summary>
        /// CurrentCost of device
        /// </summary>
        public virtual decimal CurrentCost { get; set; }
        /// <summary>
        /// Start date of device cost
        /// </summary>
        public virtual DateTime StartDate { get; set; }
        /// <summary>
        /// End date of device cost
        /// </summary>
        public virtual DateTime? EndDate { get; set; }
        /// <summary>
        /// Referenced specification.
        /// </summary>
        public virtual PhysicalResourceSpecification Specification { get; set; }

    }
}
