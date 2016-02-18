using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.ReserveMsisdn
{
    /// <summary>
    /// Order corresponding to ReserveMsisdn
    /// </summary>
    public class ReserveMsisdnOrder : Order
    {
        /// <summary>
        /// Discriminator corresponding to ReserveMsisdnBizOp
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.ReserveMsisdnOrder;  }
        }
    }
}
