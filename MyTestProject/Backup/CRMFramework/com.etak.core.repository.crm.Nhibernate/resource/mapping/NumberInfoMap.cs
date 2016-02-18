using com.etak.core.model;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.resource.mapping
{
    class NumberInfoMap : ClassMap<NumberInfo>
    {
        public NumberInfoMap()
        {
            Schema("dbo");
            Table("SYS_NPM");
            DynamicUpdate();
            DynamicInsert();

            Id(x => x.Resource).
                Column("Resource").CustomType("AnsiString").Length(25).
                GeneratedBy.Assigned();

            HasOne(x => x.NumberProperty).Cascade.All();
            HasMany(x => x.NumberDealerSharing).KeyColumn("RESOURCE").Inverse();

            Map(x => x.TrafficTypeID, "TrafficTypeID");
            Map(x => x.CategoryID, "CategoryID");
            Map(x => x.UpdateUserID, "UpdateUserID");
            Map(x => x.UpdateDate, "UpdateDate");
            Map(x => x.CreateUserID, "CreateUserID");
            Map(x => x.CreateDate, "CreateDate");
            Map(x => x.DataStatus, "DataStatus");
        }
    }
}
