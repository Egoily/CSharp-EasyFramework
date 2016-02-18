using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping for CustomerCharge class
    /// </summary>
    public class CustomerChargeMap : ClassMap<CustomerCharge>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public CustomerChargeMap()
        {
            Schema("dbo");
            Table("CRM_CUSTOMERS_CHARGES");
            DynamicUpdate();
            Id(x => x.Id, "CUSTOMER_CHARGEID").GeneratedBy.Custom<PrefixIdGenerator>();

            References(x => x.Customer, "CUSTOMERID");
            References(x => x.ChargingAccount, "ACCOUNTID");
            References(x => x.Product, "CUSTOMERPRODUCTID");
            References(x => x.Schedule, "CUSTOMER_CHARGE_SCHEDULEID");
            References(x => x.ChargeDefinition, "CHARGEID");
            References(x => x.Invoice, "INVOICEID");
            References(x => x.Tax, "TAXID");

            Map(x => x.Currency, "ISO4217_CURRENCY_CODE").CustomType<ISO4217CurrencyCodes>();
            Map(x => x.Amount, "AMOUNT");
            Map(x => x.InformationalAmount, "INFORMATIONAL_AMOUNT");
            Map(x => x.ChargingDate, "CHARGEDATE");
            Map(x => x.PeriodNumber, "PERIODNUMBER");
            Map(x => x.CycleNumber, "CYCLE_NUMBER");
            Map(x => x.Comments, "COMMENTS");
            Map(x => x.ExternalReferenceCode, "EXTERNAL_REFERENCE_CODE");
            Map(x => x.TaxAmount, "TAX_AMOUNT");
            
        }
    }
}