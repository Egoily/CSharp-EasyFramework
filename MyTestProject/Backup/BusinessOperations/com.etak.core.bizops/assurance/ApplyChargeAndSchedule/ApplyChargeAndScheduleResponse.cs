using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.assurance.ApplyChargeAndSchedule
{
    /// <summary>
    /// Reponse for ApplyChargeAndSchedule, contains the charges created. 
    /// </summary>
    public class ApplyChargeAndScheduleResponse : CreateNewOrderResponse, ISubscriptionBasedResponse
    {
        /// <summary>
        /// CustomerChargeSchedule created in case that the charge at catalog level provided
        /// was a recurring charge
        /// </summary>
        public virtual CustomerChargeSchedule ScheduleAdded { get; set; }

        /// <summary>
        /// The CustomerCharge created to the customer
        /// </summary>
        public virtual CustomerCharge ChargeAdde { get; set; }
        /// <summary>
        /// Subscription related to the customer
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
