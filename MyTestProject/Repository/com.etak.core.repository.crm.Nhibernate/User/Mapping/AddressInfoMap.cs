using com.etak.core.model;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.Mapping
{
    internal class AddressInfoMap : ClassMap<AddressInfo>
    {
        public AddressInfoMap()
        {
            Schema("dbo");
            Table("Addresses");
            DynamicUpdate();
            DynamicInsert();
            LazyLoad();
            Id(x => x.Id, "Id").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.Address, "Address");
            Map(x => x.Area, "Area");
            Map(x => x.Block, "Block");
            Map(x => x.BuildingDoor, "BuildingDoor");
            Map(x => x.City, "City");
            Map(x => x.Comments, "Comments");
            Map(x => x.CountryId, "CountryId");
            Map(x => x.CreateDate, "CreateDate");
            Map(x => x.CreateUser, "CreateUser");
            Map(x => x.Door, "Door");
            Map(x => x.HouseExtention, "HouseExtention");
            Map(x => x.HouseNo, "HouseNo");
            Map(x => x.Neighborhood, "Neighborhood");
            Map(x => x.PoBox, "PoBox");
            Map(x => x.Portal, "Portal");
            Map(x => x.Stair, "Stair");
            Map(x => x.State, "State");
            Map(x => x.Status, "Status");
            Map(x => x.Suburb, "Suburb");
            Map(x => x.ZipCode, "ZipCode");

            //HasOne(x => x.User).PropertyRef(x => x.UserAddress);

            HasMany(x => x.Owners).KeyColumn("AddressId").Inverse().Cascade.All();
        }
    }
}