using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
	public class CommissionHistory : LoadeableEntity
	{
		[DataMember]
		public virtual Int64 CommissionID { get; set; }
		[DataMember]
		public virtual Int32 CommissionPlanID { get; set; }
		[DataMember]
		public virtual Int32 CommissionDetailID { get; set; }
		[DataMember]
		public virtual Int32 DealerID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual String SalesManID { get; set; }
		[DataMember]
		public virtual Int32 CustomerID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual String MSISDN { get; set; }
		[DataMember]
		public virtual Decimal PaymentDealerAmount { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual Decimal? PaymentSalesManAmount { get; set; }
		[DataMember]
		public virtual Int32 PaymentUnit { get; set; }
		[DataMember]
		public virtual Boolean Approved { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual DateTime CreateDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual DateTime? LastSendDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual Boolean? PaymentSalesManSucceeded { get; set; }
		[DataMember]
		public virtual Boolean PaymentDealerSucceeded { get; set; }

        [DataMember(EmitDefaultValue = false)]
		public virtual IList<CommissionRequestError> CommissionRequestErrors { get; set; }
	}
}
