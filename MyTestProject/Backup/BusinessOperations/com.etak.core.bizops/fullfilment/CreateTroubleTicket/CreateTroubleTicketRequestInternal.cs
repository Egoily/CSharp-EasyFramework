using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model;

namespace com.etak.core.bizops.fullfilment.CreateTroubleTicket
{
    /// <summary>
    /// Request Internal of CreateTroubleTicketBizOp
    /// </summary>
    public class CreateTroubleTicketRequestInternal : CreateNewOrderRequest, ISubscriptionLastActiveBasedRequest
    {
        /// <summary>
        /// MSISDN that related with Trouble ticket
        /// </summary>
        public virtual string MSISDN{get; set;} 
        /// <summary>
        /// subscription of MSISDN
        /// </summary>
        public virtual ResourceMBInfo Subscription{get; set;}

        /// <summary>
        /// trouble tikcet info
        /// </summary>
        public virtual TroubleTicketInfo TTInfo { get; set; }
    }
}
