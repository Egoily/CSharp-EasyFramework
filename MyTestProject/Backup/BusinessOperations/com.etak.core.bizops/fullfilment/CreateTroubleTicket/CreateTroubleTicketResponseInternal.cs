using com.etak.core.model;
using com.etak.core.model.operation.messages;


namespace com.etak.core.bizops.fullfilment.CreateTroubleTicket
{
    /// <summary>
    /// Internal response for CreateTroubleTicketBizop
    /// </summary>
    public class CreateTroubleTicketResponseInternal : CreateNewOrderResponse
    {   
        /// <summary>
        /// Created trouble ticket
        /// </summary>
        public TroubleTicketInfo TroubleTicket { get; set; }
        
    }
}
