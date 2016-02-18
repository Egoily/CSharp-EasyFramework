using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.inventory
{
    [DataContract]
    [Serializable]
    public abstract class PhysicalResource
    {
        /// <summary>
        /// unique Id of Physical Resource
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        /// Referenced physical product
        /// </summary>
        public virtual PhysicalResourceSpecification PhysicalResourceSpecification { get; set; }
        /// <summary>
        /// serial number
        /// </summary>
        public virtual string SerialNumber { get; set; }
    }
}
