using com.etak.core.model.revenueManagement;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping for Invoice class
    /// </summary>
    public class InvoiceMap : ClassMap<Invoice>
    {
        /// <summary>
        /// default constructor so fluentNhibernate builds the XML mapping
        /// </summary>
        public InvoiceMap()
        {
            Schema("dbo");
            Table("CRM_ACCOUNT_INVOICES");
            DynamicUpdate();

            Id(x => x.Id, "INVOICEID").GeneratedBy.Custom<PrefixIdGenerator>();
            References(x => x.ChargingAccount, "ACCOUNTID");
            References(x => x.ChargedCustomer, "CUSTOMERID");
            References(x => x.GeneratingBillRun, "BILLRUNID");

            HasMany(x => x.Charges).KeyColumn("INVOICEID")
               .Inverse();

            Map(x => x.LegalInvoiceNumber, "LEGAL_INVOICE_NUMBER");
            Map(x => x.StartDate, "STARTDATE");
            Map(x => x.EndDate, "ENDDATE");
            Map(x => x.Status, "STATUSID").CustomType<InvoiceStatus>();
            Map(x => x.InvoiceFileName, "INVOICE_FILENAME");
        }
    }
}