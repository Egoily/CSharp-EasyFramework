using com.etak.core.model;
using com.etak.core.model.usage;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.UnitTests.dtoConverter
{
    [TestFixture]
    public class UsageDetailDtoMappingTest
    {
        [Test()]
        public static void ToDto_CorrectUsageDetailRecord_ShouldReturnUsageDetailDTO()
        {
            var usageInfo = CreateDefaultObject.Create<UsageDetailRecord>();
            usageInfo.Subservicetypeid = DtoDictionaries.UsagesSubTypesEnumToInt[UsagesSubTypes.Data];

            var usageDto = usageInfo.ToDto();

            CheckAreEquals(usageInfo, usageDto);
        }

        private static void CheckAreEquals(UsageDetailRecord usageInfo, model.usage.UsageDetailDTO usageDto)
        {
            Assert.AreEqual(usageDto.Amount, (usageInfo.Amount1 + usageInfo.Amount2));
            Assert.AreEqual(usageDto.BNumber, usageInfo.Bnumber);
            Assert.AreEqual(usageDto.Bleg, usageInfo.Bleg);
            Assert.AreEqual(usageDto.CallDirectionId, usageInfo.Calldirectionid);
            Assert.AreEqual(usageDto.CallTypeId, usageInfo.Calltypeid);
            Assert.AreEqual(usageDto.EndDate, usageInfo.Enddate);
            Assert.AreEqual(usageDto.PromotionPlanId, usageInfo.Promotionplanid);
            Assert.AreEqual(usageDto.StartDate, usageInfo.Startdate);
            Assert.AreEqual(DtoDictionaries.UsagesSubTypesEnumToInt[usageDto.SubServiceTypeId], usageInfo.Subservicetypeid);
            Assert.AreEqual(usageDto.TSC, usageInfo.Tsc);
        }
    }
}
