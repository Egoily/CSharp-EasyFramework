using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.dto;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.CreateCustomerAccountCurrency
{
    /// <summary>
    /// Request DTO of CreateCustomerAccountCurrency
    /// </summary>
    public class CreateCustomerAccountCurrencyRequestDTO : OrderRequestDTO
    {
        /// <summary>
        /// CustomerDataDTO
        /// </summary>
        public CustomerDTO CustomerDto { get; set; }
    }
}
