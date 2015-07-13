using com.etak.core.model;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.Mapping
{
    internal class ProfileInfoMap : ClassMap<ProfileInfo>
    {
        public ProfileInfoMap()
        {
            Schema("dbo");
            Table("PersonProfile");
            DynamicUpdate();
            LazyLoad();
            Id(x => x.Id, "Id").GeneratedBy.Custom<PrefixIdGenerator>();

            Map(x => x.Phone).Column("Phone");
            Map(x => x.Email).Column("Email");

            References(x => x.Owner, "PersonId");
            References(x => x.Address, "AddressId");

            References(x => x.Father, "FatherId");
            References(x => x.Mother, "MotherId");
            References(x => x.Spouse, "SpouseId");
        }
    }
}