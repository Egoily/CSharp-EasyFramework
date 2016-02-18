using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.QueryCustomerProduct;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerId;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.subscription;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;
using List = NHibernate.Mapping.List;

namespace com.etak.core.bizops.UnitTests.fullfilment.QueryCustomerProduct
{
    [TestFixture()]
    public class QueryCustomerProductBizOpUnitTests : AbstractBusinessOperationTest<QueryCustomerProductBizOp, QueryCustomerProductRequestDTO, QueryCustomerProductResponseDTO, QueryCustomerProductRequestInternal, QueryCustomerProductResponseInternal>
    {
        public GetCustomerProductAssignmentsByCustomerIdResponse StandardGetCustomerProductAssignmentsByCustomerIdResponse()
        {
            var actualGetCustomerProductAssignmentsByCustomerIdResponse =
                CreateDefaultObject.Create<GetCustomerProductAssignmentsByCustomerIdResponse>();
            var actualCustomerProductAssignments = new List<CustomerProductAssignment>();
            var actualCustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>();
            actualCustomerProductAssignment.ProductChargePurchased = new ProductChargeOption();
            actualCustomerProductAssignment.ProductChargePurchased.Id = 1;
            actualCustomerProductAssignment.PurchasedProduct = CreateDefaultObject.Create<Product>();
            actualCustomerProductAssignment.PurchasingCustomer = CreateDefaultObject.Create<CustomerInfo>();

            actualCustomerProductAssignments.Add(actualCustomerProductAssignment);
            actualGetCustomerProductAssignmentsByCustomerIdResponse.CustomerProductAssignments =
                actualCustomerProductAssignments;
            return actualGetCustomerProductAssignmentsByCustomerIdResponse;
        }

        public void StandardMocks()
        {
            var mockGetCustomerProductsAssignmentsByCustomerIdMS = MockMicroService<GetCustomerProductAssignmentsByCustomerIdRequest, GetCustomerProductAssignmentsByCustomerIdResponse>();
            var mockGetCustomer = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
#region Case1
            var actualCustomer1 = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomer1.CustomerID = 1;
            var actualsubscription1 = CreateDefaultObject.Create<ResourceMBInfo>();
            actualsubscription1.CustomerInfo.CustomerID = 1;
            actualCustomer1.ResourceMBInfo.Clear();
            actualCustomer1.ResourceMBInfo.Add(actualsubscription1);
            var actualresponse1 = new GetCustomerProductAssignmentsByCustomerIdResponse();
            actualresponse1.CustomerProductAssignments = new List<CustomerProductAssignment>{CreateDefaultObject.Create<CustomerProductAssignment>()};
            actualresponse1.CustomerProductAssignments.First().PurchasingCustomer.CustomerID = 1;
            var actualGetCustomerProductAssignmentsByCustomerIdRequest1 = Arg.Is<GetCustomerProductAssignmentsByCustomerIdRequest>(x => x.CustomerId == 1);
            mockGetCustomerProductsAssignmentsByCustomerIdMS.Process(actualGetCustomerProductAssignmentsByCustomerIdRequest1, Arg.Any<RequestInvokationEnvironment>())
    .Returns(actualresponse1);
            mockGetCustomer.GetById(1).Returns(actualCustomer1);
#endregion

            #region Case2
            var actualCustomer2 = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomer2.CustomerID = 2;
            var actualsubscription2 = CreateDefaultObject.Create<ResourceMBInfo>();
            actualsubscription2.CustomerInfo.CustomerID = 2;
            actualCustomer2.ResourceMBInfo.Clear();
            actualCustomer2.ResourceMBInfo.Add(actualsubscription2);
            var actualGetCustomerProductAssignmentsByCustomerIdRequest2 = Arg.Is<GetCustomerProductAssignmentsByCustomerIdRequest>(x => x.CustomerId == 2);
            mockGetCustomerProductsAssignmentsByCustomerIdMS.Process(actualGetCustomerProductAssignmentsByCustomerIdRequest2, Arg.Any<RequestInvokationEnvironment>())
    .Returns(x => { throw new DataValidationErrorException("Error",6); });
            mockGetCustomer.GetById(2).Returns(actualCustomer2);
            #endregion

            #region Case3
            var actualCustomer3 = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomer3.CustomerID = 3;
            var actualsubscription3 = CreateDefaultObject.Create<ResourceMBInfo>();
            actualsubscription3.CustomerInfo.CustomerID = 3;
            actualCustomer3.ResourceMBInfo.Clear();
            actualCustomer3.ResourceMBInfo.Add(actualsubscription3);
            var actualresponse3 = new GetCustomerProductAssignmentsByCustomerIdResponse
            {
                CustomerProductAssignments = new List<CustomerProductAssignment>()
            
            };
            var actualGetCustomerProductAssignmentsByCustomerIdRequest3 = Arg.Is<GetCustomerProductAssignmentsByCustomerIdRequest>(x => x.CustomerId == 3);
            mockGetCustomerProductsAssignmentsByCustomerIdMS.Process(actualGetCustomerProductAssignmentsByCustomerIdRequest3, Arg.Any<RequestInvokationEnvironment>())
    .Returns(actualresponse3);
            mockGetCustomer.GetById(3).Returns(actualCustomer3);
            #endregion
        }
        [TestFixtureSetUp]
        public static void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [Test()]
        public void QueryCustomerProductBizOp_CorrectCustomerIdGiven_ShouldReturnCorrectCustomerProduct()
        {
            var standardGetCustomerProductAssResponse = StandardGetCustomerProductAssignmentsByCustomerIdResponse();

            var actualRequest = new QueryCustomerProductRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
            };

            var expectedProductPurchaseDTOs = new List<CustomerProductAssignmentDTO>();
            var expectedProductPurchaseDTO = CreateDefaultObject.Create<CustomerProductAssignmentDTO>();
            expectedProductPurchaseDTO.PurchasingCustomerId = actualRequest.CustomerId;
            expectedProductPurchaseDTO.ProductChargePurchasedId = standardGetCustomerProductAssResponse.CustomerProductAssignments.First().ProductChargePurchased.Id;
            expectedProductPurchaseDTO.PurchasedProductId = standardGetCustomerProductAssResponse.CustomerProductAssignments.First().PurchasedProduct.Id;
            expectedProductPurchaseDTO.CreatingOrderId = 0;
            expectedProductPurchaseDTOs.Add(expectedProductPurchaseDTO);

            var expectedQueryCustomerProductResponseDTO = new QueryCustomerProductResponseDTO
            {
                resultType = ResultTypes.Ok,
                ProductPurchaseDto = expectedProductPurchaseDTOs,
                Subscription = new SubscriptionDTO { CustomerId = actualRequest.CustomerId }
            };

            MockAbsctractBusinessOperation(actualRequest);
            StandardMocks();
            var actualresponse = CallBizOp(actualRequest);
            AssertExt.ObjectPropertiesAreEqual(expectedQueryCustomerProductResponseDTO.ProductPurchaseDto, actualresponse.ProductPurchaseDto);
            Assert.IsTrue(actualresponse.resultType == expectedQueryCustomerProductResponseDTO.resultType);
            Assert.IsTrue(actualresponse.Subscription.CustomerId == expectedQueryCustomerProductResponseDTO.Subscription.CustomerId);
        }

        [Test()]
        public void QueryCustomerProductBizOp_IncorrectCustomerProductsAssignmentGiven_ShouldThrowException()
        {

            var actualRequest = new QueryCustomerProductRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 2,
            };


            MockAbsctractBusinessOperation(actualRequest);
            StandardMocks();
            var response = CallBizOp(actualRequest);
            Assert.IsTrue(response.resultType == ResultTypes.DataValidationError);
        }

        [Test()]
        public void QueryCustomerProductBizOp_EmptyProductGiven_ShouldReturnEmptyProduct()
        {

            var actualRequest = new QueryCustomerProductRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 3,
            };

            MockAbsctractBusinessOperation(actualRequest);
            StandardMocks();
            var response = CallBizOp(actualRequest);
            Assert.IsEmpty(response.ProductPurchaseDto);
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
        }
    }
}