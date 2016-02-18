using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.CreateTroubleTicket
{
    /// <summary>
    /// DTO response for CreateTroubleTicketBizop
    /// </summary>
    public class CreateTroubleTicketResponseDTO : OrderResponseDTO
    {
        /// <summary>
        /// Created trouble ticket in DTO form
        /// </summary>
        public TroubleTicketInfo TroubleTicket { get; set; }
    }
}
