using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.subscription;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.UnitTests.dtoConverter
{
    [TestFixture]
    public class CrmCustomersPromotionInfoMappingTest
    {
        [Test]
        public static void ToDto_CorrectCrmCustomersPromotionInfo_ShouldReturnCrmCustomersPromotionInfoDTO()
        {
            CrmCustomersPromotionInfo ccpInfo = CreateDefaultObject.Create<CrmCustomersPromotionInfo>();

            var ccpDto = ccpInfo.ToDto();

            CheckAreEquals(ccpInfo, ccpDto);
        }

        [Test]
        public static void ToCoreDto_CorrectCrmCustomersPromotionInfoDTO_ShouldReturnCrmCustomersPromotionInfo()
        {
            CrmCustomersPromotionInfoDTO ccpDto = CreateDefaultObject.Create<CrmCustomersPromotionInfoDTO>();

            var ccpInfo = ccpDto.ToCore();

            CheckAreEquals(ccpInfo, ccpDto);
        }

        
        private static void CheckAreEquals(CrmCustomersPromotionInfo ccpInfo, model.subscription.CrmCustomersPromotionInfoDTO ccpDto)
        {
            Assert.AreEqual(ccpDto.Active, ccpInfo.Active);
            Assert.AreEqual(ccpDto.ActiveWithoutCredit, ccpInfo.ActiveWithoutCredit);
            Assert.AreEqual(ccpDto.BatchId, ccpInfo.BatchId);
            Assert.AreEqual(ccpDto.BatchNo, ccpInfo.BatchNo);
            Assert.AreEqual(ccpDto.CurrentLimit, ccpInfo.CurrentLimit);
            Assert.AreEqual(ccpDto.CustomerId, ccpInfo.CustomerId);
            Assert.AreEqual(ccpDto.DeActiveReason, ccpInfo.DeActiveReason);
            Assert.AreEqual(ccpDto.EndDate, ccpInfo.EndDate);
            Assert.AreEqual(ccpDto.FirstUsed, ccpInfo.FirstUsed);
            Assert.AreEqual(ccpDto.IsBasePromotion, ccpInfo.IsBasePromotion);
            Assert.AreEqual(ccpDto.Priority, ccpInfo.Priority);
            Assert.AreEqual(ccpDto.PromotionId, ccpInfo.PromotionId);
            Assert.AreEqual(ccpDto.RenewAutomatically, ccpInfo.RenewAutomatically);
            Assert.AreEqual(ccpDto.RenewDate, ccpInfo.RenewDate);
            Assert.AreEqual(ccpDto.RenewalCount, ccpInfo.RenewalCount);
            Assert.AreEqual(ccpDto.StartDate, ccpInfo.StartDate);
            Assert.AreEqual(ccpDto.WhiteList, ccpInfo.WhiteList);
            //PromtionDetail will be checked by it's own test
            //Assert.AreEqual(ccpDto.PromotionDetail);
        }

    }
}
