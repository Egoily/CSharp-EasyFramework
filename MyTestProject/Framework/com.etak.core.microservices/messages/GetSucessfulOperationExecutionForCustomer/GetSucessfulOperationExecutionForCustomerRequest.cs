using System;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.GetSucessfulOperationExecutionForCustomer
{
    /// <summary>
    /// Request for containing the filters for the get operation
    /// </summary>
    public class GetSucessfulOperationExecutionForCustomerRequest : RequestBase
    {
        /// <summary>
        /// The discriminator of the type of operation we want to retrieve
        /// </summary>
        public string OperationDiscriminator { get; set; }

        /// <summary>
        /// The start date for the range of dates to search for
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date for the range of dates to search for
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The customer that was affected by the operations
        /// </summary>
        public CustomerInfo Customer { get; set; }
    }
}
