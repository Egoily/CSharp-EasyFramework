using System;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.CancelCustomerProduct
{
    /// <summary>
    /// Class for CancelCustomerProduct request  in DTO model 
    /// </summary>
    public class CancelCustomerProductRequestDTO : OrderRequestDTO
    {
        /// <summary>
        /// CustomerProductAssignment id to be cancelled
        /// </summary>
        public Int64 CustomerProductAssignmentId { get; set; }

        /// <summary>
        /// Cancel date
        /// </summary>
        public DateTime CancelDate { get; set; }

        /// <summary>
        /// Is it using nexbillcycle for enddate or cancelDate
        /// </summary>
        public bool UseNextBillCycleEndDateInRecurring { get; set; }
        /// <summary>
        /// store the current system time
        /// </summary>
        public DateTime? CurrentTime { get; set; }
    }
}
