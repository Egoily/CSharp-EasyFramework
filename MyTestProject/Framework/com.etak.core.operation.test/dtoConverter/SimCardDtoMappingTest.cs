using System;
using com.etak.core.model;
using com.etak.core.model.resource;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.test.dtoConverter
{
    [TestFixture]
    public class SimCardDtoMappingTest
    {

        [Test()]
        public static void SimCardCoreToDto()
        {
            var simInfo = CreateDefaultObject.Create<SIMCardInfo>();
            var dealerInfo = CreateDefaultObject.Create<DealerInfo>();
            simInfo.Dealer = dealerInfo;

            var simDto = simInfo.ToDto();

            CheckAreEquals(simInfo, simDto, false);
        }

        [Test()]
        public static void SimCardDtoToCore()
        {
            var simDto = CreateDefaultObject.Create<SimCardDTO>();

            var simInfo = simDto.ToCore();

            CheckAreEquals(simInfo, simDto, true);
        }

        private static void CheckAreEquals(SIMCardInfo simInfo, SimCardDTO simDto, Boolean fromDto)
        {
            if (!fromDto)
                Assert.AreEqual(simDto.DealerID, simInfo.Dealer.DealerID);

            Assert.AreEqual(simDto.ACC, simInfo.ACC);
            Assert.AreEqual(simDto.ADM1, simInfo.ADM1);
            Assert.AreEqual(simDto.ADM2, simInfo.ADM2);
            Assert.AreEqual(simDto.ActivateType, simInfo.ActivateType);
            Assert.AreEqual(simDto.AlgoID, simInfo.AlgoID);
            Assert.AreEqual(simDto.AlgorithmName, simInfo.AlgorithmName);
            Assert.AreEqual(simDto.AssignStatusId, simInfo.AssignStatusId);
            Assert.AreEqual(simDto.ChangeStatusDate, simInfo.ChangeStatusDate);
            Assert.AreEqual(simDto.EKI_Index, simInfo.EKI_Index);
            Assert.AreEqual(simDto.ICCID, simInfo.ICCID);
            Assert.AreEqual(simDto.IMSI1, simInfo.IMSI1);
            Assert.AreEqual(simDto.IMSI2, simInfo.IMSI2);
            Assert.AreEqual(simDto.IMSI3, simInfo.IMSI3);
            Assert.AreEqual(simDto.IMSI4, simInfo.IMSI4);
            Assert.AreEqual(simDto.IMSI5, simInfo.IMSI5);
            Assert.AreEqual(simDto.IMSI6, simInfo.IMSI6);
            Assert.AreEqual(simDto.IMSI7, simInfo.IMSI7);
            Assert.AreEqual(simDto.IMSI8, simInfo.IMSI8);
            Assert.AreEqual(simDto.IMSI9, simInfo.IMSI9);
            Assert.AreEqual(simDto.IMSI10, simInfo.IMSI10);
            Assert.AreEqual(simDto.IMSI11, simInfo.IMSI11);
            Assert.AreEqual(simDto.IMSI12, simInfo.IMSI12);
            Assert.AreEqual(simDto.IMSI13, simInfo.IMSI13);
            Assert.AreEqual(simDto.IMSI14, simInfo.IMSI14);
            Assert.AreEqual(simDto.IMSI15, simInfo.IMSI15);
            Assert.AreEqual(simDto.IsProvision, simInfo.IsProvision);
            Assert.AreEqual(simDto.KI, simInfo.KI);
            Assert.AreEqual(simDto.KIC2, simInfo.KIC2);
            Assert.AreEqual(simDto.KIC_0F, simInfo.KIC_0F);
            Assert.AreEqual(simDto.KID2, simDto.KID2);
            Assert.AreEqual(simDto.KID_0F, simInfo.KID_0F);
            Assert.AreEqual(simDto.KIK2, simInfo.KIK2);
            Assert.AreEqual(simDto.KIK_0F, simInfo.KIK_0F);
            Assert.AreEqual(simDto.MSISDN, simInfo.MSISDN);
            Assert.AreEqual(simDto.ManufactureDate, simInfo.ManufactureDate);
            Assert.AreEqual(simDto.ManufacturerEncryptionType, simInfo.ManufacturerEncryptionType);
            Assert.AreEqual(simDto.ManufacturerID, simInfo.ManufacturerID);
            Assert.AreEqual(simDto.OPC, simInfo.OPC);
            Assert.AreEqual(simDto.PIN1, simInfo.PIN1);
            Assert.AreEqual(simDto.PIN2, simInfo.PIN2);
            Assert.AreEqual(simDto.PUK1, simInfo.PUK1);
            Assert.AreEqual(simDto.PUK2, simInfo.PUK2);
            Assert.AreEqual(simDto.SIMType, simInfo.SIMType);
            Assert.AreEqual(simDto.Status, simInfo.Status);
            Assert.AreEqual(simDto.TEMPORARY_IMSI, simInfo.TEMPORARY_IMSI);
            Assert.AreEqual(simDto.TEMPORARY_MSISDN, simInfo.TEMPORARY_MSISDN);
            

        }
    }
}
