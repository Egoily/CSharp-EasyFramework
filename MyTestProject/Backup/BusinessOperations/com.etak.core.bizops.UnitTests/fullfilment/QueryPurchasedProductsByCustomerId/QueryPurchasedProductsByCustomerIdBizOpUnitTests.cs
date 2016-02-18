using System;
using System.Collections.Generic;
using com.etak.core.bizops.fullfilment.QueryPurchasedProductsByCustomerId;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerId;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.QueryPurchasedProductsByCustomerId
{
    [TestFixture()]
    public class QueryPurchasedProductsByCustomerIdBizOpUnitTests : AbstractBusinessOperationTest<QueryPurchasedProductsByCustomerIdBizOp, QueryPurchasedProductsByCustomerIdRequestDTO, QueryPurchasedProductsByCustomerIdResponseDTO, QueryPurchasedProductsByCustomerIdRequestInternal, QueryPurchasedProductsByCustomerIdResponseInternal>
    {
        private
            IMicroService
                <GetCustomerProductAssignmentsByCustomerIdRequest, GetCustomerProductAssignmentsByCustomerIdResponse>
            mockMicroServiceGetCustomerProductAssignmentsByCustomerId;
        private
            IMicroService
                <CheckAuthorizationRequest, CheckAuthorizationResponse>
            mockMicroServiceCheckAuthorization;

        public void reMockGetCustomer(QueryPurchasedProductsByCustomerIdRequestDTO requestDto)
        {
            var mockGetCustomerMS = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedCustomer = CreateDefaultObject.Create<CustomerInfo>();
            mockedCustomer.CustomerID = requestDto.CustomerId;
            mockedCustomer.ResourceMBInfo.Clear();
            var mockedSubscription = CreateDefaultObject.Create<ResourceMBInfo>();
            mockedSubscription.CustomerInfo.CustomerID = requestDto.CustomerId;
            mockedCustomer.ResourceMBInfo.Add(mockedSubscription);
            mockGetCustomerMS.GetById(requestDto.CustomerId).ReturnsForAnyArgs(mockedCustomer);
        }

        [TestFixtureSetUp()]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceGetCustomerProductAssignmentsByCustomerId = MockMicroService<GetCustomerProductAssignmentsByCustomerIdRequest, GetCustomerProductAssignmentsByCustomerIdResponse>();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void QueryPurchasedProductsByCustomerIdBizOp_GivenCustomerId_ShouldReturnOKCustomerProductAssignments()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse{IsAuthorized = true};
                
            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);


            var mockedProductOfferingRepo = MockRepositoryManager.GetMockedRepository<IProductOfferingRepository<ProductOffering>>();
            mockedProductOfferingRepo.GetByGroupId(Arg.Any<int>()).ReturnsForAnyArgs(new List<ProductOffering>());

            var actualGetGetCustomerProductAssignmentsByCustomerIdResponse =
                CreateDefaultObject.Create<GetCustomerProductAssignmentsByCustomerIdResponse>();
            var actualCustomerProductAssignments = new List<CustomerProductAssignment>();
            var actualCustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>();
            //populate value for each attribut that not in primitive type
            actualCustomerProductAssignment.ProductChargePurchased = new ProductChargeOption();//CreateDefaultObject.Create<ProductChargeOption>();
            actualCustomerProductAssignment.ProductChargePurchased.Id = 5;
            actualCustomerProductAssignment.PurchasedProduct = CreateDefaultObject.Create<Product>();
            actualCustomerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan.APIVisible = APIVisible.Visible;
            actualCustomerProductAssignment.PurchasingCustomer = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerProductAssignment.StartDate = new DateTime(2016, 5, 5);
            actualCustomerProductAssignment.EndDate = null;
            var actualCustomerProductAssignment2 = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCustomerProductAssignment2.StartDate = new DateTime(2017, 5, 5);
            actualCustomerProductAssignment2.EndDate = null;
            actualCustomerProductAssignment2.ProductChargePurchased = new ProductChargeOption();//CreateDefaultObject.Create<ProductChargeOption>();
            actualCustomerProductAssignment2.ProductChargePurchased.Id = 5;
            actualCustomerProductAssignment2.PurchasedProduct = CreateDefaultObject.Create<Product>();
            actualCustomerProductAssignment2.PurchasedProduct.AssociatedPrmotionPlan.APIVisible = APIVisible.Visible;
            actualCustomerProductAssignment2.PurchasingCustomer = CreateDefaultObject.Create<CustomerInfo>();
            var actualCustomerProductAssignment3 = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCustomerProductAssignment3.StartDate = new DateTime(2016, 5, 5);
            actualCustomerProductAssignment3.EndDate = new DateTime(2016, 6, 5);
            actualCustomerProductAssignment3.ProductChargePurchased = new ProductChargeOption();//CreateDefaultObject.Create<ProductChargeOption>();
            actualCustomerProductAssignment3.ProductChargePurchased.Id = 5;
            actualCustomerProductAssignment3.PurchasedProduct = CreateDefaultObject.Create<Product>();
            actualCustomerProductAssignment3.PurchasedProduct.AssociatedPrmotionPlan.APIVisible = APIVisible.Visible;
            actualCustomerProductAssignment3.PurchasingCustomer = CreateDefaultObject.Create<CustomerInfo>();

            //add it to list
            actualCustomerProductAssignments.Add(actualCustomerProductAssignment);
            actualCustomerProductAssignments.Add(actualCustomerProductAssignment2);
            actualCustomerProductAssignments.Add(actualCustomerProductAssignment3);
            // addd list to response
            actualGetGetCustomerProductAssignmentsByCustomerIdResponse.CustomerProductAssignments =
                actualCustomerProductAssignments;

            //expected DTO
            var expectedProductPurchaseDTOs = new List<CustomerProductAssignmentDTO>();
            var expectedProductPurchaseDTO = CreateDefaultObject.Create<CustomerProductAssignmentDTO>();
            expectedProductPurchaseDTO.PurchasingCustomerId = (int)actualCustomerProductAssignment.PurchasingCustomer.CustomerID;
            expectedProductPurchaseDTO.ProductChargePurchasedId = actualCustomerProductAssignment.ProductChargePurchased.Id;
            expectedProductPurchaseDTO.PurchasedProductId = actualCustomerProductAssignment.PurchasedProduct.Id;

            expectedProductPurchaseDTOs.Add(expectedProductPurchaseDTO);
            expectedProductPurchaseDTOs.Add(expectedProductPurchaseDTO);

            var actualGetCustomerProductAssignmentsByCustomerIdRequest = Arg.Is<GetCustomerProductAssignmentsByCustomerIdRequest>(x => x.CustomerId == 1000000000);
            mockMicroServiceGetCustomerProductAssignmentsByCustomerId.Process(
                actualGetCustomerProductAssignmentsByCustomerIdRequest, null).Returns(actualGetGetCustomerProductAssignmentsByCustomerIdResponse);

            var requestDto = new QueryPurchasedProductsByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1000000000,
            };

            MockAbsctractBusinessOperation(requestDto);
            reMockGetCustomer(requestDto);

            var res = CallBizOp(requestDto);
            Assert.AreEqual(res.resultType, ResultTypes.Ok);
            Assert.IsTrue(res.ProductsPurchaseDto.Count == 3);
            Assert.IsTrue(res.Subscription.CustomerId == requestDto.CustomerId);

            requestDto = new QueryPurchasedProductsByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1000000000,
                StartDate = new DateTime(2016, 5, 5),
                EndDate = new DateTime(2016, 6, 5),
            };

            MockAbsctractBusinessOperation(requestDto);
            reMockGetCustomer(requestDto);
            res = CallBizOp(requestDto);
            Assert.AreEqual(res.resultType, ResultTypes.Ok);
            Assert.IsTrue(res.ProductsPurchaseDto.Count == 2);
            Assert.IsTrue(res.Subscription.CustomerId == requestDto.CustomerId);
        }

        [Test()]
        public void QueryPurchasedProductsByCustomerIdBizOp_GivenCustomerId_ShouldReturnOKNullCustomerProductAssignments()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            
            var actualGetCustomerProductAssignmentsByCustomerIdResponse =
                CreateDefaultObject.Create<GetCustomerProductAssignmentsByCustomerIdResponse>();
            actualGetCustomerProductAssignmentsByCustomerIdResponse.CustomerProductAssignments =
                new List<CustomerProductAssignment>(){};

            var actualGetCustomerProductAssignmentsByCustomerIdRequest = Arg.Is<GetCustomerProductAssignmentsByCustomerIdRequest>(x => x.CustomerId == 2000000000);
            mockMicroServiceGetCustomerProductAssignmentsByCustomerId.Process(
                actualGetCustomerProductAssignmentsByCustomerIdRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(actualGetCustomerProductAssignmentsByCustomerIdResponse);

            var request = new QueryPurchasedProductsByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 200000000,
                
            };

            MockAbsctractBusinessOperation(request);
            reMockGetCustomer(request);
            var response = CallBizOp(request);

            Assert.IsEmpty(response.ProductsPurchaseDto);
            Assert.AreEqual(response.resultType, ResultTypes.Ok);
            Assert.IsTrue(response.Subscription.CustomerId == request.CustomerId);
        }

        [Test()]
        public void QueryPurchasedProductsByCustomerIdBizOp_GivenCustomerId_ShouldReturnNOKCustomerProductAssignments()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var actualGetCustomerProductAssignmentsByCustomerIdRequest = Arg.Is<GetCustomerProductAssignmentsByCustomerIdRequest>(x => x.CustomerId == 300000000);
            mockMicroServiceGetCustomerProductAssignmentsByCustomerId.Process(
                actualGetCustomerProductAssignmentsByCustomerIdRequest, null).Returns(x => { throw new Exception("Error"); });

            var requestDto = new QueryPurchasedProductsByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 300000000
            };

            MockAbsctractBusinessOperation(requestDto);
            var response = CallBizOp(requestDto);
            Assert.IsTrue(response.resultType == ResultTypes.UnknownError);
        }

        [Test()]
        public void QueryPurchasedProductsByCustomerIdBizOp_StartDateLaterThanEndDate_ShouldThrowDataValidationException()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var requestDto = new QueryPurchasedProductsByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 300000000,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now.AddDays(1)
            };

            MockAbsctractBusinessOperation(requestDto);
            var response = CallBizOp(requestDto);
            Assert.IsTrue(response.resultType == ResultTypes.DataValidationError);
        }

        [Test()]
        public void QueryPurchasedProductsByCustomerIdBizOp_NOK_NullCustomerId()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var requestDto = new QueryPurchasedProductsByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1000000000
            };

            MockAbsctractBusinessOperation(requestDto);
            var mockedcustomerrepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            mockedcustomerrepo.GetById(new int()).ReturnsForAnyArgs((CustomerInfo) null);

            var res = CallBizOp(requestDto);
            Assert.AreEqual(res.resultType, ResultTypes.BussinessLogicError);  
        }
        [Test()]
        public void QueryPurchasedProductsByCustomerIdBizOp_NOK_authorizationfailed()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var requestDto = new QueryPurchasedProductsByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1000000000
            };

            MockAbsctractBusinessOperation(requestDto);

            var res = CallBizOp(requestDto);
            Assert.AreEqual(res.resultType, ResultTypes.AuthorizationError);
            Assert.AreEqual(res.errorCode,BizOpsErrors.AuthorizeErrorUser);
        }
    }
}