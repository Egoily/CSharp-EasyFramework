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
    public class RmPromotionPlanDetailDtoMappingTest
    {
        [Test]
        public static void ToDto_CorrectRmPromotionPlanDetailInfo_ShouldReturnRmPromotionPlanDetailInfoDTO()
        {
            RmPromotionPlanDetailInfo core = CreateDefaultObject.Create<RmPromotionPlanDetailInfo>();

            var dto = core.ToDto();

            CheckAreEquals(core, dto);
        }

        [Test]
        public static void ToCore_CorrectRmPromotionPlanDetailInfoDTO_ShouldReturnRmPromotionPlanDetailInfo()
        {
            RmPromotionPlanDetailInfoDTO dto = CreateDefaultObject.Create<RmPromotionPlanDetailInfoDTO>();

            var core = dto.ToCore();

            CheckAreEquals(core, dto);
        }

        private static void CheckAreEquals(RmPromotionPlanDetailInfo core, RmPromotionPlanDetailInfoDTO dto)
        {
            Assert.AreEqual(dto.ApplyOnRoaming, core.ApplyOnRoaming);
            Assert.AreEqual(dto.ApplyOnSuperOnNet, core.ApplyOnSuperOnNet);
            Assert.AreEqual(dto.BasePromotionPlanDetailId, core.BasePromotionPlanDetailId);
            Assert.AreEqual(dto.BlackListId, core.BlackListId);
            Assert.AreEqual(dto.CallDirectionId, core.CallDirectionId);
            Assert.AreEqual(dto.CountryCode, core.CountryCode);
            Assert.AreEqual(dto.CurrencyId, core.CurrencyId);
            Assert.AreEqual(dto.DateCategoryId, core.DateCategoryId);
            Assert.AreEqual(dto.DateCategoryTypeId, core.DateCategoryTypeId);
            Assert.AreEqual(dto.DeleteFlag, core.DeleteFlag);
            Assert.AreEqual(dto.DiscountMethodId, core.DiscountMethodId);
            Assert.AreEqual(dto.EndDate, core.EndDate);
            Assert.AreEqual(dto.Limit, core.Limit);
            Assert.AreEqual(dto.LimitPerCall, core.LimitPerCall);
            Assert.AreEqual(dto.LimitPerDay, core.LimitPerDay);
            Assert.AreEqual(dto.LimitUnit, core.LimitUnit);
            Assert.AreEqual(dto.MaximumAllowedBalance, core.MaximumAllowedBalance);
            Assert.AreEqual(dto.NumberCategoryId, core.NumberCategoryId);
            Assert.AreEqual(dto.OverLimitRateplanId, core.OverLimitRateplanId);
            Assert.AreEqual(dto.PromotionMethodId, core.PromotionMethodId);
            Assert.AreEqual(dto.PromotionPlanDetailId, core.PromotionPlanDetailId);
            Assert.AreEqual(dto.PromotionPlanDetailName, core.PromotionPlanDetailName);
            Assert.AreEqual(dto.PromotionTypeId, core.PromotionTypeId);
            Assert.AreEqual(dto.PromotionTypeId, core.PromotionTypeId);
            Assert.AreEqual(dto.Prompt, core.Prompt);
            Assert.AreEqual(dto.RatePlanId, core.RatePlanId);
            Assert.AreEqual(dto.RmPromotionPlanInfo.PromotionPlanId, core.RmPromotionPlanInfo.PromotionPlanId);
            Assert.AreEqual(dto.ServiceTypeId, core.ServiceTypeId);
            Assert.AreEqual(dto.Setup, core.Setup);
            Assert.AreEqual(dto.StartDate, core.StartDate);
            Assert.AreEqual(dto.SubServiceTypeId, core.SubServiceTypeId);
            Assert.AreEqual(dto.Tariff1, core.Tariff1);
            Assert.AreEqual(dto.Tariff2, core.Tariff2);
            Assert.AreEqual(dto.UnitCategoryId, core.UnitCategoryId);
            Assert.AreEqual(dto.UsageFee, core.UsageFee);
            Assert.AreEqual(dto.WalletTypeId, core.WalletTypeId);
            Assert.AreEqual(dto.WhiteList, core.WhiteList);
        }
    }
}
