using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.inventory
{
    /// <summary>
    /// Device information 
    /// </summary>
    public class Device
    {
        /// <summary>
        /// unique Id of the Product Inventory 
        /// </summary>
        public virtual long Id { get; set; } 

        /// <summary>
        /// SKU of 3rd Party Company(eg:WSA) to identify model of device.
        /// </summary>
        public virtual string SKU { get; set; }
        /// <summary>
        ///  model number defined by manufacturer.
        /// </summary>
        public virtual string ModelNumber { get; set; }
        /// <summary>
        /// Name of device
        /// </summary>
        public virtual MultiLingualDescription Name { get; set; }
        /// <summary>
        /// Description of device
        /// </summary>
        public virtual MultiLingualDescription Description { get; set; }
        /// <summary>
        /// StandardCost of device
        /// </summary>
        public virtual decimal StandardCost { get; set; }
        /// <summary>
        /// CurrentCost of device
        /// </summary>
        public virtual decimal CurrentCost { get; set; }
    }
}
