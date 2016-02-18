using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
	[DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
	public class DealerProperty : LoadeableEntity
	{
        [DataMember(EmitDefaultValue = false)]
		public virtual Int64? DealerID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int PropertyID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? InvoiceNO { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? StatementNO { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual decimal? MinInvoiceAmount { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? MinInvoicePeriod { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? LogoID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? InvoiceBaseOn { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual DateTime? CreateDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? UserID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? MaxPinInvalidTimes { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual string CPSCode { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? CountryCode { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? TaxPlanId { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual bool? InvoiceDetail { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? LanguageId { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? InvoiceDueDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? BillingEntity { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? BundleId { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual decimal? CreditLimit { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? ProvisionID { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? CommissionGroup { get; set; }
        [DataMember(EmitDefaultValue = false)]
		public virtual int? BillingScenarioID { get; set; }
	}
}
