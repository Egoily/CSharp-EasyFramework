using System;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Class to map to Nhibernate class Account
    /// </summary>
    public class AccountMap : ClassMap<Account> 
    {
       
        // CREATE TABLE CRM_ACCOUNTS
        //( 
        //    ACCOUNTID            bigint  NOT NULL ,
        //    ACCOUNTTYPEID        smallint  NULL ,
        //    NAMEID               int  NULL ,
        //    DESCRIPTIONID        int  NULL ,
        //    EXTERNAL_GROUPID     int  NULL ,
        //    BILLCYCLEID          smallint  NULL ,
        //    LAST_BILLRUNID       int  NULL ,
        //    PRIMARY_CUSTOMERID   int  NULL ,
        //    STATUSID             integer  NULL ,
        //    BALANCE_UNITDIMENSIONID int  NULL ,
        //    BALANCE_UNITTYPEID   int  NULL 
        //)
       
        /// <summary>
        /// The constructor called by fluent to map the class
        /// </summary>
        public AccountMap()
        {
            Schema("dbo");
            Table("CRM_ACCOUNTS");
            DynamicUpdate();            
            DiscriminateSubClassesOnColumn<Int32>("BALANCE_UNITDIMENSIONID", 100)                
                .Not.Nullable();

            Id(x => x.Id, "ACCOUNTID").GeneratedBy.Custom < PrefixIdGenerator >();
            HasOne(x => x.Balance).Cascade.All();

            References(x => x.Name).Cascade.All() .Column("NAMEID");
            References(x => x.Description).Cascade.All().Column("DESCRIPTIONID");

            References(x => x.BillingCycle).Column("BILLCYCLEID");
            References(x => x.CurrentAsignedCustomer).Column("PRIMARY_CUSTOMERID");
            References(x => x.LastBillRun).Column("LAST_BILLRUNID");
        }
    }
}