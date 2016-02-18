using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.inventory
{
    [DataContract]
    [Serializable]
    public class ProductInventory
    {
        /// <summary>
        /// unique Id of the Product Inventory 
        /// </summary>
        public virtual long Id { get; set; } 
        /// <summary>
        /// Referenced Physical Product
        /// </summary>
        public virtual PhysicalProduct Product { get; set; }
        /// <summary>
        /// Begining qty
        /// </summary>
        public virtual int BeginningQuantity { get; set; }
        /// <summary>
        ///  qty available for sale
        /// </summary>
        public virtual int AvailabeQuantity { get; set; }

    }
}
