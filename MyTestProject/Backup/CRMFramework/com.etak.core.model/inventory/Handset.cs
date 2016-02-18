using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.inventory
{
    [DataContract]
    [Serializable]
    public class Handset:PhysicalResource
    {
        /// <summary>
        /// Network type
        /// </summary>
        public virtual NetworkType NetworkType { get; set; }
    }
}
