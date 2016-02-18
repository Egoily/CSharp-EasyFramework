using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.SendSMS
{
    /// <summary>
    /// Order for SendSMS.
    /// </summary>
    public class SendSMSOrder : Order
    {
        /// <summary>
        /// Discriminator for SendSMS order
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.SendSMSOrder; }
        }
    }
}
