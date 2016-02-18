using System;
using System.Runtime.Serialization;
using com.etak.core.model.operation;

namespace com.etak.core.model
{
    /// <summary>
    /// Represents any change in Prmotions or Services
    /// </summary>
    [DataContract]
    [Serializable]
    public class CrmCustomersBalanceTransationHistory
    {
        /// <summary>
        /// Unique Id of the entity
        /// </summary>
        public virtual long Id { get; set; }

        /// <summary>
        /// Customer service in which the balance change took place
        /// </summary>
        public virtual ServicesInfo CustomerService { get; set; }

        /// <summary>
        /// Customer promotion in which the balance change took place
        /// </summary>
        public virtual CrmCustomersPromotionInfo CustomerPromotion { get; set; }

        /// <summary>
        /// Date in which the transaction took place
        /// </summary>
        public virtual DateTime TransactionTime { get; set; }

        /// <summary>
        /// Amount before the change
        /// </summary>
        public virtual decimal AmountAfter { get; set; }
        
        /// <summary>
        /// Amount after the change
        /// </summary>
        public virtual decimal AmountBefore { get; set; }

        /// <summary>
        /// Amount changed
        /// </summary>
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// Id of the operation provided by the MVMNO
        /// </summary>
        public virtual string ExternalId { get; set; }
        
        /// <summary>
        /// Id of the customer owning the promotion or the service
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary>
        /// Comments sent to the operation
        /// </summary>
        public virtual string Comments { get; set; }

        /// <summary>
        /// Promotion plan to which the promotion changed belongs to
        /// </summary>
        public virtual RmPromotionPlanInfo PromotionPlan { get; set; }

        /// <summary>
        /// Subscription to which the services or the promotion is associated
        /// </summary>
        public virtual ResourceMBInfo Subscription { get; set; }

        /// <summary>
        /// Package of the service that creates the prmotion plan detail id that has been
        /// modified
        /// </summary>
        public virtual PackageInfo Package { get; set; }
        
       
        public virtual int Unit { get; set; }

        /// <summary>
        /// Operation run that performs the change
        /// </summary>
        public virtual BusinessOperationExecution ChangingOperation { get; set; }

        /// <summary>
        /// Id of the operation log
        /// </summary>
        public virtual long LogId { get; set; }
    }
}
