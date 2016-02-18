using System;


namespace com.etak.core.model.operation
{
    /// <summary>
    /// Entity to keep the transitions of state of an order
    /// </summary>
    public class OrderTransition
    {
        /// <summary>
        /// Unique ID of the transition
        /// </summary>
        public virtual Int64 Id { get; set; }

        /// <summary>
        /// Order that had the transicion
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// The date in the transition took place
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Source state of the order before the transition
        /// </summary>
        public virtual String SourceState { get; set; }

        /// <summary>
        /// Destination state of the order after the transition
        /// </summary>
        public virtual String DestinationState { get; set; }

        /// <summary>
        /// Unique code for the transition for that order type
        /// </summary>
        public virtual Int32 TransitionCode { get; set; }

        /// <summary>
        /// The operation that trigered the change.
        /// </summary>
        public virtual BusinessOperationExecution TriggeringOperation { get; set; }
    }
}
