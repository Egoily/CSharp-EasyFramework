using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ServicesInfo
    {
        /// <summary>
        /// Unique Id of the service
        /// </summary>
        public virtual int? ServiceID { get; set; } 
        /// <summary>
        /// Credit Limit, means balance for prepaid customer, and Credit limit for postpaid
        /// </summary>
        public virtual decimal? CreditLimit { get; set; }
        /// <summary>
        /// un billed balance, only effect for postpaid customer.
        /// </summary>
        public virtual decimal? UnBilledBalance { get; set; }
        public virtual decimal? BilledBalance { get; set; }
        public virtual int? InvoiceTemplateID { get; set; }

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
        public virtual BundleInfo BundleDefinition { get; set; }
        public virtual ProductInfo ProductInfo { get; set; }
        public virtual CustomerInfo CustomerInfo { get; set; }
        

        public virtual ServicesInfo Clone()
        {
            ServicesInfo servicesInfo = this.MemberwiseClone() as ServicesInfo;
            return servicesInfo;
        }
    }
}
