using com.etak.core.model;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.customer.mapping
{
    /// <summary>
    /// Nhibernate Class map for CrmCustomersBalanceTransationHistory
    /// </summary>
    public class CrmCustomersBalanceTransationHistoryMap : ClassMap<CrmCustomersBalanceTransationHistory>
    {
        /// <summary>
        /// Constructor so fluent nhibernat build the equivalent hbm.xml on runtime
        /// </summary>
        public CrmCustomersBalanceTransationHistoryMap()
        {
            Table("CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY");
            DynamicInsert();
            DynamicUpdate();

            Id(x => x.Id, "NHIBERNATE_KEY").GeneratedBy.Custom<MiddlefixIdGenerator>();
            References(x => x.CustomerService, "SERVICEID").ForeignKey("FK_CRM_CUSTOMER_SERVICESID");
            References(x => x.CustomerPromotion, "PROMOTIONID").ForeignKey("FK_CRM_CUSTOMER_PROMOTIONSID");
            References(x => x.ChangingOperation, "OPERATION_EXECUTIONID").ForeignKey("FK_CRM_OPERATION_EXECUTIONSID");
            Map(x => x.TransactionTime, "TRANSACTIONTIME");
            Map(x => x.AmountAfter, "AMOUNT_AFTER").Precision(22).Scale(8);
            Map(x => x.AmountBefore, "AMOUNT_BEFORE").Precision(22).Scale(8);
            Map(x => x.ExternalId, "EXTERNAL_REASONCODE").CustomType("AnsiString");
            References(x => x.Customer, "CUSTOMERID").ForeignKey("FK_CRM_CUSTOMERSID");
            Map(x => x.Comments, "COMMENTS");
            Map(x => x.LogId, "LOGID");
            References(x => x.PromotionPlan, "PROMOTIONPLANID").ForeignKey("FK_RM_PROMOTIONPLANID");
            References(x => x.Subscription, "RESOURCEID").ForeignKey("FK_CRM_CUSTOMER_RESOURCEMBID");
            Map(x => x.Unit, "ID_UNIT");
            References(x => x.Package, "PACKAGEID").ForeignKey("FK_RM_PACKAGES");
        }
    }
}
