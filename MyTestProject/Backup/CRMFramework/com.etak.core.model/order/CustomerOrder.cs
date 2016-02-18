using com.etak.core.model.operation;
using com.etak.core.model.revenueManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.order
{
    /// <summary>
    /// End user Order
    /// </summary>
    public class CustomerOrder : Order
    {
        /// <summary>
        /// Discriminator
        /// </summary>
        public override string Discriminator
        {
            get { return "CTMO"; }
        }
        /// <summary>
        /// Order Items of Order
        /// </summary>
        public virtual IList<OrderItem> OrderItems { get; set; }
        /// <summary>
        /// Reference Customer
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }
        /// <summary>
        /// Sequence Id
        /// </summary>
        public virtual int SequenceId { get; set; }
        /// <summary>
        /// Delivery Address
        /// </summary>
        public virtual AddressInfo DeliveryAddress { get; set; }
        /// <summary>
        /// Delivery Product, for exmaple shipping method with charge.
        /// </summary>
        public virtual Product DeliveryProduct { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public virtual string Comments { get; set; }
        /// <summary>
        /// DeliveryDate 
        /// </summary>
        public virtual DateTime? DeliveryDate { get; set; }
    }
}
