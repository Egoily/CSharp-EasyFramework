using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class DealerPropertiesInfo
    {
        virtual public int? PropertyID { get; set; }
        virtual public int? InvoiceNO { get; set; }
        virtual public int? StatementNO { get; set; }
        virtual public decimal? MinInvoiceAmount { get; set; }
        virtual public int? MinInvoicePeriod { get; set; }
        virtual public int? LogoID { get; set; }
        virtual public int? InvoiceBaseOn { get; set; }
        virtual public DateTime? CreateDate { get; set; }
        virtual public int? UserID { get; set; }
        virtual public int? MaxPinInvalidTimes { get; set; }
        virtual public DealerInfo DealerInfo { get; set; }
        virtual public string CPSCode { get; set; }
        virtual public int? CountryCode { get; set; }
        virtual public int? TaxPlanId { get; set; }
        virtual public bool? InvoiceDetail { get; set; }
        virtual public int? LanguageId { get; set; }
        virtual public int? InvoiceDueDate { get; set; }
        virtual public int? BillingEntity { get; set; }
        virtual public int? BundleId { get; set; }
        virtual public decimal? CreditLimit { get; set; }
        virtual public int? ProvisionID { get; set; }
        virtual public int? BillingScenarioID { get; set; }
      
        //TODO: added for KSA to Base
        virtual public int CommissionGroup{get;set;}
    }
}
