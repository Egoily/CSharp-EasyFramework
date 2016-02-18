using System;
using System.Collections.Generic;


namespace com.etak.core.model.operation
{
    /// <summary>
    /// Entity that represent an order, it can be a Customer order or a service order. 
    /// </summary>
    public abstract class Order
    {
        /// <summary>
        /// Unique Id of the order
        /// </summary>
        public virtual Int64 Id { get; set; }

        /// <summary>
        /// The discriminator for the order type
        /// </summary>
        public abstract String Discriminator { get;}
        
        /// <summary>
        /// Date in which the order was created
        /// </summary>
        public virtual DateTime CreationDate { get; set; }

        /// <summary>
        /// Date in which the order was updated for last time. 
        /// </summary>
        public virtual DateTime LastUpdateDate { get; set; }
        
        /// <summary>
        /// The date in which the order was completed
        /// </summary>
        public virtual DateTime CompletitionDate { get; set; }

        /// <summary>
        /// Status of the order
        /// </summary>
        public virtual String Status { get; set; }

        /// <summary>
        /// Dealer that created the order (provided by the operation that creates the order
        /// </summary>
        public virtual DealerInfo Dealer { get; set; }

        /// <summary>
        /// External identifier of the Order (provided by the one that creates the order)
        /// </summary>
        public virtual String ExternalId { get; set; }

        /// <summary>
        /// An Id if the order requires reach an external system (provided by the one that creates the order)
        /// </summary>
        public virtual String ExternalThirdPartyId { get; set; }

        /// <summary>
        /// An Id if the order requires reach an external system (provided by thrid party)
        /// </summary>
        public virtual String ThirdPartyId { get; set; }

        /// <summary>
        /// Business Operation that triggered this order. 
        /// </summary>
        public virtual BusinessOperationExecution StartingOperation { get; set; }

        /// <summary>
        /// Operations that affected this operation
        /// </summary>
        public virtual IList<BusinessOperationExecution> OperationsForOrder { get; set; }

        /// <summary>
        /// Parent order in case this order is a sub order
        /// </summary>
        public virtual Order ParentOrders { get; set; }

        /// <summary>
        /// List of sub order in case is a multi order line
        /// </summary>
        public virtual IList<Order> SubOrders { get; set; }
    }
}
