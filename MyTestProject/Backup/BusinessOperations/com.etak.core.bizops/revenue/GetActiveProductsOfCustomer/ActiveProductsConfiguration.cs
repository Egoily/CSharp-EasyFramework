using System;
using System.Collections.Generic;
using com.etak.core.operation.contract;

namespace com.etak.core.bizops.revenue.GetActiveProductsOfCustomer
{
    /// <summary>
    /// Configuration with the settings requierd to perfrom GetActiveProducsOfCustomerBizOp
    /// </summary>
    public class ActiveProductsConfiguration : BasicOperationConfiguration
    {
        /// <summary>
        /// the specific products for active product
        /// </summary>
        public List<Int32> SpecificProductsForActiveProducts { get; set; }
        /// <summary>
        /// the Datatransfer permission products
        /// </summary>
        public List<Int32> DataTransferPermissions { get; set; }

    }
}
