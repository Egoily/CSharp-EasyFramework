using System;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.UpdateCustomerData
{
    /// <summary>
    /// Request to update customer information
    /// </summary>
    public class UpdateCustomerDataInternalRequest : CreateNewOrderRequest, ICustomerBasedRequest
    {
        /// <summary>
        /// CustomerDTO object with the new information to be update
        /// </summary>
        public virtual CustomerDTO NewCustomerInfo { get; set; }

        /// <summary>
        /// CustomerInfo of actual customer Information
        /// </summary>
        public CustomerInfo Customer { get; set; }

        /// <summary>
        /// Function to compare 
        /// </summary>
        public Func<BankInfo, BankInfo, Boolean> BankComparerFunc { get; set; }

    }
}
