using com.etak.core.model;
using com.etak.core.model.resource;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.test.dtoConverter
{
    [TestFixture]
    public class MSISDNResourceDtoMappingTest
    {
        [Test()]
        public static void MSISDNResourceCoreToDto()
        {
            var numberInfo = CreateDefaultObject.Create<NumberInfo>();

            var numberDto = numberInfo.ToDto();

            CheckAreEquals(numberInfo, numberDto);
        }

        [Test()]
        public static void MSISDNResourceDtoToCore()
        {
            var resDto = CreateDefaultObject.Create<MSISDNResourceDTO>();

            var resInfo = resDto.ToCore();

            CheckAreEquals(resInfo, resDto);
        }

        private static void CheckAreEquals(NumberInfo resInfo, MSISDNResourceDTO resDto)
        {
            Assert.AreEqual(resDto.Category, resInfo.CategoryID);
            Assert.AreEqual(resDto.MSISDN, resInfo.Resource);
        }
    }
}
