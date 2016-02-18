using com.etak.core.model.dto;
using com.etak.core.model.operation.messages;

namespace com.etak.vfes.implementation.bizOps.ChangeOfHolderBizOp
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangeOfHolderResponseDTO : OrderResponseDTO
    {
        /// <summary>
        /// the created new customer dto
        /// </summary>
        public CustomerDTO Customer { get; set; }
        /// <summary>
        ///  the total amount that transferd from old holder to new holder
        /// </summary>
        public decimal BenefitTransfered { get; set; }
    }
}
