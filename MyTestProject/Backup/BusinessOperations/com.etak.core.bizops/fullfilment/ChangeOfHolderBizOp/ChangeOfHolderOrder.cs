using com.etak.core.bizops;
using com.etak.core.model.operation;

namespace com.etak.vfes.implementation.bizOps.ChangeOfHolderBizOp
{
    /// <summary>
    /// ChangeOfHolderOrder
    /// </summary>
    public class ChangeOfHolderOrder : Order
    {
        /// <summary>
        /// ChangeOfHolder order
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.ChangeOfHolder; }
        }
    }
}
