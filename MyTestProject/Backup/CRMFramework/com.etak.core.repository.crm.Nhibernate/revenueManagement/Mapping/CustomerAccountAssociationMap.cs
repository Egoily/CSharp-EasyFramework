using com.etak.core.model.revenueManagement;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    class CustomerAccountAssociationMap : ClassMap<CustomerAccountAssociation>
    {
        public CustomerAccountAssociationMap()
        {
            Schema("dbo");
            Table("CRM_CUSTOMER_ACCOUNT_ASSN");
            DynamicUpdate();
            DynamicInsert();

            Id(x => x.Id, "CUSTOMER_ACCOUNT_ASSNID").GeneratedBy.Custom<PrefixIdGenerator>();

            Map(x => x.StartDate, "STARTDATE");
            Map(x => x.EndTime, "ENDDATE");

            References(x => x.Account, "ACCOUNTID");
            References(x => x.Customer, "CUSTOMERID");
        }
    }
}
