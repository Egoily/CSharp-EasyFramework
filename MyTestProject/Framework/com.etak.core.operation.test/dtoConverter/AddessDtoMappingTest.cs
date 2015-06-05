using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.test.dtoConverter
{
    [TestFixture]
    public class AddessDtoMappingTest
    {
        //[TestFixtureSetUp()]
        //public static void initialize()
        //{
        //    RepositoryManager.AddAssemby(typeof(SessionFactoryHelper).Assembly);
        //}

        [Test()]
        public static void AddressCoreToDto()
        {
            AddressInfo addInfo = CreateDefaultObject.Create<AddressInfo>(1);

            var addDto = addInfo.ToDto();

            CheckAreEquals(addInfo, addDto);
        }

        [Test()]
        public static void AddressDtoToCore()
        {
            AddressDTO addDto = CreateDefaultObject.Create<AddressDTO>(1);

            var addInfo = addDto.ToCore();

            CheckAreEquals(addInfo, addDto);
        }

        private static void CheckAreEquals(AddressInfo addInfo, AddressDTO addDto)
        {
            Assert.AreEqual(addDto.Address, addInfo.Address);
            Assert.AreEqual(addDto.Area, addInfo.Area);
            Assert.AreEqual(addDto.Block, addInfo.Block);
            Assert.AreEqual(addDto.BuildingDoor, addInfo.BuildingDoor);
            Assert.AreEqual(addDto.City, addInfo.City);
            Assert.AreEqual(addDto.CountryId, addInfo.CountryId);
            Assert.AreEqual(addDto.Door, addInfo.Door);
            Assert.AreEqual(addDto.HouseExtension, addInfo.HouseExtention);
            Assert.AreEqual(addDto.HouseNo, addInfo.HouseNo);
            Assert.AreEqual(addDto.Neighborhood, addInfo.Neighborhood);
            Assert.AreEqual(addDto.PoBox, addInfo.PoBox);
            Assert.AreEqual(addDto.Portal, addInfo.Portal);
            Assert.AreEqual(addDto.Stair, addInfo.Stair);
            Assert.AreEqual(addDto.State, addInfo.State);
            Assert.AreEqual(addDto.Status, addInfo.Status);
            Assert.AreEqual(addDto.Suburb, addInfo.Suburb);
            Assert.AreEqual(addDto.ZipCode, addInfo.ZipCode);
        }
    }
}
