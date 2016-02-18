using System;


namespace com.etak.core.model.operation.messages
{
    /// <summary>
    /// Class for the core model of a response of an operation that modifies an order
    /// </summary>
    public abstract class ModifyOrderReponse : ResponseBase
    {
        /// <summary>
        /// Unique id of the transition of the change done in the order
        /// </summary>
        public virtual Int32 OrderTransitionCode { get; set; }
    }
}
