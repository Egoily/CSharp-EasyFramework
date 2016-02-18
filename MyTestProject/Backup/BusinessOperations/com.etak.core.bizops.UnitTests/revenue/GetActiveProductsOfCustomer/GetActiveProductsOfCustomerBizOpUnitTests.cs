using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.revenue.GetActiveProductsOfCustomer;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.operation;
using com.etak.core.operation.dtoConverters;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.customer;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using com.etak.core.customer.message.GetActiveProductsOfCustomer;
using NSubstitute;
using NUnit.Framework;
using Newtonsoft.Json;

namespace com.etak.core.bizops.UnitTests.revenue.GetActiveProductsOfCustomer
{
    [TestFixture]
    public class GetActiveProductsOfCustomerBizOpUnitTests :
       AbstractBusinessOperationTest<GetActiveProducsOfCustomerBizOp, GetActiveProductsOfCustomerRequestDTO, GetActiveProductsOfCustomerResponseDTO, GetActiveProductsOfCustomerRequestInternal, GetActiveProductsOfCustomerResponseInternal>
    {
        private ICustomerInfoRepository<CustomerInfo> mockCustomerRepo;
        [TestFixtureSetUp]
        public static void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [TestCase()]
        public void GetActiveProductsOfCustomerBizOp_OK()
        {
            var getActiveProductsOfCustomerMS = MockMicroService<GetActiveProductsOfCustomerRequest, GetActiveProductsOfCustomerResponse>();
            var getInvoicesByCustomerIdAndLegalInvoiceNumberMS = MockMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();

            var getInvoicesRequest = new GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest();
            var getInvoicesResponse = new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse()
            {
                Invoices = new[] {new Invoice(){StartDate = DateTime.MinValue,EndDate = DateTime.MaxValue}}
            };
            getInvoicesByCustomerIdAndLegalInvoiceNumberMS.Process(getInvoicesRequest,null).ReturnsForAnyArgs(getInvoicesResponse);

            int[] specificProducts = { 1003000000, 1003000001, 1003000002, 1003000003, 1003000004, 1003000005, 1003000006, 1003000027, 1003000028};
            int[] dataTransferPermissions = {1003000065, 1003000066};
            int[] dataTransferSent = { 1003000067 };
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var actualOperationConfigurations = new List<OperationConfiguration>();
            var actualOperationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var actualBizOpConfiguration = new ActiveProductsConfiguration();
            actualBizOpConfiguration.SpecificProductsForActiveProducts = new List<int>();
            actualBizOpConfiguration.DataTransferPermissions = new List<int>();
            foreach (var product in specificProducts)
            {
                actualBizOpConfiguration.SpecificProductsForActiveProducts.Add(product);
            }
            foreach (var product in dataTransferPermissions)
            {
                actualBizOpConfiguration.DataTransferPermissions.Add(product);
            }

            actualOperationConfiguration.JSonConfig = JsonConvert.SerializeObject(actualBizOpConfiguration);
            actualOperationConfiguration.EndDate = new DateTime(2016, 5, 5);
            actualOperationConfiguration.StarTime = DateTime.Now;
            actualOperationConfigurations.Add(actualOperationConfiguration);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(actualOperationConfigurations);
            
            var request = Arg.Is <GetActiveProductsOfCustomerRequest>(x => x.Customer.CustomerID == 5);

            var customerProducts = new List<CustomerProductAssignment>();
            var defaultProductSpecific = CreateDefaultObject.Create<CustomerProductAssignment>();
            defaultProductSpecific.PurchasedProduct.Id = 1003000000;
            var defaultProductPermission = CreateDefaultObject.Create<CustomerProductAssignment>();
            defaultProductPermission.PurchasedProduct.Id = 1003000065;
            var defaultProductDataSent = CreateDefaultObject.Create<CustomerProductAssignment>();
            defaultProductDataSent.PurchasedProduct.Id = 1003000067;
            defaultProductDataSent.ProductChargePurchased.Charges.Add(new ChargeNonRecurring());
            var defaultProductNotConfig = CreateDefaultObject.Create<CustomerProductAssignment>();
            defaultProductNotConfig.PurchasedProduct.Id = 1003000014;
            defaultProductNotConfig.ProductChargePurchased.Charges.Add(new ChargeNonRecurring());
            customerProducts.Add(defaultProductSpecific);
            customerProducts.Add(defaultProductPermission);
            customerProducts.Add(defaultProductDataSent);
            customerProducts.Add(defaultProductNotConfig);
            var actualgetActiveProductsOfCustomerResponse = new GetActiveProductsOfCustomerResponse()
            {
                CustomerProductAssignments = customerProducts
            };

            getActiveProductsOfCustomerMS.Process(request, Arg.Any<RequestInvokationEnvironment>()).Returns(actualgetActiveProductsOfCustomerResponse);
            var requestDTO = new GetActiveProductsOfCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 5,
            };
            
            MockAbsctractBusinessOperation(requestDTO);

            mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedActualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockedActualCustomerInfo.PropertyInfo = new List<PropertyInfo>()
            {
                CreateDefaultObject.Create<PropertyInfo>()
            };
            mockedActualCustomerInfo.PropertyInfo.First().PendingStatus = (int)PendingStatus.Active;
            mockedActualCustomerInfo.CustomerID = 5;
            mockedActualCustomerInfo.ResourceMBInfo.Clear();
            mockedActualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedActualCustomerInfo.ResourceMBInfo.First().CustomerInfo.CustomerID = 5;
            mockCustomerRepo.GetById(mockedActualCustomerInfo.CustomerID.Value).Returns(mockedActualCustomerInfo);

            var responseDTO = CallBizOp(requestDTO);

               
            Assert.AreEqual(responseDTO.resultType, ResultTypes.Ok);
            Assert.IsTrue(responseDTO.CustomerProductAssignments.Count(x => x.PurchasedProductId == 1003000000) == 1);
            Assert.IsTrue(responseDTO.CustomerProductAssignments.Count(x => x.PurchasedProductId == 1003000065) == 1);
            Assert.IsTrue(responseDTO.CustomerProductAssignments.Count(x => x.PurchasedProductId == 1003000067) == 1);
            Assert.IsTrue(responseDTO.CustomerProductAssignments.Count(x => x.PurchasedProductId == 1003000014) == 1);
        }

        [TestCase()]
        public void GetActiveProductsOfCustomerBizOp_OKRequest_EmptyResponse()
        {
            var getActiveProductsOfCustomerMS = MockMicroService<GetActiveProductsOfCustomerRequest, GetActiveProductsOfCustomerResponse>();
            var getInvoicesByCustomerIdAndLegalInvoiceNumberMS = MockMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();

            var getInvoicesRequest = new GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest();
            var getInvoicesResponse = new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse()
            {
                Invoices = new[] { new Invoice() { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue } }
            };
            getInvoicesByCustomerIdAndLegalInvoiceNumberMS.Process(getInvoicesRequest, null).ReturnsForAnyArgs(getInvoicesResponse);

            int[] products = new int[] { 1003000000, 1003000001, 1003000002, 1003000003, 1003000004, 1003000005, 1003000006, 1003000027, 1003000028 };
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var actualOperationConfigurations = new List<OperationConfiguration>();
            var actualOperationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var actualBizOpConfiguration = new ActiveProductsConfiguration();
            actualBizOpConfiguration.SpecificProductsForActiveProducts = new List<int>();
            foreach (var product in products) 
            {
                actualBizOpConfiguration.SpecificProductsForActiveProducts.Add(product);
            }            
            
            actualOperationConfiguration.JSonConfig = JsonConvert.SerializeObject(actualBizOpConfiguration);
            actualOperationConfiguration.EndDate = new DateTime(2016, 5, 5);
            actualOperationConfiguration.StarTime = DateTime.Now;
            actualOperationConfigurations.Add(actualOperationConfiguration);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(actualOperationConfigurations);
            
            var request = Arg.Is<GetActiveProductsOfCustomerRequest>(x => x.Customer.CustomerID == 5);

            var response = new GetActiveProductsOfCustomerResponse()
            {
                CustomerProductAssignments = new List<CustomerProductAssignment>()
            };

            getActiveProductsOfCustomerMS.Process(request, Arg.Any<RequestInvokationEnvironment>()).Returns(response);
            var requestDTO = new GetActiveProductsOfCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 5,
            };

            MockAbsctractBusinessOperation(requestDTO);
            mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedActualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockedActualCustomerInfo.PropertyInfo = new List<PropertyInfo>()
            {
                CreateDefaultObject.Create<PropertyInfo>()
            };
            mockedActualCustomerInfo.PropertyInfo.First().PendingStatus = (int)PendingStatus.Active;
            mockedActualCustomerInfo.CustomerID = 5;
            mockedActualCustomerInfo.ResourceMBInfo.Clear();
            mockedActualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedActualCustomerInfo.ResourceMBInfo.First().CustomerInfo.CustomerID = 5;
            mockCustomerRepo.GetById(mockedActualCustomerInfo.CustomerID.Value).Returns(mockedActualCustomerInfo);

            var responseDTO = CallBizOp(requestDTO);

            var expectedResponseDTO = new GetActiveProductsOfCustomerResponseDTO()
            {
                CustomerProductAssignments = new List<CustomerProductAssignmentDTO>(),
                Subscription = new SubscriptionDTO() { CustomerId = 5}
            };

            Assert.AreEqual(responseDTO.resultType, ResultTypes.Ok);         
            Assert.IsEmpty(responseDTO.CustomerProductAssignments);
            Assert.AreEqual(responseDTO.Subscription.CustomerId, expectedResponseDTO.Subscription.CustomerId);

        }

        [TestCase()]
        public void GetActiveProductsOfCustomerBizOp_CustomerIsNotProvided_ThrowBusinessLogicErrorException()
        {
            var getActiveProductsOfCustomerMS = MockMicroService<GetActiveProductsOfCustomerRequest, GetActiveProductsOfCustomerResponse>();

            var request = Arg.Is<GetActiveProductsOfCustomerRequest>(x => x.Customer == null);

            var response = new GetActiveProductsOfCustomerResponse()
            {
                CustomerProductAssignments = null
            };

            getActiveProductsOfCustomerMS.Process(request, Arg.Any<RequestInvokationEnvironment>()).Returns(response);
            var requestDTO = new GetActiveProductsOfCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
            };

            MockAbsctractBusinessOperation(requestDTO);

            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            CustomerInfo customerInfo = null;
            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(customerInfo);

            var responseDTO = CallBizOp(requestDTO);

            Assert.AreEqual(responseDTO.resultType, ResultTypes.BussinessLogicError);
            Assert.IsNull(response.CustomerProductAssignments);
            Assert.AreNotEqual(responseDTO.messages.Length, 0);
        }
        [TestCase()]
        public void GetActiveProductsOfCustomerBizOp_ProductsOutOfInvoiceRange_OK()
        {
            var getActiveProductsOfCustomerMS = MockMicroService<GetActiveProductsOfCustomerRequest, GetActiveProductsOfCustomerResponse>();
            var getInvoicesByCustomerIdAndLegalInvoiceNumberMS = MockMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();

            var getInvoicesRequest = new GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest();
            var getInvoicesResponse = new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse()
            {
                Invoices = new[] { new Invoice() { StartDate = DateTime.Now.AddHours(-1), EndDate = DateTime.MaxValue } }
            };
            getInvoicesByCustomerIdAndLegalInvoiceNumberMS.Process(getInvoicesRequest, null).ReturnsForAnyArgs(getInvoicesResponse);

            int[] specificProducts = { 1003000000, 1003000001, 1003000002, 1003000003, 1003000004, 1003000005, 1003000006, 1003000027, 1003000028 };
            int[] dataTransferPermissions = { 1003000065, 1003000066 };
            int[] dataTransferSent = { 1003000067 };
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var actualOperationConfigurations = new List<OperationConfiguration>();
            var actualOperationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var actualBizOpConfiguration = new ActiveProductsConfiguration();
            actualBizOpConfiguration.SpecificProductsForActiveProducts = new List<int>();
            actualBizOpConfiguration.DataTransferPermissions = new List<int>();
            foreach (var product in specificProducts)
            {
                actualBizOpConfiguration.SpecificProductsForActiveProducts.Add(product);
            }
            foreach (var product in dataTransferPermissions)
            {
                actualBizOpConfiguration.DataTransferPermissions.Add(product);
            }

            actualOperationConfiguration.JSonConfig = JsonConvert.SerializeObject(actualBizOpConfiguration);
            actualOperationConfiguration.EndDate = new DateTime(2016, 5, 5);
            actualOperationConfiguration.StarTime = DateTime.Now;
            actualOperationConfigurations.Add(actualOperationConfiguration);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(actualOperationConfigurations);

            var request = Arg.Is<GetActiveProductsOfCustomerRequest>(x => x.Customer.CustomerID == 1);

            var customerProducts = new List<CustomerProductAssignment>();
            var defaultProductSpecific = CreateDefaultObject.Create<CustomerProductAssignment>();
            defaultProductSpecific.PurchasedProduct.Id = 1003000000;
            defaultProductSpecific.StartDate = DateTime.Now.AddDays(-1);
            var defaultProductPermission = CreateDefaultObject.Create<CustomerProductAssignment>();
            defaultProductPermission.PurchasedProduct.Id = 1003000065;
            defaultProductPermission.StartDate = DateTime.Now.AddDays(-1);
            var defaultProductDataSent = CreateDefaultObject.Create<CustomerProductAssignment>();
            defaultProductDataSent.PurchasedProduct.Id = 1003000067;
            defaultProductDataSent.StartDate = DateTime.Now.AddDays(-1);
            defaultProductDataSent.ProductChargePurchased.Charges.Add(new ChargeNonRecurring());
            var defaultProductNotConfig = CreateDefaultObject.Create<CustomerProductAssignment>();
            defaultProductNotConfig.PurchasedProduct.Id = 1003000014;
            defaultProductNotConfig.StartDate = DateTime.Now.AddDays(-1);
            defaultProductNotConfig.ProductChargePurchased.Charges.Add(new ChargeNonRecurring());
            customerProducts.Add(defaultProductSpecific);
            customerProducts.Add(defaultProductPermission);
            customerProducts.Add(defaultProductDataSent);
            customerProducts.Add(defaultProductNotConfig);
            var actualgetActiveProductsOfCustomerResponse = new GetActiveProductsOfCustomerResponse()
            {
                CustomerProductAssignments = customerProducts
            };

            getActiveProductsOfCustomerMS.Process(request, Arg.Any<RequestInvokationEnvironment>()).Returns(actualgetActiveProductsOfCustomerResponse);
            var requestDTO = new GetActiveProductsOfCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
            };

            MockAbsctractBusinessOperation(requestDTO);

            var responseDTO = CallBizOp(requestDTO);

            Assert.AreEqual(responseDTO.resultType, ResultTypes.Ok);
            Assert.IsTrue(responseDTO.CustomerProductAssignments.Count(x => x.PurchasedProductId == 1003000000) == 1);
            Assert.IsTrue(responseDTO.CustomerProductAssignments.Count(x => x.PurchasedProductId == 1003000065) == 1);
            Assert.IsTrue(responseDTO.CustomerProductAssignments.Count(x => x.PurchasedProductId == 1003000067) == 0);
            Assert.IsTrue(responseDTO.CustomerProductAssignments.Count(x => x.PurchasedProductId == 1003000014) == 0);
        }

    }
}
