using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.CreateCustomer;
using com.etak.core.customer.message.CreateAddresses;
using com.etak.core.customer.message.CreateCustomerInfo;
using com.etak.core.dealer.messages.GetDealerInfoById;
using com.etak.core.GSMSubscription.message.GetCustomerDataRoamingLimitNotificationByCustomerId;
using com.etak.core.GSMSubscription.messages.CreateCustomerDastaRoamingLimitNotification;
using com.etak.core.GSMSubscription.messages.GetCustomerDataRoamingLimitsByCustomerID;
using com.etak.core.microservices.messages.GetLanguageTypeByCode;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository.crm.configuration;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using Newtonsoft.Json;
using NHibernate.Util;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.CreateCustomer
{
    [TestFixture]
    public class CreateCustomerBizOpUnitTests : AbstractSinglePhaseOrderProcessorTest<CreateCustomerBizOp, CreateCustomerRequestDTO, CreateCustomerResponseDTO, CreateCustomerRequestInternal, CreateCustomerResponseInternal, CreateCustomerOrder>
    {
        private IMicroService<CreateCustomerInfoRequest, CreateCustomerInfoResponse> mockMicroServiceCreateCustomer;

        private IOperationConfigurationRepository<OperationConfiguration> mockConfigurationRepository;

        private IMicroService<GetLanguageTypeInfoByCodeRequest, GetLanguageTypeInfoByCodeResponse>
            mockMicroServiceGetLanguageType;

        private IMicroService<CreateAddressesRequest, CreateAddressesResponse> mockMicroServiceCreateAddress;

        private IMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse> mockMicroServiceGetDealerInfo;

        private IMicroService<GetCustomerDataRoamingLimitNotificationByCustomerIdRequest, GetCustomerDataRoamingLimitNotificationByCustomerIdResponse> mockMicroServiceGetCustomerDataRoamingLimitNotification;

        private IMicroService<GetCustomerDataRoamingLimitsByCustomerIDRequest, GetCustomerDataRoamingLimitsByCustomerIDResponse> mockGetCustomerDataRoamingLimitsByCustomerID;

        private IMicroService <CreateCustomerDastaRoamingLimitNotificationRequest, CreateCustomerDastaRoamingLimitNotificationResponse> mockCreateCusotmerDataRoamingLimitNotifications;

        private int expectedcustomerid = 99;
            
        [TestFixtureSetUp()]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCreateCustomer =
                MockMicroServiceManager.GetMockedMicroService<CreateCustomerInfoRequest, CreateCustomerInfoResponse>();
            mockConfigurationRepository = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            mockMicroServiceGetLanguageType =
                MockMicroServiceManager
                    .GetMockedMicroService<GetLanguageTypeInfoByCodeRequest, GetLanguageTypeInfoByCodeResponse>();
            mockMicroServiceCreateAddress =
                MockMicroServiceManager.GetMockedMicroService<CreateAddressesRequest, CreateAddressesResponse>();
            mockMicroServiceGetDealerInfo =
                MockMicroServiceManager.GetMockedMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();

            mockMicroServiceGetCustomerDataRoamingLimitNotification = MockMicroServiceManager.GetMockedMicroService<GetCustomerDataRoamingLimitNotificationByCustomerIdRequest, GetCustomerDataRoamingLimitNotificationByCustomerIdResponse>();

            mockGetCustomerDataRoamingLimitsByCustomerID = MockMicroService<GetCustomerDataRoamingLimitsByCustomerIDRequest, GetCustomerDataRoamingLimitsByCustomerIDResponse>();

            mockCreateCusotmerDataRoamingLimitNotifications = MockMicroService<CreateCustomerDastaRoamingLimitNotificationRequest,CreateCustomerDastaRoamingLimitNotificationResponse>();

        }

        [Test]
        public void CreateCustomerBizOp_GivenCustomer_ReturnOkCustomer()
        {
            #region populate data for mock getLanguageType

            var actualLanguageTypeRequest = Arg.Is<GetLanguageTypeInfoByCodeRequest>(x => x.LanguadeId == 1);
            var actualGetLanguageTypeInfoByCodeResponse = new GetLanguageTypeInfoByCodeResponse();
            var actualLanguageTypeInfo = CreateDefaultObject.Create<LanguageTypeInfo>();
            actualGetLanguageTypeInfoByCodeResponse.LanguageTypeInfos = new List<LanguageTypeInfo>() { actualLanguageTypeInfo };

            mockMicroServiceGetLanguageType.Process(actualLanguageTypeRequest, null)
                .Returns(actualGetLanguageTypeInfoByCodeResponse);

            #endregion populate data for mock getLanguageType

            #region populate data to mock getDealer

            var getDealerRequest = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == 1);
            var getDealerResponse = CreateDefaultObject.Create<GetDealerInfoByIdResponse>();
            getDealerResponse.DealerInfo = CreateDefaultObject.Create<DealerInfo>();

            mockMicroServiceGetDealerInfo.Process(getDealerRequest, null).Returns(getDealerResponse);

            #endregion populate data to mock getDealer

            #region populate data for configuration

            var actualOperationConfigurations = new List<OperationConfiguration>();
            var actualOperationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var actualCreateCustomerConfig = new CreateCustomerConfiguration();
            actualCreateCustomerConfig.InvoiceDetails = false;
            actualCreateCustomerConfig.InvoiceDueDate = 1;
            actualCreateCustomerConfig.LanguageID = 1;
            actualCreateCustomerConfig.MappingInfoOldId = 0;
            actualCreateCustomerConfig.MappingInfoOrgId = 0;
            actualCreateCustomerConfig.MappingInfoStatus = false;
            actualCreateCustomerConfig.PaymentType = (int)PaymentType.Postpayment;
            actualCreateCustomerConfig.PendingStatus = (int)PendingStatus.PreActive;
            actualCreateCustomerConfig.BusinessType = (int)CustomerBusinessType.Private;
            actualOperationConfiguration.JSonConfig = JsonConvert.SerializeObject(actualCreateCustomerConfig);
            actualOperationConfiguration.EndDate = new DateTime(2016, 5, 5);
            actualOperationConfiguration.StarTime = DateTime.Now;
            actualOperationConfigurations.Add(actualOperationConfiguration);
            mockConfigurationRepository.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(actualOperationConfigurations);

            #endregion populate data for configuration

            #region populate actual object

            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.CustomerID = expectedcustomerid;
            actualCustomerInfo.ResourceMBInfo.First().CustomerInfo = actualCustomerInfo;
            actualCustomerInfo.Addresses = new List<CustomerAddress>();
            var actualCustAddress = CreateDefaultObject.Create<CustomerAddress>();
            actualCustAddress.Address = CreateDefaultObject.Create<AddressInfo>();
            actualCustAddress.Customer = actualCustomerInfo;
            actualCustomerInfo.Addresses.Add(actualCustAddress);
            actualCustomerInfo.BankInfo = new List<BankInfo>();
            var actualBankInfo = CreateDefaultObject.Create<BankInfo>();
            actualCustomerInfo.BankInfo.Add(actualBankInfo);

            var createCustomerInfoResponse = new CreateCustomerInfoResponse();
            createCustomerInfoResponse.CustomerInfo = actualCustomerInfo;

            #endregion populate actual object

            #region populate requestDto

            var actualRequestDto = CreateDefaultObject.Create<CustomerDTO>();
            actualRequestDto.CustomerId = expectedcustomerid;
            actualRequestDto.CustomerData = CreateDefaultObject.Create<CustomerDataDTO>();
            actualRequestDto.CustomerData.BankInformation = CreateDefaultObject.Create<BankInformationDTO>();
            actualRequestDto.CustomerData.BankInformation.EndDate = null;
            actualRequestDto.CustomerData.CustomerAddress = CreateDefaultObject.Create<AddressDTO>();
            //actualRequestDto.CustomerData.DeliveryAddress = CreateDefaultObject.Create<AddressDTO>();
            actualRequestDto.ChildCustomers = new List<int>();

            #endregion populate requestDto

            #region mock create address microService

            var createAddressRequest = Arg.Any<CreateAddressesRequest>();
            var actualCreateAddressResponse = new CreateAddressesResponse();
            actualCreateAddressResponse.AddressInfos = new List<AddressInfo>() { actualCustAddress.Address };
            mockMicroServiceCreateAddress.Process(Arg.Any<CreateAddressesRequest>(), null).Returns(actualCreateAddressResponse);

            #endregion mock create address microService

            #region mock create customer MS

            var createCustomerRequest =
            Arg.Is<CreateCustomerInfoRequest>(cust => cust.CustomerInfo.CustomerID == expectedcustomerid);
            mockMicroServiceCreateCustomer.Process(createCustomerRequest, null).Returns(createCustomerInfoResponse);

            #endregion mock create customer MS

            //#region mock Subscription MS

            //var createCustomerRequest =
            //Arg.Is<CreateCustomerInfoRequest>(cust => cust.CustomerInfo.CustomerID == expectedcustomerid);
            //mockMicroServiceCreateCustomer.Process(createCustomerRequest, null).Returns(createCustomerInfoResponse);

            //#endregion mock create customer MS

            #region mockMicroServiceGetCustomerDataRoamingLimitNotification

            var getDataRoamingReq = Arg.Is<GetCustomerDataRoamingLimitNotificationByCustomerIdRequest>(x => x.CustomerId == actualRequestDto.CustomerId);
            var getDataRoamingResp = new GetCustomerDataRoamingLimitNotificationByCustomerIdResponse()
            {
                CustomerDataRoamingLimitNotifications = new List<CustomerDataRoamingLimitNotification>()
                {
                    CreateDefaultObject.Create<CustomerDataRoamingLimitNotification>()
                }
            };
            mockMicroServiceGetCustomerDataRoamingLimitNotification.Process(getDataRoamingReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getDataRoamingResp);
            #endregion

            #region mock GetCustomerDataRoamingLimitsByCustomerID
            var getDataRoamingLimitReq = Arg.Is<GetCustomerDataRoamingLimitsByCustomerIDRequest>(x => x.CustomerID == actualRequestDto.CustomerId);
            var getDataRoamingLimitResp = new GetCustomerDataRoamingLimitsByCustomerIDResponse()
            {
                CustomerDataRoamingLimits = new List<CustomerDataRoamingLimit>() { CreateDefaultObject.Create<CustomerDataRoamingLimit>() }
            };
            mockGetCustomerDataRoamingLimitsByCustomerID.Process(getDataRoamingLimitReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getDataRoamingLimitResp);
            #endregion

            #region mockBizOp

            var actualCreateCustomerRequestDTO = new CreateCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerToBeCreatedDto = actualRequestDto,
                LanguageId = 1
                //PendingStatus =  (int)PendingStatus.PreActive
            };
            MockAbstractSinglePhaseOrderProcessor(actualCreateCustomerRequestDTO);
            var actualCreateCustomerResponseDTO = CallBizOp(actualCreateCustomerRequestDTO);

            var expectedCreateCustomerResponseDTO = new CreateCustomerResponseDTO
            {
                Customer = new CustomerDTO { CustomerId = expectedcustomerid },
                Subscription = new SubscriptionDTO { CustomerId = expectedcustomerid}
            };

            Assert.IsTrue(actualCreateCustomerResponseDTO.resultType == ResultTypes.Ok);
            Assert.IsTrue(actualCreateCustomerResponseDTO.Customer.CustomerId == expectedCreateCustomerResponseDTO.Customer.CustomerId);
            Assert.IsTrue(actualCreateCustomerResponseDTO.Subscription.CustomerId == expectedCreateCustomerResponseDTO.Subscription.CustomerId);

            #endregion mockBizOp
        }

        [Test]
        public void CreateCustomerBizOp_GivenCustomer_ReturnExceptionNullCustomer()
        {
            #region populate data for mock getLanguageType

            var actualLanguageTypeRequest = Arg.Is<GetLanguageTypeInfoByCodeRequest>(x => x.LanguadeId == 1);
            var actualGetLanguageTypeInfoByCodeResponse = new GetLanguageTypeInfoByCodeResponse();
            var actualLanguageTypeInfo = CreateDefaultObject.Create<LanguageTypeInfo>();
            actualGetLanguageTypeInfoByCodeResponse.LanguageTypeInfos = new List<LanguageTypeInfo>() { actualLanguageTypeInfo };

            mockMicroServiceGetLanguageType.Process(actualLanguageTypeRequest, null)
                .Returns(actualGetLanguageTypeInfoByCodeResponse);

            #endregion populate data for mock getLanguageType

            #region populate data to mock getDealer

            var getDealerRequest = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == 1);
            var getDealerResponse = CreateDefaultObject.Create<GetDealerInfoByIdResponse>();
            getDealerResponse.DealerInfo = CreateDefaultObject.Create<DealerInfo>();

            mockMicroServiceGetDealerInfo.Process(getDealerRequest, null).Returns(getDealerResponse);

            #endregion populate data to mock getDealer

            #region populate data for configuration

            var actualOperationConfigurations = new List<OperationConfiguration>();
            var actualOperationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var actualCreateCustomerConfig = new CreateCustomerConfiguration();
            actualCreateCustomerConfig.InvoiceDetails = false;
            actualCreateCustomerConfig.InvoiceDueDate = 1;
            actualCreateCustomerConfig.LanguageID = 1;
            actualCreateCustomerConfig.MappingInfoOldId = 0;
            actualCreateCustomerConfig.MappingInfoOrgId = 0;
            actualCreateCustomerConfig.MappingInfoStatus = false;
            actualCreateCustomerConfig.PaymentType = (int)PaymentType.Postpayment;
            actualCreateCustomerConfig.PendingStatus = (int)PendingStatus.PreActive;
            actualCreateCustomerConfig.BusinessType = (int)CustomerBusinessType.Private;
            actualOperationConfiguration.JSonConfig = JsonConvert.SerializeObject(actualCreateCustomerConfig);
            actualOperationConfiguration.EndDate = new DateTime(2016, 5, 5);
            actualOperationConfiguration.StarTime = DateTime.Now;
            actualOperationConfigurations.Add(actualOperationConfiguration);
            mockConfigurationRepository.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(actualOperationConfigurations);

            #endregion populate data for configuration

            #region populate actual object

            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.Addresses = new List<CustomerAddress>();
            var actualCustAddress = CreateDefaultObject.Create<CustomerAddress>();
            actualCustAddress.Address = CreateDefaultObject.Create<AddressInfo>();
            actualCustAddress.Customer = actualCustomerInfo;
            actualCustomerInfo.Addresses.Add(actualCustAddress);
            actualCustomerInfo.BankInfo = new List<BankInfo>();
            var actualBankInfo = CreateDefaultObject.Create<BankInfo>();
            actualCustomerInfo.BankInfo.Add(actualBankInfo);

            var createCustomerInfoResponse = new CreateCustomerInfoResponse();

            #endregion populate actual object

            #region populate requestDto

            var actualRequestDto = CreateDefaultObject.Create<CustomerDTO>();
            actualRequestDto.CustomerData = CreateDefaultObject.Create<CustomerDataDTO>();
            actualRequestDto.CustomerData.BankInformation = CreateDefaultObject.Create<BankInformationDTO>();
            actualRequestDto.CustomerData.BankInformation.EndDate = null;
            actualRequestDto.CustomerData.CustomerAddress = CreateDefaultObject.Create<AddressDTO>();

            actualRequestDto.ChildCustomers = new List<int>();

            #endregion populate requestDto

            #region mock create address microService

            var createAddressRequest = Arg.Any<CreateAddressesRequest>();
            var actualCreateAddressResponse = new CreateAddressesResponse();
            actualCreateAddressResponse.AddressInfos = new List<AddressInfo>() { actualCustAddress.Address };
            mockMicroServiceCreateAddress.Process(Arg.Any<CreateAddressesRequest>(), null).Returns(actualCreateAddressResponse);

            #endregion mock create address microService

            #region mock create customer MS

            var createCustomerRequest =
            Arg.Is<CreateCustomerInfoRequest>(cust => cust.CustomerInfo.CustomerID == 1);
            mockMicroServiceCreateCustomer.Process(createCustomerRequest, null).Returns(createCustomerInfoResponse);

            #endregion mock create customer MS

            #region mockMicroServiceGetCustomerDataRoamingLimitNotification

            var getDataRoamingReq = Arg.Is<GetCustomerDataRoamingLimitNotificationByCustomerIdRequest>(x => x.CustomerId == actualRequestDto.CustomerId);
            var getDataRoamingResp = new GetCustomerDataRoamingLimitNotificationByCustomerIdResponse()
            {
                CustomerDataRoamingLimitNotifications = new List<CustomerDataRoamingLimitNotification>()
                {
                    CreateDefaultObject.Create<CustomerDataRoamingLimitNotification>()
                }
            };
            mockMicroServiceGetCustomerDataRoamingLimitNotification.Process(getDataRoamingReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getDataRoamingResp);
            #endregion

            #region mock GetCustomerDataRoamingLimitsByCustomerID
            var getDataRoamingLimitReq = Arg.Is<GetCustomerDataRoamingLimitsByCustomerIDRequest>(x => x.CustomerID == actualRequestDto.CustomerId);
            var getDataRoamingLimitResp = new GetCustomerDataRoamingLimitsByCustomerIDResponse()
            {
                CustomerDataRoamingLimits = new List<CustomerDataRoamingLimit>() { CreateDefaultObject.Create<CustomerDataRoamingLimit>()}
            };
            mockGetCustomerDataRoamingLimitsByCustomerID.Process(getDataRoamingLimitReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getDataRoamingLimitResp);
            #endregion

            #region mockBizOp

            var requestDto = new CreateCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerToBeCreatedDto = actualRequestDto,
                LanguageId = 1
            };
            MockAbstractSinglePhaseOrderProcessor(requestDto);
            var response = CallBizOp(requestDto);

            var res = response.resultType;
            Assert.IsNull(response.Customer);
            Assert.IsTrue(response.resultType != ResultTypes.Ok);

            #endregion mockBizOp
        }

        [Test]
        public void CreateCustomerBizOp_GivenCustomer_ReturnNOkCustomer()
        {
            #region populate data for mock getLanguageType

            var actualLanguageTypeRequest = Arg.Is<GetLanguageTypeInfoByCodeRequest>(x => x.LanguadeId == 1);
            var actualGetLanguageTypeInfoByCodeResponse = new GetLanguageTypeInfoByCodeResponse();
            var actualLanguageTypeInfo = CreateDefaultObject.Create<LanguageTypeInfo>();
            actualGetLanguageTypeInfoByCodeResponse.LanguageTypeInfos = new List<LanguageTypeInfo>() { actualLanguageTypeInfo };

            mockMicroServiceGetLanguageType.Process(actualLanguageTypeRequest, null)
                .Returns(actualGetLanguageTypeInfoByCodeResponse);

            #endregion populate data for mock getLanguageType

            #region populate data to mock getDealer

            var getDealerRequest = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == 1);
            var getDealerResponse = CreateDefaultObject.Create<GetDealerInfoByIdResponse>();
            getDealerResponse.DealerInfo = CreateDefaultObject.Create<DealerInfo>();

            mockMicroServiceGetDealerInfo.Process(getDealerRequest, null).Returns(getDealerResponse);

            #endregion populate data to mock getDealer

            #region populate data for configuration

            var actualOperationConfigurations = new List<OperationConfiguration>();
            var actualOperationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var actualCreateCustomerConfig = new CreateCustomerConfiguration();
            actualCreateCustomerConfig.InvoiceDetails = false;
            actualCreateCustomerConfig.InvoiceDueDate = 1;
            actualCreateCustomerConfig.LanguageID = 1;
            actualCreateCustomerConfig.MappingInfoOldId = 0;
            actualCreateCustomerConfig.MappingInfoOrgId = 0;
            actualCreateCustomerConfig.MappingInfoStatus = false;
            actualCreateCustomerConfig.PaymentType = (int)PaymentType.Postpayment;
            actualCreateCustomerConfig.PendingStatus = (int)PendingStatus.PreActive;
            actualCreateCustomerConfig.BusinessType = (int)CustomerBusinessType.Private;
            actualOperationConfiguration.JSonConfig = JsonConvert.SerializeObject(actualCreateCustomerConfig);
            actualOperationConfiguration.EndDate = new DateTime(2016, 5, 5);
            actualOperationConfiguration.StarTime = DateTime.Now;
            actualOperationConfigurations.Add(actualOperationConfiguration);
            mockConfigurationRepository.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(actualOperationConfigurations);

            #endregion populate data for configuration

            #region populate actual object

            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.Addresses = new List<CustomerAddress>();
            var actualCustAddress = CreateDefaultObject.Create<CustomerAddress>();
            actualCustAddress.Address = CreateDefaultObject.Create<AddressInfo>();
            actualCustAddress.Customer = actualCustomerInfo;
            actualCustomerInfo.Addresses.Add(actualCustAddress);
            actualCustomerInfo.BankInfo = new List<BankInfo>();
            var actualBankInfo = CreateDefaultObject.Create<BankInfo>();
            actualCustomerInfo.BankInfo.Add(actualBankInfo);

            var createCustomerInfoResponse = new CreateCustomerInfoResponse();
            createCustomerInfoResponse.CustomerInfo = actualCustomerInfo;

            #endregion populate actual object

            #region populate requestDto

            var actualRequestDto = CreateDefaultObject.Create<CustomerDTO>();
            actualRequestDto.CustomerData = CreateDefaultObject.Create<CustomerDataDTO>();
            actualRequestDto.CustomerData.BankInformation = CreateDefaultObject.Create<BankInformationDTO>();
            actualRequestDto.CustomerData.BankInformation.EndDate = null;
            actualRequestDto.CustomerData.CustomerAddress = CreateDefaultObject.Create<AddressDTO>();
            //actualRequestDto.CustomerData.DeliveryAddress = CreateDefaultObject.Create<AddressDTO>();
            actualRequestDto.ChildCustomers = new List<int>();

            #endregion populate requestDto

            #region mock create address microService

            var createAddressRequest = Arg.Any<CreateAddressesRequest>();
            var actualCreateAddressResponse = new CreateAddressesResponse();
            actualCreateAddressResponse.AddressInfos = new List<AddressInfo>() { actualCustAddress.Address };
            mockMicroServiceCreateAddress.Process(Arg.Any<CreateAddressesRequest>(), null).Returns(actualCreateAddressResponse);

            #endregion mock create address microService

            #region mock create customer MS

            var createCustomerRequest =
            Arg.Is<CreateCustomerInfoRequest>(cust => cust.CustomerInfo.CustomerID == 1);
            mockMicroServiceCreateCustomer.Process(createCustomerRequest, null).Returns(x => { throw new Exception("Errror"); });

            #endregion mock create customer MS

            #region mockMicroServiceGetCustomerDataRoamingLimitNotification

            var getDataRoamingReq = Arg.Is<GetCustomerDataRoamingLimitNotificationByCustomerIdRequest>(x => x.CustomerId == actualRequestDto.CustomerId);
            var getDataRoamingResp = new GetCustomerDataRoamingLimitNotificationByCustomerIdResponse()
            {
                CustomerDataRoamingLimitNotifications = new List<CustomerDataRoamingLimitNotification>()
                {
                    CreateDefaultObject.Create<CustomerDataRoamingLimitNotification>()
                }
            };
            mockMicroServiceGetCustomerDataRoamingLimitNotification.Process(getDataRoamingReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getDataRoamingResp);
            #endregion

            #region mock GetCustomerDataRoamingLimitsByCustomerID
            var getDataRoamingLimitReq = Arg.Is<GetCustomerDataRoamingLimitsByCustomerIDRequest>(x => x.CustomerID == actualRequestDto.CustomerId);
            var getDataRoamingLimitResp = new GetCustomerDataRoamingLimitsByCustomerIDResponse()
            {
                CustomerDataRoamingLimits = new List<CustomerDataRoamingLimit>() { CreateDefaultObject.Create<CustomerDataRoamingLimit>() }
            };
            mockGetCustomerDataRoamingLimitsByCustomerID.Process(getDataRoamingLimitReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getDataRoamingLimitResp);
            #endregion

            #region mockBizOp

            var requestDto = new CreateCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerToBeCreatedDto = actualRequestDto,
                LanguageId = 1
            };
            MockAbstractSinglePhaseOrderProcessor(requestDto);
            var response = CallBizOp(requestDto);

            var res = response.resultType;

            Assert.IsTrue(response.resultType == ResultTypes.UnknownError);

            #endregion mockBizOp
        }

    }
}