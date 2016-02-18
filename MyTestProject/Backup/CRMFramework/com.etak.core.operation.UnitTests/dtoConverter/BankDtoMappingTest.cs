using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.UnitTests.dtoConverter
{
    [TestFixture()]
    public class BankDtoMappingTest
    {
        [Test()]
        public static void ToDto_CorrectBankInfo_ShouldReturnBankInformationDTO()
        {
            var bankInfo = CreateDefaultObject.Create<BankInfo>();

            var bankDto = bankInfo.ToDto();

            CheckAreEquals(bankInfo, bankDto);

        }

        [Test()]
        public static void ToCore_CorrectBankInformationDTO_ShouldReturnBankInfo()
        {
            var bankDto = CreateDefaultObject.Create<BankInformationDTO>();

            var bankInfo = bankDto.ToCore();

            CheckAreEquals(bankInfo, bankDto);
        }


        private static void CheckAreEquals(BankInfo bankInfo, BankInformationDTO bankDto)
        {
            Assert.AreEqual(bankDto.ABI, bankInfo.ABI);
            Assert.AreEqual(bankDto.AccountCode, bankInfo.AccountCode);
            Assert.AreEqual(bankDto.BankCode, bankInfo.BankCode);
            Assert.AreEqual(bankDto.BankName, bankInfo.BankName);
            Assert.AreEqual(bankDto.BankNumber, bankInfo.BankNumber);
            Assert.AreEqual(bankDto.CAB, bankInfo.CAB);
            Assert.AreEqual(bankDto.CVC, bankInfo.CVC);
            Assert.AreEqual(bankDto.City, bankInfo.City);
            Assert.AreEqual(bankDto.CountryID, bankInfo.CountryID);
            Assert.AreEqual(bankDto.CreateDate, bankInfo.CreateDate);
            Assert.AreEqual(bankDto.EndDate, bankInfo.EndDate);
            Assert.AreEqual(bankDto.IBAN, bankInfo.IBAN);
            Assert.AreEqual(bankDto.Owner, bankInfo.Owner);
            Assert.AreEqual(bankDto.StartDate, bankInfo.StartDate);
            Assert.AreEqual(bankDto.Swift, bankInfo.Swift);
            Assert.AreEqual(bankDto.ValidDate, bankInfo.ValidDate);
        }

    }
}
