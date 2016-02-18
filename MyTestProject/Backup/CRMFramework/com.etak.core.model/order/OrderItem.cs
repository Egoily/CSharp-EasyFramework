using com.etak.core.model.operation;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.order
{
    /// <summary>
    /// Order item
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Unique Id of the order
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public virtual decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public virtual int Quantity { get; set; }
        /// <summary>
        /// Referenced Order
        /// </summary>
        public virtual Order Order { get; set; }
        /// <summary>
        /// Referenced Prodcut
        /// </summary>
        public virtual Product Product { get; set; }
        /// <summary>
        /// Referenced Prodcut
        /// </summary>
        public virtual ProductOffering ProductOffering { get; set; }
    }
}
