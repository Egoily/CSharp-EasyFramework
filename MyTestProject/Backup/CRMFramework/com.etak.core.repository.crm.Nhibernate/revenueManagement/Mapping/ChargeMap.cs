using System;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Fluent Nhibernate mapping class for ProductChargeOption
    /// </summary>
    public class ChargeMap : ClassMap<Charge>
    {

//        CREATE TABLE RM_CHARGES
//( 
//    CHARGEID             int  NOT NULL ,
//    CHARGETYPEID         int  NULL ,
//    CHARGE_CATEGORYID    integer  NULL ,
//    NAMEID               int  NULL ,
//    DESCRIPTIONID        int  NULL ,
//    PRORATE_UNITID       int  NULL ,
//    PRORATE_QTY          INT  NULL ,
//    CREATEDATE           datetime  NULL ,
//    INFORMATIONAL_ONLY   varchar(1)  NULL ,
//    CUSTOM_COMPUTE_RULEID integer  NULL ,
//    START_PERIODNUMBER   int  NULL ,
//    END_PERIODNUMBER     int  NULL ,
//    PERIODICITY          integer  NULL ,
//    CYCLE_REPEAT_COUNT   integer  NULL ,
//    PERIOD_UNITID        int  NULL ,
//    PERIOD_UNIT_COUNT    integer  NULL ,
//    TIME_OF_CHARGE       varchar(1)  NULL ,
//    PERIOD_COMMITMENT    integer  NULL ,
//    TARGET_CHARGETYPEID  int  NULL ,
//    ADJUSTMENTTYPEID     int  NULL ,
//    ADJUSTMENT_UNITID    int  NULL ,
//    ADJUSTMENT_QUANTITY  decimal(18,4)  NULL ,
//    STATUSID             INT  NULL 
//)
        /// <summary>
        /// Default constructor so fluent map builds the xml on runtime.
        /// </summary>
        public ChargeMap()
        {
            Schema("dbo");
            Table("RM_CHARGES");
            DynamicUpdate();
            DynamicInsert();

            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();
            DiscriminateSubClassesOnColumn<Int32>("CHARGETYPEID", 0).Not.Nullable();

            Id(x => x.Id,"CHARGEID").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.Category).Column("CHARGE_CATEGORYID");

            References(x => x.Name).Cascade.All().Column("NAMEID");
            References(x => x.Description).Cascade.All().Column("DESCRIPTIONID");

            Map(x => x.ProrateUnit).CustomType<Nullable<TimeUnits>>().Column("PRORATE_UNITID");
            Map(x => x.ProrateQty).Column("PRORATE_QTY");
            Map(x => x.CreateTime).Column("CREATEDATE");
            Map(x => x.InformationalOnly).CustomType<EnumStringType<InformationalTypes>>().Column("INFORMATIONAL_ONLY");
            Map(x => x.Status).CustomType<ChargeStatus>().Column("STATUSID");
            Map(x => x.TypeOfTimeOfCharge).CustomType<TimesOfChargeCustomMapper>().Column("TIME_OF_CHARGE");
            Map(x => x.GeneralLedgerAccount).Column("GL_CODE");

            HasMany(x => x.Prices)
                .KeyColumn("CHARGEID")
                .Inverse()
                .Cascade.All()
                .Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            HasManyToMany(x => x.ReferencingOptions).                
                Table("RM_CHARGE_OPTIONS").
                ParentKeyColumn("CHARGEID").
                ChildKeyColumn("PRODUCT_CHARGE_OPTIONID").
                Inverse().
                Cascade.SaveUpdate().
                Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            HasMany(x => x.ReferencedCharges).
                KeyColumn("CHARGEID")
                .Inverse()
                .Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();         

            HasMany(x => x.ReferencingCharges).               
               KeyColumn("TARGET_CHARGEID").
               Inverse()
               .Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();
        }
    }

    /// <summary>
    /// Maps TimesOfCharge to custom strings in the DB
    /// </summary>
    public class TimesOfChargeCustomMapper :   EnumStringType<TimesOfCharge>
    {
        /// <summary>
        /// Gets the Db representation for the enum TimesOfCharge
        /// </summary>
        /// <param name="enm">the TimesOfCharge to be converted</param>
        /// <returns>the TimesOfCharge in string format to be persisted in the DB</returns>
        public override object GetValue(object enm)
        {
            if (null == enm)
                return String.Empty;

            switch ((TimesOfCharge)enm)
            {
                case TimesOfCharge.Arrear: return "A";
                case TimesOfCharge.InAdvance: return "V";
                default: throw new ArgumentException("Invalid TimesOfCharge.");
            }
        }

        /// <summary>
        /// Deserializes a persisted value of TimesOfCharge in string form in the db
        /// </summary>
        /// <param name="code">The String representation in the DB</param>
        /// <returns>the TimesOfCharge in enum form</returns>
        public override object GetInstance(object code)
        {           
            if ("A".Equals(code))
                return TimesOfCharge.Arrear;
            if ("V".Equals(code))
                return TimesOfCharge.InAdvance;

            throw new ArgumentException(
                "Cannot convert code '" + code + "' to TimesOfCharge.");
        }  
    }
}