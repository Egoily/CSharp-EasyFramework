using System;
using com.etak.core.model;
using com.etak.core.model.subscription;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.UnitTests.dtoConverter
{
    [TestFixture]
    public class SubscriptionDtoMappingTest
    {
        [Test()]
        public static void ToCore_CorrectSubscriptionDTO_ShouldReturnResourceMBInfo()
        {
            var subsDto = CreateDefaultObject.Create<SubscriptionDTO>(1);
            
            var subsInfo = subsDto.ToCore();

            CheckAreEquals(subsInfo, subsDto, true);

        }

        [Test()]
        public static void ToDto_CorrectResourceMBInfo_ShouldReturnSubscriptionDTO()
        {
            var subsInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            var cust1 = CreateDefaultObject.Create<CustomerInfo>();
            var dealer = CreateDefaultObject.Create<DealerInfo>();
            subsInfo.CustomerInfo = cust1;
            subsInfo.OperatorInfo = dealer;

            var subsDto = subsInfo.ToDto();

            CheckAreEquals(subsInfo, subsDto, false);
        }

        /// <summary>
        /// Check all the properties. If it's a conversion from Dto to Core, the CustomerInfo
        /// and DealerInfo are not filled.
        /// </summary>
        /// <param name="subsInfo">Core Object</param>
        /// <param name="subsDto">Dto Object</param>
        /// <param name="fromDto">Determines if we need to check the customerinfo and the dealerinfo</param>
        private static void CheckAreEquals(ResourceMBInfo subsInfo, SubscriptionDTO subsDto, Boolean fromDto)
        {
            Assert.AreEqual(subsDto.ActiveDeadlineDate, subsInfo.ActiveDeadlineDate);
            Assert.AreEqual(subsDto.BearerServiceList, subsInfo.BearerServiceList);
            Assert.AreEqual(subsDto.CBPassword, subsInfo.CBPassword);
            Assert.AreEqual(subsDto.CBSubsoption, subsInfo.CBSubsoption);
            Assert.AreEqual(subsDto.CBWrongAttempts, subsInfo.CBWrongAttempts);
            Assert.AreEqual(subsDto.Calculation, subsInfo.Calculation);
            Assert.AreEqual(subsDto.ChangeStatusDate, subsInfo.ChangeStatusDate);
            Assert.AreEqual(subsDto.CreateDate, subsInfo.CreateDate);
            Assert.AreEqual(subsDto.ICCId, subsInfo.ICC);
            Assert.AreEqual(subsDto.IMSI, subsInfo.IMSI);
            Assert.AreEqual(subsDto.Id, subsInfo.ResourceID);
            Assert.AreEqual(subsDto.LastConsumeDate, subsInfo.LastConsumeDate);
            Assert.AreEqual(subsDto.LastUsed, subsInfo.LastUsed);
            Assert.AreEqual(subsDto.MSISDN, subsInfo.Resource);
            Assert.AreEqual(subsDto.MainNumberStatus, subsInfo.MainNumberStatus);
            Assert.AreEqual(subsDto.MainNumberVoiceMailStatus, subsInfo.MainNumberVoiceMailStatus);
            Assert.AreEqual(subsDto.MobileType, subsInfo.MobileType);
            Assert.AreEqual(subsDto.MsIsdnAlertInd, subsInfo.MsIsdnAlertInd);
            Assert.AreEqual(subsDto.NAM, subsInfo.NAM);
            Assert.AreEqual(subsDto.OCPPlmnTemplateId, subsInfo.OCPPlmnTemplateId);
            Assert.AreEqual(subsDto.ODBMask, subsInfo.ODBMask);
            Assert.AreEqual(subsDto.PINInvalidTimes, subsInfo.PINInvalidTimes);
            Assert.AreEqual(subsDto.PINInvalidTimesTotal, subsInfo.PINInvalidTimesTotal);
            Assert.AreEqual(subsDto.PUK, subsInfo.PUK);
            Assert.AreEqual(subsDto.PortedNO, subsInfo.PortedNO);
            Assert.AreEqual(subsDto.ProvisionId, subsInfo.ProvisionId);
            Assert.AreEqual(subsDto.Remarks, subsInfo.Remarks);
            Assert.AreEqual(subsDto.StartDate, subsInfo.StartDate);
            Assert.AreEqual(subsDto.Status, subsInfo.StatusID);
            Assert.AreEqual(subsDto.TeleServiceList, subsInfo.TeleServiceList);
            Assert.AreEqual(subsDto.TempNO, subsInfo.TempNO);
            Assert.AreEqual(subsDto.UserId, subsInfo.UserID);
            Assert.AreEqual(subsDto.UssdAllowed, subsInfo.UssdAllowed);
            Assert.AreEqual(subsDto.WelcomeSMS, subsInfo.WelcomeSMS);

            if (!fromDto)
            {
                Assert.AreEqual(subsDto.CustomerId, subsInfo.CustomerInfo.CustomerID);
                Assert.AreEqual(subsDto.OperatorInfo, subsInfo.OperatorInfo.DealerID);
            }
        }
    }
}
