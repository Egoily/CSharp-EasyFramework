using com.etak.core.model.revenueManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.inventory
{
    /// <summary>
    /// PhysicalProduct for device, accesssories etc..
    /// </summary>
    [DataContract]
    [Serializable]
    public class PhysicalProduct:Product
    {
        /// <summary>
        /// Specification
        /// </summary>
        public virtual PhysicalResourceSpecification PhysicalResourceSpecification { get; set; }

    }
}
