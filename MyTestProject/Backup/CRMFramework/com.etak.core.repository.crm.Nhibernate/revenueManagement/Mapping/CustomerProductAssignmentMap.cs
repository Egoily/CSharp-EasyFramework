using com.etak.core.model.revenueManagement;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Fluent Nhibernate mapping for CustomerProductAssignment class
    /// </summary>
    public class CustomerProductAssignmentMap : ClassMap<CustomerProductAssignment>
    {
        /// <summary>
        /// default constructor so fluentNhibernate builds the XML mapping
        /// </summary>
        public CustomerProductAssignmentMap()
        {
            Schema("dbo");
            Table("CRM_CUSTOMER_PRODUCT_ASSN");
            DynamicUpdate();
          
            Id(x => x.Id, "CUSTOMERPRODUCTID").GeneratedBy.Custom<PrefixIdGenerator>();
            References(x => x.PurchasingCustomer, "CUSTOMERID");
            References(x => x.PurchasedProduct, "PRODUCTGK");
            References(x => x.ProductChargePurchased, "PRODUCT_CHARGE_OPTIONID");
            References(x => x.ProductOffering, "PRODUCT_OFFERINGID");
            Map(x => x.StartDate, "STARTDATE");
            Map(x => x.CreateDate, "CREATEDATE");
            Map(x => x.EndDate, "ENDDATE");           
            
        }
    }
}