using System;
using com.etak.core.model.operation.messages;

namespace com.etak.core.model.operation
{
    /// <summary>
    /// Entity to persist the execution of a bussiness operation
    /// </summary>
    public class BusinessOperationExecutionDTO
    {
        /// <summary>
        /// Unique Id of the operation
        /// </summary>
        public virtual Int64 Id { get; set; }

        /// <summary>
        /// Discriminator for the bussines operation.
        /// </summary>
        public virtual String ProcessorDiscriminator { get; set; }

        /// <summary>
        /// Identification of the type of the operation
        /// </summary>
        public virtual string OperationCode { get; set; }

        /// <summary>
        /// Date in which we started to process the request
        /// </summary>
        public virtual DateTime StartTime { get; set; }

        /// <summary>
        /// Date in which the operation was completed
        /// </summary>
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// The id of the user authenticated
        /// </summary>
        public virtual Int32 ? UserId { get; set; }

        /// <summary>
        /// The external channel that performed the operation.
        /// </summary>
        public virtual String Channel { get; set; }

        /// <summary>
        /// The Dealer id  obtained as a result of vmno parameter sent
        /// </summary>
        public virtual Int32 ? MVNOId { get; set; }

        /// <summary>
        /// Customer id of the  operation (if any)
        /// </summary>
        public virtual Int32 ? CustomerId { get; set; }

        /// <summary> 
        /// Subscription id of the operation (if any)
        /// </summary>
        public virtual Int32 ? SubscriptionId { get; set; }

        /// <summary>
        /// MSIDN resource of the operation (if any)
        /// </summary>
        public virtual String MSISDN { get; set; }

        /// <summary>
        /// Id of the Account of the operation (if any)
        /// </summary>
        public virtual Int64 ? AccountId { get; set; }

        /// <summary>
        /// Product Offering in the operation (if any)
        /// </summary>
        public virtual Int32 ? ProductOfferingId { get; set; }

        /// <summary>
        /// Product related with the operation (if any)
        /// </summary>
        public virtual Int32? ProductId { get; set; }

        /// <summary>
        /// the Id of the association between product and customer as a result of the operation.  (if any)
        /// </summary>
        public virtual Int64 ? ProductAssignmentId { get; set; }

        /// <summary>
        /// SimCard resource of the operation (if any)
        /// </summary>
        public virtual String ICCId { get; set; }

        /// <summary>
        /// Amount of the operation
        /// </summary>
        public virtual Decimal Amount { get; set; }

        /// <summary>
        /// Customer Destination of the operation (if any) for operations like balance transfer
        /// </summary>
        public virtual Int32 ? CustomerDestinationId { get; set; }

        /// <summary> 
        /// Id of the Subscription of the operation (if any) or operations like balance transfer
        /// </summary>
        public virtual Int32 ? SubscriptionDestinationId { get; set; }

        /// <summary>
        /// The order afected by this operation (if any)
        /// </summary>
        public virtual Int64 ? OrderManagedId { get; set; }

        /// <summary>
        /// The Business Operation that invoked current business Operation
        /// </summary>
        public virtual Int64 ? ParentBusinessOperationId { get; set; }

        /// <summary>
        /// The root Business Operation that end up in the invokation of this business operation
        /// </summary>
        public virtual Int64? RootBusinessOperationId { get; set; }

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
    }
}
