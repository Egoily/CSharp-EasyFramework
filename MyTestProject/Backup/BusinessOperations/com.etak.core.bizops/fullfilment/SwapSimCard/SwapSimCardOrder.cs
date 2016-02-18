using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.SwapSimCard
{
    /// <summary>
    /// SwapSimCardOrder of SwapSimCardOBizOp
    /// </summary>
    public class SwapSimCardOrder : Order
    {
        /// <summary>
        /// Discriminator of SwapSimCardOBizOp
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.SwapSimCardOrder; }
        }
    }
}
