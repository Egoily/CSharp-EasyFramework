using System;


namespace com.etak.core.model.subscription
{
    /// <summary>
    /// The DTO representation of ServicesInfo, represents a service of a customer
    /// </summary>
    public class ServicesInfoDTO
    {
        /// <summary>
        /// Unique Id of the service
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// Credit Limit, means balance for prepaid customer, and Credit limit for postpaid
        /// </summary>
        public virtual decimal? CreditLimit { get; set; }

        /// <summary>
        /// un billed balance, only effect for postpaid customer.
        /// </summary>
        public virtual decimal? UnBilledBalance { get; set; }
        
        public virtual decimal? BilledBalance { get; set; }
        
        public virtual int? InvoiceTemplateId { get; set; }

        /// <summary>
        /// Start date of the validity period for the service
        /// </summary>
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// End date of the validity period for the service
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// Date in which the service was created
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }

        /// <summary>
        /// Id of the user that created the entity
        /// </summary>
        public virtual int? UserID { get; set; }

        public virtual double? DepositAmount { get; set; }

        public virtual bool DeleteFlag { get; set; }
        
        public virtual byte[] UpdateStamp { get; set; }
        
        /// <summary>
        /// -1 or BundleDefinition.BundleID == CREDITLIMITBASEBUNDLEID means the service is base service.
        /// </summary>
        public virtual int CREDITLIMITBASEBUNDLEID { get; set; }
        
        public virtual int? Status { get; set; }
        
        public virtual bool PointToBaseBundle { get; set; }
        
        /// <summary>
        /// The Id of the bundle of this product is an instance
        /// </summary>
        public virtual Int32 BundleDefinitionId { get; set; }

        /// <summary>
        /// The id of the product to which this service is an instance
        /// </summary>
        public virtual Int32 ProductInfoId { get; set; }
        
        /// <summary>
        /// Id of the customer to which this service is associated
        /// </summary>
        public virtual Int32 CustomerId { get; set; }
    }
}
