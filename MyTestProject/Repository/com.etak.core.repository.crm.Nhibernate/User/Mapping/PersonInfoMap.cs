using com.etak.core.model;
using com.etak.core.model.Enums;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.Mapping
{
    internal class PersonInfoMap : ClassMap<PersonInfo>
    {
        public PersonInfoMap()
        {
            Schema("dbo");
            Table("Persons");
            DynamicUpdate();
            LazyLoad();
            Id(x => x.Id, "Id").GeneratedBy.Custom<PrefixIdGenerator>();

            Map(x => x.Surname).Column("Surname").Not.Nullable();
            Map(x => x.GivenName).Column("GivenName").Not.Nullable();
            Map(x => x.Gender).Column("Gender").CustomType<Gender>().Not.Nullable();
            Map(x => x.Age).Column("Age");
            Map(x => x.Comments).Column("Comments");

            HasOne(x => x.Profile).PropertyRef(x => x.Owner).Cascade.All();
        }
    }
}