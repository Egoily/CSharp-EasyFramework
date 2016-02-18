using System;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation
{
    /// <summary>
    /// Entity to persist the execution of a bussiness operation
    /// </summary>
    public class BusinessOperationExecution
    {
        /// <summary>
        /// Unique Id of the operation
        /// </summary>
        public virtual Int64 Id { get; set; }

        /// <summary>
        /// The definition of the instance being execute
        /// </summary>
        public virtual BusinessOperation OperationDefintition { get; set; }

        ///// <summary>
        ///// Discriminator for the bussines operation.
        ///// </summary>
        //public virtual String ProcessorDiscriminator { get; set; }

        ///// <summary>
        ///// Identification of the type of the operation
        ///// </summary>
        //public virtual string OperationCode { get; set; }

        /// <summary>
        /// Date in which we started to process the request
        /// </summary>
        public virtual DateTime StartTime { get; set; }

        /// <summary>
        /// Date in which the operation was completed
        /// </summary>
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// The user authenticated
        /// </summary>
        public virtual LoginInfo User { get; set; }

        /// <summary>
        /// The external channel that performed the operation.
        /// </summary>
        public virtual String Channel { get; set; }

        /// <summary>
        /// The Dealer obtained as a result of vmno parameter sent
        /// </summary>
        public virtual DealerInfo MVNO { get; set; }

        /// <summary>
        /// Customer of the  operation (if any)
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary> 
        /// Subscription of the operation (if any)
        /// </summary>
        public virtual ResourceMBInfo Subscription { get; set; }

        /// <summary>
        /// MSIDN resource of the operation (if any)
        /// </summary>
        public virtual NumberInfo MSISDN { get; set; }

        /// <summary>
        /// Account of the operation (if any)
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// Product in the operation (if any)
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// the association between product and customer as a result of the operation.  (if any)
        /// </summary>
        public virtual CustomerProductAssignment ProductAssignment { get; set; }

        /// <summary>
        /// SimCard resource of the operation
        /// </summary>
        public virtual SIMCardInfo SimCard { get; set; }

        /// <summary>
        /// Amount of the operation
        /// </summary>
        public virtual Decimal Amount { get; set; }

        /// <summary>
        /// Customer Destination of the operation (if any) for operations like balance transfer
        /// </summary>
        public virtual CustomerInfo CustomerDestination { get; set; }

        /// <summary> 
        /// Subscription of the operation (if any) or operations like balance transfer
        /// </summary>
        public virtual ResourceMBInfo SubscriptionDestination { get; set; }

        /// <summary>
        /// The order afected by this operation (if any)
        /// </summary>
        public virtual Order OrderManaged { get; set; }

        /// <summary>
        /// The Business Operation that invoked current business Operation
        /// </summary>
        public virtual BusinessOperationExecution ParentBusinessOperation { get; set; }

        /// <summary>
        /// The root Business Operation that end up in the invokation of this business operation
        /// </summary>
        public virtual BusinessOperationExecution RootBusinessOperation { get; set; }

        /// <summary>
        /// The type of the result of the operation
        /// </summary>
        public virtual ResultTypes ResultType { get; set; }

        /// <summary>
        /// Error code of the operation
        /// </summary>
        public virtual Int32 ErrorCode { get; set; }

        /// <summary>
        /// Exception details of the operation 
        /// </summary>
        public virtual String SystemMessages { get; set; }

        /// <summary>
        /// Exception details of the operation 
        /// </summary>
        public virtual OperationConfiguration Configuration { get; set; }
    }
}
