using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.dto;
using com.etak.core.model.operation.messages;
using com.etak.core.model.operation.contract.subscription;

namespace com.etak.core.bizops.fullfilment.CreateTroubleTicket
{
    /// <summary>
    /// Request DTO of CreateTroubleTicketBizOp
    /// </summary>
    public class CreateTroubleTicketRequestDTO : OrderRequestDTO, IMsisdnBasedDTORequest
    {
        /// <summary>
        /// Type of Trouble Ticket wish to create
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Subtype of Trouble Ticket wish to create
        /// </summary>
        public string subtype { get; set; }
        /// <summary>
        /// MSISDN of customer that related with the trouble ticket
        /// </summary>
        public string MSISDN { get; set; }
        /// <summary>
        /// Severity of trouble ticket
        /// </summary>
        public int? priority { get; set; }
        /// <summary>
        /// Description of trouble ticket
        /// </summary>
        public String troubleTicketDescription { get; set; }

    }
}
