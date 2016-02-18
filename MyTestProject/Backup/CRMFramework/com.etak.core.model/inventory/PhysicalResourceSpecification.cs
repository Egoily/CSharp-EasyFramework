using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.inventory
{
    /// <summary>
    /// Device information 
    /// </summary>
    public class PhysicalResourceSpecification
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
        /// Description of device
        /// </summary>
        public virtual MultiLingualDescription Color { get; set; }
        /// <summary>
        /// Storage of device
        /// </summary>
        public virtual MultiLingualDescription Storage { get; set; }
        /// <summary>
        /// OperationSystem of device
        /// </summary>
        public virtual MultiLingualDescription OperationSystem { get; set; }
        /// <summary>
        /// Brand of device
        /// </summary>
        public virtual MultiLingualDescription Brand { get; set; }
        /// <summary>
        /// Front Camera of device
        /// </summary>
        public virtual MultiLingualDescription FrontCamera { get; set; }
        /// <summary>
        /// Back Camera of device
        /// </summary>
        public virtual MultiLingualDescription BackCamera { get; set; }
        /// <summary>
        /// Back Camera of device
        /// </summary>
        public virtual string ImageUrl { get; set; }
        /// <summary>
        /// Cost history of device
        /// </summary>
        public virtual IList<PhysicalResourceCost> Costs { get; set; }

    }
}
