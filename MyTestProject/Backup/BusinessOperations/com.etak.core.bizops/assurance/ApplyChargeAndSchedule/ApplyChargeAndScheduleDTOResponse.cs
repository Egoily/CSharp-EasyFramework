using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.assurance.ApplyChargeAndSchedule
{
    /// <summary>
    /// ApplyChargeAndSchedule DTO response
    /// </summary>
    public class ApplyChargeAndScheduleDTOResponse : OrderResponseDTO,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// The Charge created
        /// </summary>
        public CustomerChargeDTO customerCharge { get; set; }
        /// <summary>
        /// Subscription related to the customer
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
