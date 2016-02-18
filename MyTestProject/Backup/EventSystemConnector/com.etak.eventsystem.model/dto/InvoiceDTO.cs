
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.eventsystem.model.dto
{
     [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]    
    public enum InvoiceStatuses
    {
        [EnumMember] Unknown = -1,
        [EnumMember] Drafted = 0,
        [EnumMember] Validated = 10,
        [EnumMember] Ready = 20,
        [EnumMember] Open = 30,
        [EnumMember] Closed = 40,
        [EnumMember] Cancelled = 50,
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]    
    public class InvoiceDTO : LoadeableEntity
    {  
        /// <summary>
        ///  Unique Id of the invoice
        /// </summary>
        [DataMember]
         public long Id { get; set; }

        /// <summary>
        /// The customer against whom the invoice is issued against.
        /// </summary>
        [DataMember]
         public Int32 ChargedCustomerId { get; set; }
              
        /// <summary>
        /// The account that is charged with this Invoice
        /// </summary>
        [DataMember]
         public Int64 ChargingAccountId { get; set; }
       
        /// <summary>
        /// the last day that is billed through the invoice.
        /// </summary>
        [DataMember]
        public DateTime? EndDate { get; set; }
       
        /// <summary>
        /// The BillRun that generated this Invoice
        /// </summary>
        [DataMember]
        public Int32 GeneratingBillRunId { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string InvoiceFileName { get; set; }
        
        /// <summary>the legal invoice number, the number of the invoice as presented on the bill.
        /// This number is the actual reference of the legal document that an invoice
        /// is.  There are different legal requirements to be applied when generating
        /// those numbers, depending on the country laws.        
        /// </summary>
        [DataMember]
        public string LegalInvoiceNumber { get; set; }
       
        /// <summary>
        /// the first day that is billed through the invoice.
        /// </summary>
        [DataMember]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Current status of the invoice
        /// </summary>     
        [DataMember]
        public virtual InvoiceStatuses ? Status { get; set; }
    }
}
