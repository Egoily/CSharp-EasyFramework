using System;


namespace com.etak.core.model.operation.messages
{
    /// <summary>
    /// DTO class message for the response of an operation that modifies an order
    /// </summary>
    public class ModifyOrderResponseDTO : ResponseBaseDTO
    {
        /// <summary>
        /// The unique transition type for that order
        /// </summary>
        public Int32 OrderTransitionCode { get; set; }
    }
}
