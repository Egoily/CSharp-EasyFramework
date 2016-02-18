using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.UnreserveMsisdn
{
    /// <summary>
    /// Order of UnreserveMsisdnBizOp
    /// </summary>
    public class UnreserveMsisdnOrder : Order
    {
        /// <summary>
        /// Discriminator of UnreserveMsisdnOrder
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.UnreserveMsisdnOrder; }
        }
    }
}
