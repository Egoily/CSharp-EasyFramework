using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.UpdateCustomerData
{
    /// <summary>
    /// Order class for UpdateCustomerOperation
    /// </summary>
    public class UpdateCustomerOrder : Order
    {
        /// <summary>
        /// unique discriminator for UpdateCustomerOrder
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.UpdateCustomerOrder; }
        }
    }
}