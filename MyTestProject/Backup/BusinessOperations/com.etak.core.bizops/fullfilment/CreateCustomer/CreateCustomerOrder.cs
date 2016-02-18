using System;
using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.CreateCustomer
{
    /// <summary>
    /// Order for Create Customer
    /// </summary>
    public class CreateCustomerOrder : Order
    {
        /// <summary>
        /// Discriminator for Crate Customer Order
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.CreateCustomerOperation; }
        }
    }
}
