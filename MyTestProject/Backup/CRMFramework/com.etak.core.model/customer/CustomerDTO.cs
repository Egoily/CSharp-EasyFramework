using System;
using System.Collections.Generic;

namespace com.etak.core.model.dto
{
    /// <summary>
    /// Customer DTO, which will contain the main information and the CustomerDataDTO object with the rest
    /// </summary>
    public class CustomerDTO
    {
        /// <summary>
        /// Customer ID of the Customer
        /// </summary>
        public Int32 CustomerId { get; set; }

        /// <summary>
        /// ExternalId if corresponds
        /// </summary>
        public String ExternalCustomerId { get; set; }
        /// <summary>
        /// Customer Id of the parent
        /// </summary>
        public Int32? ParentCustomerId { get; set; }
        /// <summary>
        /// A list of customers if the customer have childs
        /// </summary>
        public List<Int32> ChildCustomers { get; set; }
        /// <summary>
        /// DTO Object corresponding with all the information
        /// </summary>
        public CustomerDataDTO CustomerData { get; set; }

    }
}
