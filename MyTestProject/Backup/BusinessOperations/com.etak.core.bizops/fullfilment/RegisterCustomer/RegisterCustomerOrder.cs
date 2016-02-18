using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.RegisterCustomer
{
    /// <summary>
    /// Order class definition for the Register Customer Operation
    /// </summary>
    public class RegisterCustomerOrder : Order
    {
        /// <summary>
        /// Discriminator for the Register Operation
        /// </summary>
        public override string Discriminator
        {
            get { return OperationDiscriminators.RegisterCustomer; }
        }
    }
}
