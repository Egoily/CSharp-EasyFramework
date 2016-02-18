using com.etak.core.model;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.Mapping
{
    class AddressInfoMap : ClassMap<AddressInfo>
    {        
        public AddressInfoMap()
        {
            Schema("dbo");
            Table("ADDRESSES");
            DynamicUpdate();

            Id(x => x.Id, "ADDRESSID").GeneratedBy.Custom<PrefixIdGenerator>();
            HasManyToMany(x => x.AddressUsages).Component(y =>
                {
                    y.ParentReference(z => z.Address);
                    y.Map(z => z.UsageType).CustomType<AddressUsages>().Column("USAGE_TYPE");
                    y.References(z => z.Customer).Column("CUSTOMERID");
                })
                .ParentKeyColumn("ADDRESSID")
                .Table("CRM_CUSTOMERS_ADDRESSES")
                .Inverse();

            Map(x => x.Address, "ADDRESS");
            Map(x => x.Area, "AREA");
            Map(x => x.Block, "BLOCK");
            Map(x => x.BuildingDoor, "BUILDINGDOOR");
            Map(x => x.City, "CITY");
            Map(x => x.Comments, "COMMENTS");      
            Map(x => x.CountryId, "COUNTRYID");
            Map(x => x.CreateDate, "CREATEDATE");
            Map(x => x.CreateUser, "CREATEUSER");
            Map(x => x.Door, "DOOR");
            Map(x => x.HouseExtention, "HOUSEEXTENSION");
            Map(x => x.HouseNo, "HOUSENO");
            Map(x => x.Neighborhood, "NEIGHBORHOOD");
            Map(x => x.PoBox, "POBOX");
            Map(x => x.Portal, "PORTAL");
            Map(x => x.Stair, "STAIR");
            Map(x => x.State, "STATE");
            Map(x => x.Status, "STATUS");
            Map(x => x.Suburb, "SUBURB");
            Map(x => x.ZipCode, "ZIPCODE");
        }
    }
}
