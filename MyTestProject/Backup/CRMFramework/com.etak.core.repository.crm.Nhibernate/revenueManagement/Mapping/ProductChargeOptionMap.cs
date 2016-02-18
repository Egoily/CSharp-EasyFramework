using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /*
     CREATE TABLE PRODUCT_CHARGE_OPTIONS
( 
	PRODUCT_CHARGE_OPTIONID int  NOT NULL ,
	DESCRIPTIONID        int  NULL ,
	PRODUCTGK            int  NOT NULL ,
	STARTDATE            datetime  NULL ,
	ENDDATE              datetime  NULL ,
	STATUSID             int  NULL ,
	IS_DEFAULT           varchar(1)  NULL ,
	NAMEID               int  NULL ,
	CUSTOMER_ELIGIBILITY_RULEID integer  NULL ,
	CREATEDATE           datetime  NULL 
)
     
     * CREATE TABLE RM_CHARGE_OPTIONS
    ( 
	    CHARGEID             int  NOT NULL ,
	    PRODUCT_CHARGE_OPTIONID int  NOT NULL 
    )
     */
    /// <summary>
    /// Fluent Nhibernate mapping class for ProductChargeOption
    /// </summary>
    public class ProductChargeOptionMap : ClassMap<ProductChargeOption>
    {
        /// <summary>
        /// Default constructor so fluent map builds the xml on runtime.
        /// </summary>
        public ProductChargeOptionMap()
        {
            Schema("dbo");
            Table("PRODUCT_CHARGE_OPTIONS");
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();
            DynamicUpdate();

            Id(x => x.Id, "PRODUCT_CHARGE_OPTIONID").GeneratedBy.Custom<PrefixIdGenerator>();
            References(x => x.ProductOffering).Column("PRODUCT_OFFERINGID");

            References(x => x.Name).Column("NAMEID").Cascade.All();
            References(x => x.Description).Column("DESCRIPTIONID").Cascade.All();
                
            Map(x => x.StartDate).Column("STARTDATE");
            Map(x => x.EndDate).Column("ENDDATE");
            Map(x => x.CreateDate).Column("CREATEDATE");
            Map(x => x.Status).CustomType<ProductPurchaseStatus>().Column("STATUSID");
            Map(x => x.IsDefaultOption).CustomType<EnumStringType<DefaultOptions>>().Column("IS_DEFAULT").Length(1);
           
            HasManyToMany(x => x.Charges).
                Table("RM_CHARGE_OPTIONS").
                ParentKeyColumn("PRODUCT_CHARGE_OPTIONID").
                ChildKeyColumn("CHARGEID").                   
                Cascade.SaveUpdate().
                Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();            
        }
    }
}