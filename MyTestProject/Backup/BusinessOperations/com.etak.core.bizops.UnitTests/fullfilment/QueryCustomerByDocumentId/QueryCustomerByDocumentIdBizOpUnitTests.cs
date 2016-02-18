using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.QueryCustomerByDocumentId;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.subscription;
using com.etak.core.operation.dtoConverters;
using com.etak.core.repository.crm;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.subscription;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.QueryCustomerByDocumentId
{
    [TestFixture()]
    public class QueryCustomerByDocumentIdBizOpUnitTests :
        AbstractBusinessOperationTest
            <QueryCustomerByDocumentIdBizOp, QueryCustomerByDocumentIdRequestDTO, QueryCustomerByDocumentIdResponseDTO,
                QueryCustomerByDocumentIdRequestInternal, QueryCustomerByDocumentIdResponseInternal>
    {
        public CustomerInfo StandardCustomerInfo()
        {
            var expectedCustomer = CreateDefaultObject.Create<CustomerInfo>();
            expectedCustomer.DealerID = 1;
            expectedCustomer.CustomerID = 9;
            expectedCustomer.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active).CustomerInfo
                = expectedCustomer;
            return expectedCustomer;
        }

        public void StandardMockCustomerInfo(CustomerInfo expectedCustomerInfo)
        {
            var mockRepoCust = MockRepositoryManager.GetMockedRepository<IPropertyInfoRepository<PropertyInfo>>();
            var propertyInfo = new PropertyInfo()
            {
                CustomerInfo = expectedCustomerInfo,
            };
            var listCust = new List<PropertyInfo>() { propertyInfo };
            mockRepoCust.GetByDocumentId(1, "100").Returns(listCust);
            mockRepoCust.GetByDocumentId(1, "200").Returns((new List<PropertyInfo>()));

            var mockRepoDealer = MockRepositoryManager.GetMockedRepository<IDealerInfoRepository<DealerInfo>>();
            var returnedDealer = CreateDefaultObject.Create<DealerInfo>();
            mockRepoDealer.GetById(expectedCustomerInfo.DealerID.Value).Returns(returnedDealer);

            var mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            mockCustomerRepo.GetById(expectedCustomerInfo.CustomerID.Value).Returns(expectedCustomerInfo);
        }

        [TestFixtureSetUp]
        public static void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [Test()]
        public void QueryCustomerByDocumentIdBizOp_CorrectRequestGiven_ShouldReturnCorrectCustomer()
        {
            var expectedCustomer = StandardCustomerInfo();
            var expectedResponse = new QueryCustomerByDocumentIdResponseDTO()
            {
                Customers = new List<CustomerDTO>(){expectedCustomer.ToDto()},
                Customer = expectedCustomer.ToDto(),
                Subscription = new SubscriptionDTO { CustomerId = expectedCustomer.CustomerID.Value}
            };

            var queryCustomerByDocumentIdBizOpRequestDTO = new QueryCustomerByDocumentIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                DocumentNumber = "100",
                DocumentType = 1
            };

            MockAbsctractBusinessOperation(queryCustomerByDocumentIdBizOpRequestDTO);
            #region Remock repositories
            StandardMockCustomerInfo(expectedCustomer);
            #endregion

            var queryCustomerByDocumentIdBizOpResponseDTO = CallBizOp(queryCustomerByDocumentIdBizOpRequestDTO);
            AssertExt.ObjectPropertiesAreEqual(expectedResponse.Customers,
                queryCustomerByDocumentIdBizOpResponseDTO.Customers);
            Assert.IsTrue(expectedResponse.Customer.CustomerId ==
                queryCustomerByDocumentIdBizOpResponseDTO.Customer.CustomerId);
            Assert.IsTrue(expectedResponse.Subscription.CustomerId ==
                queryCustomerByDocumentIdBizOpResponseDTO.Subscription.CustomerId);
        }

        [Test()]
        public void QueryCustomerByDocumentIdBizOp_CorrectRequest_ShouldReturnNullCustomer()
        {
            var queryCustomerByDocumentIdBizOpRequestDTO = new QueryCustomerByDocumentIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                DocumentNumber = "200",
                DocumentType = 1
            };

            MockAbsctractBusinessOperation(queryCustomerByDocumentIdBizOpRequestDTO);

            #region Remock Repository
            StandardMockCustomerInfo(StandardCustomerInfo());
            #endregion

            var queryCustomerByDocumentIdBizOpResponseDTO = CallBizOp(queryCustomerByDocumentIdBizOpRequestDTO);
            AssertExt.IsEmpty(queryCustomerByDocumentIdBizOpResponseDTO.Customers);
            AssertExt.IsNull(queryCustomerByDocumentIdBizOpResponseDTO.Customer);
            AssertExt.IsNull(queryCustomerByDocumentIdBizOpResponseDTO.Subscription);
        }
    }
}