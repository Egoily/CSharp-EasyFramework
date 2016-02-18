using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.CreateCustomerAccountCurrency
{
    /// <summary>
    /// Order of CreateCustomerAccountCurrency
    /// </summary>
    public class CreateCustomerAccountCurrencyOrder : Order
    {
        /// <summary>
        /// Discriminator of CreateCustomerAccountCurrencyOrder
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.CreateCustomerAccountCurrency; }
        }
    }
}
