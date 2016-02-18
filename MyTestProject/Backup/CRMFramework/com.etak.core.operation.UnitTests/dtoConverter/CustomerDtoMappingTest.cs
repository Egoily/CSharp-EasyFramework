using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.operation.dtoConverters;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.operation.UnitTests.dtoConverter
{
    [TestFixture]
    public class CustomerDtoMappingTest
    {
        [Test()]
        public static void ToDto_CorrectCustomerInfo_ShouldReturnCustomerDTO()
        {
            var custInfo = CreateDefaultObject.Create<CustomerInfo>(1);
            custInfo.BankInfo.FirstOrDefault().EndDate = DateTime.Now.AddDays(1);
            custInfo.PropertyInfo.FirstOrDefault().PendingStatus = 6;
            var custAdd = CreateDefaultObject.Create<CustomerAddress>(1);
            custAdd.Address = CreateDefaultObject.Create<AddressInfo>(1);
            custAdd.Customer = custInfo;
            
            custInfo.Addresses = new List<CustomerAddress>()
            {
                custAdd,
            };

            var custDto = custInfo.ToDto();

            ChekAreEquals(custInfo, custDto);

        }

        [Test()]
        public static void ToCore_CorrectCustomerDTO_ShouldReturnCustomerInfo()
        {
            var custDto = CreateDefaultObject.Create<CustomerDTO>(1);
            custDto.CustomerData = CreateDefaultObject.Create<CustomerDataDTO>(1);
            custDto.CustomerData.BankInformation = CreateDefaultObject.Create<BankInformationDTO>(1);
            custDto.CustomerData.DocumentType = DocumentTypes.CIF;
            var addDto = CreateDefaultObject.Create<AddressDTO>(1);
            custDto.CustomerData.FiscalAddress = addDto;
            custDto.CustomerData.CustomerAddress = addDto;

            var custInfo = custDto.ToCore();

            ChekAreEquals(custInfo, custDto);
        }

        private static void ChekAreEquals(CustomerInfo custInfo, CustomerDTO custDto)
        {
            //Property Info
            Assert.AreEqual(custDto.CustomerData.Email, custInfo.PropertyInfo.First().Email);
            Assert.AreEqual(DtoDictionaries.DocEnumToIntMapper[custDto.CustomerData.DocumentType], custInfo.PropertyInfo.First().IDType);
            Assert.AreEqual(custDto.CustomerData.DocumentNumber, custInfo.PropertyInfo.First().IDNumber);
            Assert.AreEqual(custDto.ExternalCustomerId, custInfo.PropertyInfo.First().ExternalId);
            Assert.AreEqual(custDto.CustomerData.BirthDay, custInfo.PropertyInfo.First().Birthday);


            //MvnoCustomerPropertyInfo
            Assert.AreEqual(custDto.CustomerData.Nationality, custInfo.MVNOCustomerPropertyInfo.First().Nationality);

            Assert.AreEqual(custDto.ParentCustomerId, custInfo.ParentID);
            Assert.AreEqual((int?)custDto.CustomerData.Title, custInfo.TitleID);
            Assert.AreEqual((int?)custDto.CustomerData.Gender, custInfo.GenderID);
            Assert.AreEqual(custDto.CustomerData.Initials, custInfo.Initials);
            Assert.AreEqual(custDto.CustomerData.FirstName, custInfo.FirstName);
            Assert.AreEqual(custDto.CustomerData.LastName, custInfo.LastName);
            Assert.AreEqual(custDto.CustomerData.LastName2, custInfo.LastName2);
            Assert.AreEqual(custDto.CustomerData.MiddleName, custInfo.MiddleName);
            Assert.AreEqual(custDto.CustomerData.Company, custInfo.Company);
            Assert.AreEqual(custDto.CustomerData.Telephone, custInfo.Telephone);
            Assert.AreEqual(custDto.CustomerData.Telefax, custInfo.Telefax);
            Assert.AreEqual(custDto.CustomerData.Mobile, custInfo.Mobile);
            Assert.AreEqual(custDto.CustomerData.Email, custInfo.Email);

            //BankInfo
            var bankInfo = custInfo.BankInfo.FirstOrDefault();
            var bankDto = custDto.CustomerData.BankInformation;
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
            

            Assert.AreEqual(custDto.CustomerId, custInfo.CustomerID);
            
        }

       
    }
}
