using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.UpdateCustomerData;
using com.etak.core.customer.message.CreateAddresses;
using com.etak.core.customer.message.UpdateCustomerInfo;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.dtoConverters;
using com.etak.core.repository.crm.customer;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.UpdateCustomerData
{
    [TestFixture()]
    public class UpdateCustomerDataBizOpUnitTests : AbstractSinglePhaseOrderProcessorTest<UpdateCustomerDataBizOp, UpdateCustomerDataDTORequest, UpdateCustomerDataDTOResponse, UpdateCustomerDataInternalRequest, UpdateCustomerDataInternalResponse, UpdateCustomerOrder>
    {
        private IMicroService<CreateAddressesRequest, CreateAddressesResponse> mockMicroServiceCraeteAddress;
        private IMicroService<UpdateCustomerInfoRequest, UpdateCustomerInfoResponse> mockMicroServiceUpdateCustomer;
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;

        [TestFixtureSetUp()]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCraeteAddress = MockMicroService<CreateAddressesRequest, CreateAddressesResponse>();
            mockMicroServiceUpdateCustomer = MockMicroService<UpdateCustomerInfoRequest, UpdateCustomerInfoResponse>();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void UpdateCustomerDataBizOp_GivenCustomer_ReturnOKCustomer()
        {
            #region populate actualObj

            //populate actual customer Info
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.PropertyInfo[0].IDType = (Int32)DocumentTypes.Passport;
            actualCustomerInfo.PropertyInfo[0].IDNumber = "DocumentNumber1";

            //populate Address Value
            actualCustomerInfo.Addresses = new List<CustomerAddress>();
            var actualAddress = CreateDefaultObject.Create<CustomerAddress>();
            var actualAddressInfo = CreateDefaultObject.Create<AddressInfo>();
            actualAddressInfo.HouseExtention = "HouseExtension1";
            actualAddressInfo.City = "Bandung";
            actualAddress.Address = actualAddressInfo;
            actualCustomerInfo.Addresses.Add(actualAddress);

            //populate BankInfo value
            actualCustomerInfo.BankInfo = new List<BankInfo>();
            var actualBankInfo = CreateDefaultObject.Create<BankInfo>();
            actualBankInfo.EndDate = null;
            actualCustomerInfo.BankInfo.Add(actualBankInfo);

            //populate ResourceMB

            actualCustomerInfo.ResourceMBInfo.Clear();
            var mockedResourceMb = CreateDefaultObject.Create<ResourceMBInfo>();
            mockedResourceMb.CustomerInfo = actualCustomerInfo;
            actualCustomerInfo.ResourceMBInfo.Add(mockedResourceMb);

            #endregion populate actualObj

            #region populate expectedDto

            //Expected Dto
            var expectedCustomerDto = CreateDefaultObject.Create<CustomerDTO>();
            var expectedCustomerData = CreateDefaultObject.Create<CustomerDataDTO>();
            var expectedAddressDto = CreateDefaultObject.Create<AddressDTO>();
            expectedCustomerData.DocumentType = DocumentTypes.Passport;
            expectedCustomerData.DocumentNumber = "DocumentNumber1";

            expectedAddressDto.City = "Bandung";
            expectedAddressDto.HouseExtension = "HouseExtension1";

            var expectedBankInformationDto = CreateDefaultObject.Create<BankInformationDTO>();
            expectedCustomerData.CustomerAddress = expectedAddressDto;
            expectedCustomerData.BankInformation = expectedBankInformationDto;
            expectedCustomerData.FiscalAddress = expectedAddressDto;
            expectedCustomerData.DeliveryAddress = expectedAddressDto;
            expectedBankInformationDto.EndDate = null;
            expectedBankInformationDto.City = "Bandung";
            expectedCustomerDto.ChildCustomers = new List<int>();
            expectedCustomerDto.CustomerData = expectedCustomerData;

            #endregion populate expectedDto

            #region mock MS Create Address

            var newCustInfo = expectedCustomerDto.ToCore();

            var actualCreateAddressesResponse = new CreateAddressesResponse();
            var actualCreateAddressInfo = CreateDefaultObject.Create<AddressInfo>();
            actualCreateAddressInfo.City = "Bandung";
            actualCreateAddressInfo.HouseExtention = "HouseExtension1";
            var actualAddressInfos = new List<AddressInfo> { actualCreateAddressInfo };

            var actualCustomerCreateAddress = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerCreateAddress.Addresses = new List<CustomerAddress>();
            var actualCreateAddress = CreateDefaultObject.Create<CustomerAddress>();
            actualCreateAddress.Address = actualCreateAddressInfo;
            actualCustomerCreateAddress.Addresses.Add(actualCreateAddress);
            actualCreateAddress.Customer = newCustInfo;
            actualCreateAddressesResponse.AddressInfos = actualAddressInfos;

            //define MS Create Address
            var actualCreateAddressesRequest = Arg.Is<CreateAddressesRequest>(ad => ad.CustomerAddresses.FirstOrDefault().Address.City == "Bandung");

            mockMicroServiceCraeteAddress.Process(actualCreateAddressesRequest, null).Returns(actualCreateAddressesResponse);

            var actualUpdateCustomerInfoResponse = CreateDefaultObject.Create<UpdateCustomerInfoResponse>();
            actualUpdateCustomerInfoResponse.CustomerInfo = actualCustomerInfo;

            #endregion mock MS Create Address

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //define MS UpdateCustomerInfo
            var actualUpdateCustomerInfo =
                Arg.Is<UpdateCustomerInfoRequest>(cust => cust.CustomerInfo.CustomerID == 1);
            mockMicroServiceUpdateCustomer.Process(actualUpdateCustomerInfo, null)
                 .Returns(actualUpdateCustomerInfoResponse);

            var requestDto = new UpdateCustomerDataDTORequest()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                Customer = expectedCustomerDto
            };

            MockAbstractSinglePhaseOrderProcessor(requestDto);

            #region Remock Repository

            //Remock Repo CustomerInfo
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            //CustomerInfo customerInfo = actualCustomerInfo;
            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(actualCustomerInfo);

            #endregion Remock Repository

            var response = CallBizOp(requestDto);
            Assert.AreEqual(response.Customer.CustomerData.FiscalAddress.City, expectedCustomerDto.CustomerData.FiscalAddress.City);
            Assert.AreEqual(ResultTypes.Ok, ResultTypes.Ok);
            Assert.IsTrue(response.Subscription.CustomerId == actualCustomerInfo.CustomerID);
        }

        [Test()]
        public void UpdateCustomerDataBizOp_GivenCustomerUpdateName_ReturnOKCustomer()
        {
            #region populate actualObj

            //populate actual customer Info
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.PropertyInfo[0].IDType = (Int32)DocumentTypes.Passport;
            actualCustomerInfo.PropertyInfo[0].IDNumber = "DocumentNumber1";

            //populate Address Value
            actualCustomerInfo.Addresses = new List<CustomerAddress>();
            var actualAddress = CreateDefaultObject.Create<CustomerAddress>();
            var actualAddressInfo = CreateDefaultObject.Create<AddressInfo>();
            actualAddressInfo.HouseExtention = "HouseExtension1";
            actualAddressInfo.City = "Bandung";
            actualAddress.Address = actualAddressInfo;
            actualCustomerInfo.Addresses.Add(actualAddress);

            //populate BankInfo value
            actualCustomerInfo.BankInfo = new List<BankInfo>();
            var actualBankInfo = CreateDefaultObject.Create<BankInfo>();
            actualBankInfo.EndDate = null;
            actualCustomerInfo.BankInfo.Add(actualBankInfo);

            #endregion populate actualObj

            #region populate expectedDto

            //Expected Dto
            var expectedCustomerDto = CreateDefaultObject.Create<CustomerDTO>();
            var expectedCustomerData = CreateDefaultObject.Create<CustomerDataDTO>();
            expectedCustomerData.FirstName = "NewName";
            var expectedAddressDto = CreateDefaultObject.Create<AddressDTO>();
            expectedCustomerData.DocumentType = DocumentTypes.Passport;
            expectedCustomerData.DocumentNumber = "DocumentNumber1";

            expectedAddressDto.City = "Bandung";
            expectedAddressDto.HouseExtension = "HouseExtension1";
            var expectedBankInformationDto = CreateDefaultObject.Create<BankInformationDTO>();
            expectedCustomerData.CustomerAddress = expectedAddressDto;
            expectedCustomerData.BankInformation = expectedBankInformationDto;
            expectedCustomerData.FiscalAddress = expectedAddressDto;
            expectedCustomerData.DeliveryAddress = expectedAddressDto;
            expectedBankInformationDto.EndDate = null;
            expectedBankInformationDto.City = "Bandung";
            expectedCustomerDto.ChildCustomers = new List<int>();
            expectedCustomerDto.CustomerData = expectedCustomerData;

            #endregion populate expectedDto

            #region mock MS Create Address

            var newCustInfo = expectedCustomerDto.ToCore();

            var actualUpdateCustomerInfoResponse = CreateDefaultObject.Create<UpdateCustomerInfoResponse>();
            actualUpdateCustomerInfoResponse.CustomerInfo = actualCustomerInfo;

            #endregion mock MS Create Address

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //define MS UpdateCustomerInfo
            var actualUpdateCustomerInfo =
                Arg.Is<UpdateCustomerInfoRequest>(cust => cust.CustomerInfo.CustomerID == 1);
            mockMicroServiceUpdateCustomer.Process(actualUpdateCustomerInfo, null)
                 .Returns(actualUpdateCustomerInfoResponse);

            var requestDto = new UpdateCustomerDataDTORequest()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                Customer = expectedCustomerDto
            };

            MockAbstractSinglePhaseOrderProcessor(requestDto);

            #region Remock Repository

            //Remock Repo CustomerInfo
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            //CustomerInfo customerInfo = actualCustomerInfo;
            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(actualCustomerInfo);

            #endregion Remock Repository

            var response = CallBizOp(requestDto);
            Assert.AreEqual(response.Customer.CustomerData.FiscalAddress.City, expectedCustomerDto.CustomerData.FiscalAddress.City);
            Assert.AreEqual(ResultTypes.Ok, ResultTypes.Ok);
        }

        [Test()]
        public void UpdateCustomerDataBizOp_GivenCustomer_ReturnNOKCustomer()
        {
            #region populate actualObj

            //populate actual customer Info
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.PropertyInfo[0].IDType = (Int32)DocumentTypes.Passport;
            actualCustomerInfo.PropertyInfo[0].IDNumber = "DocumentNumber1";

            //populate Address Value
            actualCustomerInfo.Addresses = new List<CustomerAddress>();
            var actualAddress = CreateDefaultObject.Create<CustomerAddress>();
            var actualAddressInfo = CreateDefaultObject.Create<AddressInfo>();
            actualAddressInfo.HouseExtention = "HouseExtension1";
            actualAddressInfo.City = "Bandung";
            actualAddress.Address = actualAddressInfo;
            actualCustomerInfo.Addresses.Add(actualAddress);

            //populate BankInfo value
            actualCustomerInfo.BankInfo = new List<BankInfo>();
            var actualBankInfo = CreateDefaultObject.Create<BankInfo>();
            actualBankInfo.EndDate = null;
            actualCustomerInfo.BankInfo.Add(actualBankInfo);

            #endregion populate actualObj

            #region populate expectedDto

            //Expected Dto
            var expectedCustomerDto = CreateDefaultObject.Create<CustomerDTO>();
            var expectedCustomerData = CreateDefaultObject.Create<CustomerDataDTO>();
            var expectedAddressDto = CreateDefaultObject.Create<AddressDTO>();
            expectedCustomerData.DocumentType = DocumentTypes.Passport;
            expectedCustomerData.DocumentNumber = "DocumentNumber1";

            expectedAddressDto.City = "Bandung";
            expectedAddressDto.HouseExtension = "HouseExtension1";
            var expectedBankInformationDto = CreateDefaultObject.Create<BankInformationDTO>();
            expectedCustomerData.CustomerAddress = expectedAddressDto;
            expectedCustomerData.BankInformation = expectedBankInformationDto;
            expectedCustomerData.FiscalAddress = expectedAddressDto;
            expectedCustomerData.DeliveryAddress = expectedAddressDto;
            expectedBankInformationDto.EndDate = null;
            expectedBankInformationDto.City = "Bandung";
            expectedCustomerDto.ChildCustomers = new List<int>();
            expectedCustomerDto.CustomerData = expectedCustomerData;

            #endregion populate expectedDto

            #region mock MS Create Address

            var newCustInfo = expectedCustomerDto.ToCore();

            var actualCreateAddressesResponse = new CreateAddressesResponse();
            var actualCreateAddressInfo = CreateDefaultObject.Create<AddressInfo>();
            actualCreateAddressInfo.City = "Bandung";
            actualCreateAddressInfo.HouseExtention = "HouseExtension1";
            var actualAddressInfos = new List<AddressInfo> { actualCreateAddressInfo };

            var actualCustomerCreateAddress = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerCreateAddress.Addresses = new List<CustomerAddress>();
            var actualCreateAddress = CreateDefaultObject.Create<CustomerAddress>();
            actualCreateAddress.Address = actualCreateAddressInfo;
            actualCustomerCreateAddress.Addresses.Add(actualCreateAddress);
            actualCreateAddress.Customer = newCustInfo;
            actualCreateAddressesResponse.AddressInfos = actualAddressInfos;

            //define MS Create Address
            var actualCreateAddressesRequest = Arg.Is<CreateAddressesRequest>(ad => ad.CustomerAddresses.FirstOrDefault().Address.City == "Bandung");

            mockMicroServiceCraeteAddress.Process(actualCreateAddressesRequest, null).Returns(actualCreateAddressesResponse);

            #endregion mock MS Create Address

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //define MS UpdateCustomerInfo
            var actualUpdateCustomerInfoResponse = CreateDefaultObject.Create<UpdateCustomerInfoResponse>();
            var actualUpdateCustomerInfo =
                Arg.Is<UpdateCustomerInfoRequest>(cust => cust.CustomerInfo.CustomerID == 1);
            mockMicroServiceUpdateCustomer.Process(actualUpdateCustomerInfo, null)
                .Returns(x => { throw new Exception("Error"); });

            var requestDto = new UpdateCustomerDataDTORequest()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                Customer = expectedCustomerDto
            };

            MockAbstractSinglePhaseOrderProcessor(requestDto);

            #region Remock Repository

            //Remock Repo CustomerInfo
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            //CustomerInfo customerInfo = actualCustomerInfo;
            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(actualCustomerInfo);

            #endregion Remock Repository

            var response = CallBizOp(requestDto);
            Assert.IsTrue(response.resultType == ResultTypes.UnknownError);
        }

        [Test()]
        public void UpdateCustomerDataBizOp_GivenCustomer_ReturnOKNullCustomer()
        {
            #region populate actualObj

            //populate actual customer Info
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.PropertyInfo[0].IDType = (Int32)DocumentTypes.Passport;
            actualCustomerInfo.PropertyInfo[0].IDNumber = "DocumentNumber1";

            //populate Address Value
            actualCustomerInfo.Addresses = new List<CustomerAddress>();
            var actualAddress = CreateDefaultObject.Create<CustomerAddress>();
            var actualAddressInfo = CreateDefaultObject.Create<AddressInfo>();
            actualAddressInfo.HouseExtention = "HouseExtension1";
            actualAddressInfo.City = "Bandung";
            actualAddress.Address = actualAddressInfo;
            actualCustomerInfo.Addresses.Add(actualAddress);

            //populate BankInfo value
            actualCustomerInfo.BankInfo = new List<BankInfo>();
            var actualBankInfo = CreateDefaultObject.Create<BankInfo>();
            actualBankInfo.EndDate = null;
            actualCustomerInfo.BankInfo.Add(actualBankInfo);

            //populate ResourceMB

            actualCustomerInfo.ResourceMBInfo.Clear();
            var mockedResourceMb = CreateDefaultObject.Create<ResourceMBInfo>();
            mockedResourceMb.CustomerInfo = actualCustomerInfo;
            actualCustomerInfo.ResourceMBInfo.Add(mockedResourceMb);

            #endregion populate actualObj

            #region populate expectedDto

            //Expected Dto
            var expectedCustomerDto = CreateDefaultObject.Create<CustomerDTO>();
            var expectedCustomerData = CreateDefaultObject.Create<CustomerDataDTO>();
            var expectedAddressDto = CreateDefaultObject.Create<AddressDTO>();
            expectedCustomerData.DocumentType = DocumentTypes.Passport;
            expectedCustomerData.DocumentNumber = "DocumentNumber1";

            expectedAddressDto.City = "Bandung";
            expectedAddressDto.HouseExtension = "HouseExtension1";

            var expectedBankInformationDto = CreateDefaultObject.Create<BankInformationDTO>();
            expectedCustomerData.CustomerAddress = expectedAddressDto;
            expectedCustomerData.BankInformation = expectedBankInformationDto;
            expectedCustomerData.FiscalAddress = expectedAddressDto;
            expectedCustomerData.DeliveryAddress = expectedAddressDto;
            expectedBankInformationDto.EndDate = null;
            expectedBankInformationDto.City = "Bandung";
            expectedCustomerDto.ChildCustomers = new List<int>();
            expectedCustomerDto.CustomerData = expectedCustomerData;

            #endregion populate expectedDto

            #region mock MS Create Address

            var newCustInfo = expectedCustomerDto.ToCore();

            var actualCreateAddressesResponse = new CreateAddressesResponse();
            var actualCreateAddressInfo = CreateDefaultObject.Create<AddressInfo>();
            actualCreateAddressInfo.City = "Bandung";
            actualCreateAddressInfo.HouseExtention = "HouseExtension1";
            var actualAddressInfos = new List<AddressInfo> { actualCreateAddressInfo };

            var actualCustomerCreateAddress = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerCreateAddress.Addresses = new List<CustomerAddress>();
            var actualCreateAddress = CreateDefaultObject.Create<CustomerAddress>();
            actualCreateAddress.Address = actualCreateAddressInfo;
            actualCustomerCreateAddress.Addresses.Add(actualCreateAddress);
            actualCreateAddress.Customer = newCustInfo;
            actualCreateAddressesResponse.AddressInfos = actualAddressInfos;

            //define MS Create Address
            var actualCreateAddressesRequest = Arg.Is<CreateAddressesRequest>(ad => ad.CustomerAddresses.FirstOrDefault().Address.City == "Bandung");

            mockMicroServiceCraeteAddress.Process(actualCreateAddressesRequest, null).Returns(actualCreateAddressesResponse);

            var actualUpdateCustomerInfoResponse = CreateDefaultObject.Create<UpdateCustomerInfoResponse>();
            actualUpdateCustomerInfoResponse.CustomerInfo = null;

            #endregion mock MS Create Address

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //define MS UpdateCustomer
            var actualUpdateCustomerInfo =
                Arg.Is<UpdateCustomerInfoRequest>(cust => cust.CustomerInfo.CustomerID == 1);
            mockMicroServiceUpdateCustomer.Process(actualUpdateCustomerInfo, null)
                .Returns(actualUpdateCustomerInfoResponse);

            var requestDto = new UpdateCustomerDataDTORequest()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                Customer = expectedCustomerDto
            };

            MockAbstractSinglePhaseOrderProcessor(requestDto);

            #region Remock Repository

            //Remock Repo CustomerInfo
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            //CustomerInfo customerInfo = actualCustomerInfo;
            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(actualCustomerInfo);

            #endregion Remock Repository

            var response = CallBizOp(requestDto);
            Assert.IsNull(response.Customer);
            Assert.IsNull(response.Subscription);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
        }


         [Test()]
        public void UpdateCustomerDataBizOp_GivenCustomerIsExpired_ReturnOKCustomer()
        {
            #region populate actualObj

            //populate actual customer Info
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();

            //populate Address Value
            actualCustomerInfo.Addresses = new List<CustomerAddress>();
            var actualAddress = CreateDefaultObject.Create<CustomerAddress>();
            var actualAddressInfo = CreateDefaultObject.Create<AddressInfo>();
            actualAddressInfo.HouseExtention = "HouseExtension1";
            actualAddressInfo.City = "Bandung";
            actualAddress.Address = actualAddressInfo;
       
            actualCustomerInfo.Addresses.Add(actualAddress);

            //populate BankInfo value
            actualCustomerInfo.BankInfo = new List<BankInfo>();
            var actualBankInfo = CreateDefaultObject.Create<BankInfo>();
            actualBankInfo.EndDate = null;
            actualCustomerInfo.BankInfo.Add(actualBankInfo);

            actualCustomerInfo.StatusID = 20;

            #endregion populate actualObj

            #region populate expectedDto

            //Expected Dto
            var expectedCustomerDto = CreateDefaultObject.Create<CustomerDTO>();
            var expectedCustomerData = CreateDefaultObject.Create<CustomerDataDTO>();
            var expectedAddressDto = CreateDefaultObject.Create<AddressDTO>();
            expectedAddressDto.City = "Bandung";

            expectedAddressDto.HouseExtension = "HouseExtension1";
            var expectedBankInformationDto = CreateDefaultObject.Create<BankInformationDTO>();
            expectedCustomerData.CustomerAddress = expectedAddressDto;
            expectedCustomerData.BankInformation = expectedBankInformationDto;
            expectedCustomerData.FiscalAddress = expectedAddressDto;
            expectedCustomerData.DeliveryAddress = expectedAddressDto;
            expectedCustomerData.PendingStatus = PendingStatus.Terminated;
            expectedBankInformationDto.EndDate = null;
            expectedBankInformationDto.City = "Bandung";
            expectedCustomerDto.ChildCustomers = new List<int>();
            expectedCustomerDto.CustomerData = expectedCustomerData;

            #endregion populate expectedDto

            #region mock MS Create Address

            var newCustInfo = expectedCustomerDto.ToCore();

            var actualCreateAddressesResponse = new CreateAddressesResponse();
            var actualCreateAddressInfo = CreateDefaultObject.Create<AddressInfo>();
            actualCreateAddressInfo.City = "Bandung";
            actualCreateAddressInfo.HouseExtention = "HouseExtension1";
            var actualAddressInfos = new List<AddressInfo> { actualCreateAddressInfo };

            var actualCustomerCreateAddress = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerCreateAddress.Addresses = new List<CustomerAddress>();
            var actualCreateAddress = CreateDefaultObject.Create<CustomerAddress>();
            actualCreateAddress.Address = actualCreateAddressInfo;
            actualCustomerCreateAddress.Addresses.Add(actualCreateAddress);
            actualCreateAddress.Customer = newCustInfo;
            actualCreateAddressesResponse.AddressInfos = actualAddressInfos;

            //define MS Create Address
            var actualCreateAddressesRequest = Arg.Is<CreateAddressesRequest>(ad => ad.CustomerAddresses.FirstOrDefault().Address.City == "Bandung");

            mockMicroServiceCraeteAddress.Process(actualCreateAddressesRequest, null).Returns(actualCreateAddressesResponse);

            var actualUpdateCustomerInfoResponse = CreateDefaultObject.Create<UpdateCustomerInfoResponse>();
            actualUpdateCustomerInfoResponse.CustomerInfo = actualCustomerInfo;

            #endregion mock MS Create Address

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //define MS UpdateCustomerInfo
            var actualUpdateCustomerInfo =
                Arg.Is<UpdateCustomerInfoRequest>(cust => cust.CustomerInfo.CustomerID == 1);
            mockMicroServiceUpdateCustomer.Process(actualUpdateCustomerInfo, null)
                 .Returns(actualUpdateCustomerInfoResponse);

            var requestDto = new UpdateCustomerDataDTORequest()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                Customer = expectedCustomerDto
            };

            MockAbstractSinglePhaseOrderProcessor(requestDto);

            #region Remock Repository

            //Remock Repo CustomerInfo
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            //CustomerInfo customerInfo = actualCustomerInfo;
            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(actualCustomerInfo);

            #endregion Remock Repository

            var response = CallBizOp(requestDto);
            //Assert.AreEqual(response.Customer.CustomerData.FiscalAddress.City, expectedCustomerDto.CustomerData.FiscalAddress.City);
            Assert.IsTrue(response.resultType == ResultTypes.DataValidationError);
        }
         [Test()]
         public void UpdateCustomerDataBizOp_NOK_authorizationfailed()
         {
             #region Mock CheckAuthorization
             var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

             var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
             mockMicroServiceCheckAuthorization.Process(
                 actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
             #endregion

             var requestDto = new UpdateCustomerDataDTORequest()
             {
                 user = "1644000204",
                 password = "123456",
                 vmno = "970100",
                 CustomerId = 1000000000
             };

             MockAbsctractBusinessOperation(requestDto);

             var res = CallBizOp(requestDto);
             Assert.AreEqual(res.resultType, ResultTypes.AuthorizationError);
             Assert.AreEqual(res.errorCode, BizOpsErrors.AuthorizeErrorUser);
         }

         [Test()]
         public void UpdateCustomerDataBizOp_GivenCustomerWithChangedDocumentTypeAndNumber_ReturnNOKNullCustomer()
         {
             #region populate actualObj

             //populate actual customer Info
             var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
             
             //Changed Document Type and Number for the customer
             actualCustomerInfo.PropertyInfo[0].IDType = (Int32)DocumentTypes.Passport;
             actualCustomerInfo.PropertyInfo[0].IDNumber = "DocumentNumber1";

             //populate Address Value
             actualCustomerInfo.Addresses = new List<CustomerAddress>();
             var actualAddress = CreateDefaultObject.Create<CustomerAddress>();
             var actualAddressInfo = CreateDefaultObject.Create<AddressInfo>();
             actualAddressInfo.HouseExtention = "HouseExtension1";
             actualAddressInfo.City = "Bandung";
             actualAddress.Address = actualAddressInfo;
             actualCustomerInfo.Addresses.Add(actualAddress);

             //populate BankInfo value
             actualCustomerInfo.BankInfo = new List<BankInfo>();
             var actualBankInfo = CreateDefaultObject.Create<BankInfo>();
             actualBankInfo.EndDate = null;
             actualCustomerInfo.BankInfo.Add(actualBankInfo);

             //populate ResourceMB

             actualCustomerInfo.ResourceMBInfo.Clear();
             var mockedResourceMb = CreateDefaultObject.Create<ResourceMBInfo>();
             mockedResourceMb.CustomerInfo = actualCustomerInfo;
             actualCustomerInfo.ResourceMBInfo.Add(mockedResourceMb);

             #endregion populate actualObj

             #region populate expectedDto

             //Expected Dto
             var expectedCustomerDto = CreateDefaultObject.Create<CustomerDTO>();
             var expectedCustomerData = CreateDefaultObject.Create<CustomerDataDTO>();
             var expectedAddressDto = CreateDefaultObject.Create<AddressDTO>();
             expectedCustomerData.DocumentType = DocumentTypes.NIF;
             expectedCustomerData.DocumentNumber = "DocumentNumber2";

             expectedAddressDto.City = "Bandung";
             expectedAddressDto.HouseExtension = "HouseExtension1";

             var expectedBankInformationDto = CreateDefaultObject.Create<BankInformationDTO>();
             expectedCustomerData.CustomerAddress = expectedAddressDto;
             expectedCustomerData.BankInformation = expectedBankInformationDto;
             expectedCustomerData.FiscalAddress = expectedAddressDto;
             expectedCustomerData.DeliveryAddress = expectedAddressDto;
             expectedBankInformationDto.EndDate = null;
             expectedBankInformationDto.City = "Bandung";
             expectedCustomerDto.ChildCustomers = new List<int>();
             expectedCustomerDto.CustomerData = expectedCustomerData;

             #endregion populate expectedDto

             #region mock MS Create Address

             var newCustInfo = expectedCustomerDto.ToCore();

             var actualCreateAddressesResponse = new CreateAddressesResponse();
             var actualCreateAddressInfo = CreateDefaultObject.Create<AddressInfo>();
             actualCreateAddressInfo.City = "Bandung";
             actualCreateAddressInfo.HouseExtention = "HouseExtension1";
             var actualAddressInfos = new List<AddressInfo> { actualCreateAddressInfo };

             var actualCustomerCreateAddress = CreateDefaultObject.Create<CustomerInfo>();
             actualCustomerCreateAddress.Addresses = new List<CustomerAddress>();
             var actualCreateAddress = CreateDefaultObject.Create<CustomerAddress>();
             actualCreateAddress.Address = actualCreateAddressInfo;
             actualCustomerCreateAddress.Addresses.Add(actualCreateAddress);
             actualCreateAddress.Customer = newCustInfo;
             actualCreateAddressesResponse.AddressInfos = actualAddressInfos;

             //define MS Create Address
             var actualCreateAddressesRequest = Arg.Is<CreateAddressesRequest>(ad => ad.CustomerAddresses.FirstOrDefault().Address.City == "Bandung");

             mockMicroServiceCraeteAddress.Process(actualCreateAddressesRequest, null).Returns(actualCreateAddressesResponse);

             var actualUpdateCustomerInfoResponse = CreateDefaultObject.Create<UpdateCustomerInfoResponse>();
             actualUpdateCustomerInfoResponse.CustomerInfo = actualCustomerInfo;

             #endregion mock MS Create Address

             #region Mock CheckAuthorization
             var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

             var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
             mockMicroServiceCheckAuthorization.Process(
                 actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
             #endregion

             //define MS UpdateCustomerInfo
             var actualUpdateCustomerInfo =
                 Arg.Is<UpdateCustomerInfoRequest>(cust => cust.CustomerInfo.CustomerID == 1);
             mockMicroServiceUpdateCustomer.Process(actualUpdateCustomerInfo, null)
                  .Returns(actualUpdateCustomerInfoResponse);

             var requestDto = new UpdateCustomerDataDTORequest()
             {
                 user = "1644000204",
                 password = "123456",
                 vmno = "970100",
                 Customer = expectedCustomerDto
             };

             MockAbstractSinglePhaseOrderProcessor(requestDto);

             #region Remock Repository

             //Remock Repo CustomerInfo
             var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
             //CustomerInfo customerInfo = actualCustomerInfo;
             mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(actualCustomerInfo);

             #endregion Remock Repository

             var response = CallBizOp(requestDto);

             Assert.AreEqual(response.resultType, ResultTypes.DataValidationError);
             Assert.IsNull(response.Customer);
         }
    }   
}