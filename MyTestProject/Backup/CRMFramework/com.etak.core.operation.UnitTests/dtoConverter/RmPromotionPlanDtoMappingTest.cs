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
    public class RmPromotionPlanDtoMappingTest
    {
        [Test]
        public static void ToDto_CorrectRmPromotionPlanInfo_ShouldReturnRmPromotionPlanInfoDTO()
        {
            RmPromotionPlanInfo core = CreateDefaultObject.Create<RmPromotionPlanInfo>();

            var dto = core.ToDto();

            CheckAreEquals(core, dto);
        }

        [Test]
        public static void ToCore_CorrectRmPromotionPlanInfoDTO_ShouldReturnRmPromotionPlanInfo()
        {
            RmPromotionPlanInfoDTO dto = CreateDefaultObject.Create<RmPromotionPlanInfoDTO>();

            var core = dto.ToCore();

            CheckAreEquals(core, dto);
        }

        private static void CheckAreEquals(RmPromotionPlanInfo core, RmPromotionPlanInfoDTO dto)
        {
            Assert.AreEqual(dto.APIVisible, core.APIVisible);
            Assert.AreEqual(dto.Accumulative, core.Accumulative);
            Assert.AreEqual(dto.ActiveWithoutCredit, core.ActiveWithoutCredit);
            Assert.AreEqual(dto.CrmMSISDNGroupTypeInfoID, core.CrmMSISDNGroupTypeInfoID);
            Assert.AreEqual(dto.CustomerCareVisible, core.CustomerCareVisible);
            Assert.AreEqual(dto.DailyResetTime, core.DailyResetTime);
            Assert.AreEqual(dto.DealerId, core.DealerId);
            Assert.AreEqual(dto.DiscountMonthFeeForRenew, core.DiscountMonthFeeForRenew);
            Assert.AreEqual(dto.EndDate, core.EndDate);
            Assert.AreEqual(dto.Exclusive, core.Exclusive);
            Assert.AreEqual(dto.MonthFee, core.MonthFee);
            Assert.AreEqual(dto.NotificationSmsTemplateId, core.NotificationSmsTemplateId);
            Assert.AreEqual(dto.Periodic, core.Periodic);
            Assert.AreEqual(dto.Priority, core.Priority);
            Assert.AreEqual(dto.PromotionCategoryId, core.PromotionCategoryId);
            Assert.AreEqual(dto.PromotionGroupId, core.PromotionGroupId);
            Assert.AreEqual(dto.PromotionPlanId, core.PromotionPlanId);
            Assert.AreEqual(dto.PromotionPlanName, core.PromotionPlanName);
            Assert.AreEqual(dto.PromotionType, core.PromotionType);
            Assert.AreEqual(dto.Prorate, core.Prorate);
            Assert.AreEqual(dto.RemoveImmediatelyFlag, core.RemoveImmediatelyFlag);
            Assert.AreEqual(dto.RenewAutomatically, core.RenewAutomatically);
            Assert.AreEqual(dto.ResetLimit, core.ResetLimit);
            Assert.AreEqual(dto.ResetPeriod, core.ResetPeriod);
            Assert.AreEqual(dto.ResetPeriodUnit, core.ResetPeriodUnit);
            Assert.AreEqual(dto.RestrictDuration, core.RestrictDuration);
            Assert.AreEqual(dto.RestrictUnit, core.RestrictUnit);
            Assert.AreEqual(dto.SelfCareVisible, core.SelfCareVisible);
            Assert.AreEqual(dto.SmsNotification, core.SmsNotification);
            Assert.AreEqual(dto.StartDate, core.StartDate);
            Assert.AreEqual(dto.SubscriptionFee, core.SubscriptionFee);
            Assert.AreEqual(dto.SubscriptionPeriod, core.SubscriptionPeriod);
            Assert.AreEqual(dto.SubscriptionPeriodUnit, core.SubscriptionPeriodUnit);
            Assert.AreEqual(dto.TimePointForChargeFee, core.TimePointForChargeFee);
            Assert.AreEqual(dto.RmPromotionPlanDetailInfoList.Count, core.RmPromotionPlanDetailInfoList.Count);
        }
    }
}
